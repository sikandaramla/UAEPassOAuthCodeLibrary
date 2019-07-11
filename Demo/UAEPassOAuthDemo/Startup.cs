using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Owin;
using Owin;
using UAEPassOAuthCodeLibrary;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using UAEPassOAuthDemo.Models;
using System.Net;

[assembly: OwinStartupAttribute(typeof(UAEPassOAuthDemo.Startup))]
namespace UAEPassOAuthDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //SCOPE LIST - YOU CAN ALTER/CHANGE SCOPE LIST AS PER YOUR REQUIREMENT
            List<string> scopeList = new List<string>
            {
                "urn:uae:digitalid:profile" //REQUEST PROFILE
            };


            /*
             * NOTE: AT THE TIME OF THIS WRITING, STAGING URLs ARE NOT SUPPORTED FROM ANYWHERE BUT UAE. 
             * HENCE YOU MUST TEST IT FROM UAE OR GET A VPN THAT SUPPORTS UAE SERVER
             * IN MY CASE, I HAD TO REQUEST MY CLIENTS TO ARRANGE FOR A PC WITH VS AND REMOTELY WORKED ON IT
             */

            //CONFIGURE WEB STAGING/PRODUCTION
            //SELECT UI LANGUAGE
            //CONFIGURE RETURN URL
            var options = new UAEPassAuthenticationOptions(uaePassEnum.authenticationType.Staging, uaePassEnum.localeType.English, scopeList, "/account/uaepass")
            {
                ClientId = "your-client-id",
                ClientSecret = "your-client-secret",
                Provider = new UAEPassAuthenticationProvider()
                {
                    OnAuthenticated = async z =>
                    {
                        await Task.Delay(1);
                        
                        //STORING USER'S DETAILS IN THE GLOBAL VARIABLE SO WE CAN ACCESS IT ACROSS THE APP
                        MyGlobalVariables.GlobalUser = new UAEPassUser();
                        MyGlobalVariables.GlobalUser = z.UserInfo;
                        MyGlobalVariables.RawResponse = z.RawResponse;
                    }
                }
            };

            app.UseUAEPassAuthentication(options);
        }
    }
}