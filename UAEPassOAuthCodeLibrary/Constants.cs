using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAEPassOAuthCodeLibrary
{
    internal static class Constants
    {
        public const string DefaultAuthenticationType = "QA - UAEPass";

        //STAGING END POINTS
        public const string DefaultQAAuthorizationEndpoint = "https://qa-id.uaepass.ae/trustedx-authserver/oauth/main-as";
        public const string DefaultQATokenEndpoint = "https://qa-id.uaepass.ae/trustedx-authserver/oauth/main-as/token";
        public const string DefaultQAUserInfoEndpoint = "https://qa-id.uaepass.ae/trustedx-resources/openid/v1/users/me";

        //PRODUCTION END POINTS
        public const string DefaultProductionAuthorizationEndpoint = "https://id.uaepass.ae/trustedx-authserver/oauth/main-as";
        public const string DefaultProductionTokenEndpoint = "https://id.uaepass.ae/trustedx-authserver/oauth/main-as/token";
        public const string DefaultProductionUserInfoEndpoint = "https://id.uaepass.ae/trustedx-resources/openid/v1/users/me";

        //SUPPORTED LOCALES - PROVIDING A LOCALE IS OPTIONAL
        public const string UILocalesEN = "en";
        public const string UILocalesAR = "ar";

        //RESPONSE TYPE
        public const string DefaultResponseType = "code";

        //DEFAULT ACR VALUE - THIS IS OPTIONAL
        public const string DefaultACRValue = "urn:safelayer:tws:policies:authentication:level:low";

    }
}
