using Owin;
using System;
using System.Collections.Generic;


namespace UAEPassOAuthCodeLibrary
{
    public static class UAEPassAuthenticationExtensions
    {
        public static IAppBuilder UseUAEPassAuthentication(this IAppBuilder app,
            UAEPassAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if(options.Scope == null)
                throw new ArgumentNullException(nameof(options.Scope));

            app.Use(typeof(UAEPassAuthenticationMiddleware), app, options);

            return app;
        }

        public static IAppBuilder UseUAEPassAuthentication(this IAppBuilder app, string clientId, string clientSecret, uaePassEnum.authenticationType AuthenticationType, uaePassEnum.localeType LocaleType, List<string> ScopeList, string CallbackPath = "")
        {
            return app.UseUAEPassAuthentication(new UAEPassAuthenticationOptions(AuthenticationType,LocaleType,ScopeList,CallbackPath)
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            });
        }

    }
}
