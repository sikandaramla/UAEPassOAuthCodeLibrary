using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UAEPassOAuthCodeLibrary
{
    public class UAEPassAuthenticationHandler : AuthenticationHandler<UAEPassAuthenticationOptions>
    {
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public UAEPassAuthenticationHandler(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = null;

            try
            {
                string code = null;
                string state = null;
                string error = null;

                var query = Request.Query;

                //CHECK FOR ERRORS IF RECEIVED
                var values = query.GetValues("error");
                if (values != null && values.Count > 0)
                {
                    error = values[0];
                }

                //IF ERROR IS RETURNED, DISCONTINUE THE OPERATION FROM HERE.
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }

                //IN CASE OF SUCCESS, SP APP WILL RECEIVE THE CODE
                values = query.GetValues("code");
                if (values != null && values.Count > 0)
                {
                    code = values[0];
                }
                
                
                //CHECK FOR THE STATES
                values = query.GetValues("state");
                if (values != null && values.Count > 0)
                {
                    state = values[0];
                }
                

                properties = Options.StateDataFormat.Unprotect(state);
                if (properties == null)
                {
                    return null;
                }

                //OAuth2 10.12 CROSS SITE REQUEST FORGERY - VALIDATE 
                if (!ValidateCorrelationId(properties, _logger))
                {
                    return new AuthenticationTicket(null, properties);
                }

                //REDIRECT URI
                var requestPrefix = Request.Scheme + "://" + Request.Host;
                var redirectUri = requestPrefix + Request.PathBase + Options.CallbackPath;


                //MAKE A REQUEST TO GET THE ACCESS TOKEN
                HttpClient httpClient = new HttpClient();

                //CREATING BODY FOR TOKEN REQUEST
                IEnumerable<KeyValuePair<string, string>> requestBody = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string,string>("redirect_uri",redirectUri),
                    new KeyValuePair<string, string>("grant_type","authorization_code"),
                    new KeyValuePair<string, string>("client_id",Options.ClientId),
                    new KeyValuePair<string, string>("client_secret",Options.ClientSecret),
                    new KeyValuePair<string, string>("code",code)
                };

                string tokenResponse = await PostFormUrlEncoded<string>(Options.UAEPassEndPoints.TokenEndpoint,requestBody);

                //DESERIALIZE CONTENT
                dynamic response = JsonConvert.DeserializeObject<dynamic>(tokenResponse);

                //STORING ACCESS TOKEN - NO REFRESH TOKEN IS SUPPORTED BY UAE PASS
                var accessToken = (string)response.access_token;
                

                //GET USER'S INFORMATION FROM UAE PASS USING USER INFO END POINT
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                HttpResponseMessage userResponse = await httpClient.GetAsync(Options.UAEPassEndPoints.UserInfoEndpoint);
                userResponse.EnsureSuccessStatusCode();
                var rawResponse = await userResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UAEPassUser>(rawResponse);

                var context = new UAEPassAuthenticatedContext(Context, user, accessToken, rawResponse)
                {
                    Identity = new ClaimsIdentity(
                        Options.AuthenticationType,
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType)
                };

                if (!string.IsNullOrEmpty(context.Id))
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.UserName))
                {
                    context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, context.UserName, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.Email))
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email, XmlSchemaString, Options.AuthenticationType));
                }
                
                if (!string.IsNullOrEmpty(context.Name))
                {
                    context.Identity.AddClaim(new Claim("urn:UAEPass:name", context.Name, XmlSchemaString, Options.AuthenticationType));
                }

                context.Properties = properties;

                await Options.Provider.Authenticated(context);

                var retTicket = new AuthenticationTicket(context.Identity, context.Properties);

                retTicket.Properties.AllowRefresh = false;
                retTicket.Properties.IsPersistent = true;

                return retTicket;
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }
            return new AuthenticationTicket(null, properties);
        }

        //THIS IS USED TO MAKE CALL FOR TOKEN REQUEST
        public static async Task<string> PostFormUrlEncoded<TResult>(string url, IEnumerable<KeyValuePair<string, string>> postData)
        {
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    var retResponse = await response.Content.ReadAsStringAsync();
                    return retResponse;
                }
            }
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }

            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);

            if (challenge == null) return Task.FromResult<object>(null);

            var baseUri =
                Request.Scheme +
                Uri.SchemeDelimiter +
                Request.Host +
                Request.PathBase;

            var currentUri =
                baseUri +
                Request.Path +
                Request.QueryString;

            var redirectUri =
                baseUri +
                Options.CallbackPath;

            var properties = challenge.Properties;
            if (string.IsNullOrEmpty(properties.RedirectUri))
            {
                properties.RedirectUri = currentUri;
            }

            GenerateCorrelationId(properties);
            var state = Options.StateDataFormat.Protect(properties);
            
            //SPACE SEPARATED SCOPES AS PER THE DOCUMENTATION
            var Scope = string.Join(" ", Options.Scope);

            var authorizationEndpoint =
                Options.UAEPassEndPoints.AuthorizationEndpoint +
                "?response_type=" + Uri.EscapeDataString("code") +
                "&redirect_uri=" + Uri.EscapeDataString(redirectUri) +
                "&client_id=" + Uri.EscapeDataString(Options.ClientId) +
                "&client_secret=" + Uri.EscapeDataString(Options.ClientSecret) +
                "&state=" + Uri.EscapeDataString(state) +
                "&scope=" + Uri.EscapeDataString(Scope) +
                "&acr_values=" + Uri.EscapeDataString(Constants.DefaultACRValue) +
                "&ui_locales=" + Uri.EscapeDataString(Options.Locale);
            

            Response.Redirect(authorizationEndpoint);

            return Task.FromResult<object>(null);
        }

        public override async Task<bool> InvokeAsync()
        {
            return await InvokeReplyPathAsync();
        }

        private async Task<bool> InvokeReplyPathAsync()
        {
            if (!Options.CallbackPath.HasValue || Options.CallbackPath != Request.Path) return false;
            // TODO: error responses

            var ticket = await AuthenticateAsync();
            if (ticket == null)
            {
                _logger.WriteWarning("Invalid return state, unable to redirect.");
                Response.StatusCode = 500;
                return true;
            }

            var context = new UAEPassReturnEndpointContext(Context, ticket)
            {
                SignInAsAuthenticationType = Options.SignInAsAuthenticationType,
                RedirectUri = ticket.Properties.RedirectUri
            };

            await Options.Provider.ReturnEndpoint(context);

            //SIGN IN
            if (context.SignInAsAuthenticationType != null &&
                context.Identity != null)
            {
                var grantIdentity = context.Identity;
                if (!string.Equals(grantIdentity.AuthenticationType, context.SignInAsAuthenticationType, StringComparison.Ordinal))
                {
                    grantIdentity = new ClaimsIdentity(grantIdentity.Claims, context.SignInAsAuthenticationType, grantIdentity.NameClaimType, grantIdentity.RoleClaimType);
                }
                Context.Authentication.SignIn(context.Properties, grantIdentity);
            }

            if (context.IsRequestCompleted || context.RedirectUri == null) return context.IsRequestCompleted;
            var redirectUri = context.RedirectUri;
            
            if (context.Identity == null)
            {
                //add a redirect hint that sign-in failed in some way
                redirectUri = WebUtilities.AddQueryString(redirectUri, "error", "access_denied");
            }
            Response.Redirect(redirectUri);
            context.RequestCompleted();

            return context.IsRequestCompleted;
        }
    }
}
