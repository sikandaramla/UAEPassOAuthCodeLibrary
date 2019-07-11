using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UAEPassOAuthCodeLibrary
{
    public class UAEPassAuthenticatedContext : BaseContext
    {
        public UAEPassAuthenticatedContext(IOwinContext context, UAEPassUser user, string accessToken, string rawResponse) : base(context)
        {
            AccessToken = accessToken;

            //CHECK IF Idn IS PROVIDED BY THE SERVER, IF NOT, USE PASSPORT AS THE CONTEXT ID
              
            Id = user.uuid;
            Name = user.fullnameEN;
            UserName = user.uuid;
            Email = user.email;
            UserInfo = user;
            RawResponse = rawResponse;
        }

        /// <summary>
        /// Gets the UAEPass Online access token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the UAEPass uuid
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the user's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the user's email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the UAEPass Online username
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }

        /// <summary>
        /// STRONGLY TYPED USER PROFILE WHICH WILL BE RECEIVED POST AUTHENTICATION
        /// </summary>
        public UAEPassUser UserInfo { get; set; }

        /// <summary>
        /// JSON RESPONSE WHICH WILL BE RECEIVED POST AUTHENTICATION
        /// </summary>
        public string RawResponse { get; set; }
    }
}
