using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAEPassOAuthCodeLibrary
{
    public class UAEPassAuthenticationProvider : IUAEPassAuthenticationProvider
    {
        public UAEPassAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
        }

        /// <summary>
        /// Gets or sets the function that is invoked when the Authenticated method is invoked.
        /// </summary>
        public Func<UAEPassAuthenticatedContext, Task> OnAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the function that is invoked when the ReturnEndpoint method is invoked.
        /// </summary>
        public Func<UAEPassReturnEndpointContext, Task> OnReturnEndpoint
        {
            get; set;
        }

        public Func<UAEPassAuthenticationOptions, Task> PostAuthenticatedRedirect { get; set; }


        public virtual Task Authenticated(UAEPassAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        
        

        public virtual Task ReturnEndpoint(UAEPassReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
            
        }
    }
}
