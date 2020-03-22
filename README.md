# AspNetIdentityMVC5_2FactorAutApi
Come implementare la 2factor autentication con google autenticator con JWT (no cookies)
La documentazione è disponibile online qui:
https://bitoftech.net/2014/10/15/two-factor-authentication-asp-net-web-api-angularjs-google-authenticator/
nella root del progetto è presente un documento che spiega in postman come lanciare in sequenza le chiamate per:
registrarsi ed ottenere la password applicazione
ottenere il token
chiamare una action coperta solo da JWT
chiamare una action coperta da JWT e 2FA
