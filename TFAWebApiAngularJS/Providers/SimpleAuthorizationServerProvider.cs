using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TFAWebApiAngularJS.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
        //the following method is responsible for validating the username and password sent to the authorization server token endpoint
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";
            ApplicationUser appUser = null;

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            using (AuthRepository _repo = new AuthRepository())
            {
                
                appUser = await _repo.FindUser(context.UserName, context.Password);

                if (appUser == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }
            //se trova l'utente mediante la sua userid e pwd...
            //aggiunge dei claims personalizzati
            //This claim will be included in the signed OAuth 2.0 bearer token (singifica che vengono criptati??
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            identity.AddClaim(new Claim("PSK", appUser.PSK));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "userName", context.UserName
                    }
                });
            //crea un ticket
            var ticket = new AuthenticationTicket(identity, props);
            //valida...
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}