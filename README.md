## UAE Pass Authentication (OAuth2) using OWIN 

Refer to the [sample project here](https://github.com/sikandaramla/UAEPassOAuthCodeLibrary/blob/master/Demo/UAEPassOAuthDemo/Startup.cs).

***
Download [Nuget project here](https://www.nuget.org/packages/UAEPassOAuth/).

***
## Supporting the project
If you use UAEPassOAuth Library, please consider buying me a cup of coffee

<a href='https://ko-fi.com/sikandaramla' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://cdn.ko-fi.com/cdn/kofi2.png?v=3' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>

### Setup

Add the following code to your ConfigureAuth method on the *Startup.cs*
            
```csharp
            //Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
```
Add your scope list like following below above code.
```csharp
            //SCOPE LIST - YOU CAN ALTER/CHANGE SCOPE LIST AS PER YOUR REQUIREMENT
            List<string> scopeList = new List<string>
            {
                "urn:uae:digitalid:profile" //REQUEST PROFILE
            };
```
Fianlly, initialize and configure Authentication options using following code.
```csharp
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
```
Create global variables to hold user details
```csharp
public static class MyGlobalVariables
    {
        public static UAEPassOAuthCodeLibrary.UAEPassUser GlobalUser;
        public static string RawResponse;
    }
```
For other details, you can follow sample project or write to me at sikandar.amla@gmail.com

> _**Note for Staging Environment**_ _At the time of this writing, staging is supported only within UAE so if you are outside and need to develop, you either need a VPN with UAE server or a remote computer setup in a UAE office._

