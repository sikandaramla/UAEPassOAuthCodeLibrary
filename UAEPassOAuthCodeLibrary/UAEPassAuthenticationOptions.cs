using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;


namespace UAEPassOAuthCodeLibrary
{
    public class UAEPassAuthenticationOptions : AuthenticationOptions
    {
        public class UAEPassAuthenticationEndpoints
        {
            /// <summary>
            /// Endpoint which is used to redirect users to request UAEPass access
            /// </summary>
            /// <remarks>
            /// Defaults to https://qa-id.uaepass.ae/trustedx-authserver/oauth/main-as if Staging
            /// Defaults to https://id.uaepass.ae/trustedx-authserver/oauth/main-as if Production
            /// </remarks>
            public string AuthorizationEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to exchange code for access token
            /// </summary>
            /// <remarks>
            /// Defaults to https://qa-id.uaepass.ae/trustedx-authserver/oauth/main-as/token if Staging
            /// Defaults to https://id.uaepass.ae/trustedx-authserver/oauth/main-as/token if Production
            /// </remarks>
            public string TokenEndpoint { get; set; }

            /// <summary>
            /// Endpoint which is used to obtain user information after authentication
            /// </summary>
            /// <remarks>
            /// Defaults to https://qa-id.uaepass.ae/trustedx-resources/openid/v1/users/me if Staging
            /// Defaults to https://id.uaepass.ae/trustedx-resources/openid/v1/users/me if Production
            /// </remarks>
            public string UserInfoEndpoint { get; set; }
        }

        
        /// <summary>
        /// The request path within the application's base path where the user-agent will be returned.
        /// The middleware will process this request when it arrives.
        /// Default value is "/uaepass".
        /// </summary>
        public PathString CallbackPath { get; set; }

        /// <summary>
        ///     Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        /// <summary>
        ///     Gets or sets the Google supplied Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the Google supplied Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets whether to request offline access.  If offline access is requested the <see cref="GoogleAuthenticatedContext"/> will contain a Refresh Token.
        /// </summary>
        public bool RequestOfflineAccess { get; set; }

        /// <summary>
        /// A list of permissions to request.
        /// </summary>
        public IList<string> Scope { get; set; }

        /// <summary>
        /// Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user
        /// <see cref="System.Security.Claims.ClaimsIdentity" />.
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }

        /// <summary>
        /// CLASS CONTAINING ALL THE END POINTS REQUIRED
        /// </summary>
        public UAEPassAuthenticationEndpoints UAEPassEndPoints { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UAEPassAuthenticationProvider" /> used in the authentication events
        /// </summary>
        public IUAEPassAuthenticationProvider Provider { get; set; }

        /// <summary>
        ///     Gets or sets the a pinned certificate validator to use to validate the endpoints used
        ///     in back channel communications belong to UAEPassOnline.
        /// </summary>
        /// <value>
        ///     The pinned certificate validator.
        /// </value>
        /// <remarks>
        ///     If this property is null then the default certificate checks are performed,
        ///     validating the subject name and if the signing chain is a trusted party.
        /// </remarks>
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        /// <summary>
        /// The HttpMessageHandler used to communicate with UAEPass.
        /// This cannot be set at the same time as BackchannelCertificateValidator unless the value
        /// can be downcast to a WebRequestHandler.
        /// </summary>
        public HttpMessageHandler BackchannelHttpHandler { get; set; }


        /// <summary>
        ///     Gets or sets timeout value in milliseconds for back channel communications with UAEPass.
        /// </summary>
        /// <value>
        ///     The back channel timeout in milliseconds.
        /// </value>
        public TimeSpan BackchannelTimeout { get; set; }


        /// <summary>
        ///     Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        /// <summary>
        /// Gets or sets the locale for the request, value can be en or ar for English or Arabic respectively.
        /// </summary>
        public string Locale { get; set; }


        /// <summary>
        ///     Initializes a new <see cref="GoogleAuthenticationOptions" />
        /// </summary>
        public UAEPassAuthenticationOptions(uaePassEnum.authenticationType AuthenticationType, uaePassEnum.localeType LocaleType, List<string> ScopeList, string callBackPath = "") : base("UAEPass")
        {
            Caption = Constants.DefaultAuthenticationType;

            //SET CALL BACK PATH
            CallbackPath = new PathString("/uaepass");

            if (!string.IsNullOrEmpty(callBackPath))
            {
                if(callBackPath.Substring(0,1) != "/")
                {
                    callBackPath = "/" + callBackPath;
                }

                CallbackPath = new PathString(callBackPath);
            }

            AuthenticationMode = AuthenticationMode.Passive;
            BackchannelTimeout = TimeSpan.FromSeconds(60);

            //SET END POINT URLs
            if (AuthenticationType == uaePassEnum.authenticationType.Production)
            {
                Caption = "UAE Pass Authentication";
                UAEPassEndPoints = new UAEPassAuthenticationEndpoints()
                {
                    AuthorizationEndpoint = Constants.DefaultProductionAuthorizationEndpoint,
                    TokenEndpoint = Constants.DefaultProductionTokenEndpoint,
                    UserInfoEndpoint = Constants.DefaultProductionUserInfoEndpoint
                };                
            }
            else
            {
                Caption = "UAE Pass Authentication - QA";
                UAEPassEndPoints = new UAEPassAuthenticationEndpoints()
                {
                    AuthorizationEndpoint = Constants.DefaultQAAuthorizationEndpoint,
                    TokenEndpoint = Constants.DefaultQATokenEndpoint,
                    UserInfoEndpoint = Constants.DefaultQAUserInfoEndpoint,
                };
            }

            //SET LOCALE
            if(LocaleType == uaePassEnum.localeType.English)
            {
                Locale = "en";
            }
            else
            {
                Locale = "ar";
            }

            //SET SCOPES
            Scope = ScopeList;
            
        }
    }
}


