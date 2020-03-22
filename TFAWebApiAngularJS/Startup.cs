using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TFAWebApiAngularJS.Providers;

namespace TFAWebApiAngularJS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            //qui chiama la classe webapiconfig
            WebApiConfig.Register(config);
            // un'applicazione web che utilizza queste API può solamente richiedere risorse HTTP dalla stessa origine di caricamento 
            //dell'applicazione, a meno che la risposta dall'altra origine includa i corretti header CORS.
            //ad es se l'applicazione client ha dominio www.pippo.com potrà fare richieste solo al suo dominio a meno che non sia abilitato cors
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //qui configuriamo la applicazione back-end per utilizzare l'OAuth 2.0 bearer tokens
            //per rendere sicure le action maracte con authorize
            OAuthBearerAuthenticationOptions OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            //per la generazione del token
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            //Token Consumption
            //per l'uso del token
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

        }
    }
}