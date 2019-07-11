
//using Microsoft.Owin;

//using Microsoft.Owin.Logging;
//using Microsoft.Owin.Security.Infrastructure;
using Owin;
//using System;
//using System.Net.Http;


//using System.Globalization;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.DataHandler;
//using Microsoft.Owin.Security.DataProtection;


using System;
using System.Globalization;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;


namespace UAEPassOAuthCodeLibrary
{
    public class UAEPassAuthenticationMiddleware : AuthenticationMiddleware<UAEPassAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public UAEPassAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            UAEPassAuthenticationOptions options)
            : base(next, options)
        {
            if (string.IsNullOrWhiteSpace(Options.ClientId))
                throw new ArgumentException("ClientId must be provided.");
            if (string.IsNullOrWhiteSpace(Options.ClientSecret))
                throw new ArgumentException("ClientSecret must be provided.");

            _logger = app.CreateLogger<UAEPassAuthenticationMiddleware>();

            if (Options.Provider == null)
                Options.Provider = new UAEPassAuthenticationProvider();

            if (Options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(UAEPassAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v2");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }

            if (string.IsNullOrEmpty(Options.SignInAsAuthenticationType))
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();

            
            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options))
            {
                Timeout = Options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024 * 1024 * 10,
            };
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft Owin UAEPass middleware");
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            

            
        }

        /// <summary>
        ///     Provides the <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> object for processing
        ///     authentication-related requests.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:Microsoft.Owin.Security.Infrastructure.AuthenticationHandler" /> configured with the
        ///     <see cref="T:Owin.Security.Providers.UAEPass.UAEPassAuthenticationOptions" /> supplied to the constructor.
        /// </returns>
        protected override AuthenticationHandler<UAEPassAuthenticationOptions> CreateHandler()
        {
            return new UAEPassAuthenticationHandler(_httpClient, _logger);
        }


        private static HttpMessageHandler ResolveHttpMessageHandler(UAEPassAuthenticationOptions options)

        {
            var handler = options.BackchannelHttpHandler ?? new WebRequestHandler();

            // If they provided a validator, apply it or fail.
            if (options.BackchannelCertificateValidator == null) return handler;

            // Set the cert validate callback
            var webRequestHandler = handler as WebRequestHandler;
            if (webRequestHandler == null)
            {
                throw new InvalidOperationException("Validator mismatch.");
            }
            webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;

            return handler;
        }

    }
}
