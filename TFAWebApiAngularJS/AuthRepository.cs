using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TFAWebApiAngularJS.Models;
using TFAWebApiAngularJS.Services;

namespace TFAWebApiAngularJS
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            //lo userstore si occupa di salvare l'utente i ruoli ed altro mediante entityframework, il dbcontext infatti va passato
            //se vogliamo implementare in altro modo la persistenza dei dati (senza ad es entity framework, basta che creiamo un'altra classe userstore che erediti
            //l'interfaccia iuserstore

            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            //le proprietà diefinite qui vengono recuperate con il claim di principal
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                TwoFactorEnabled = true,
               // PSK = OneTimePass.GenerateSharedPrivateKey()
                  PSK = TimeSensitivePassCode.GeneratePresharedKey()
                
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}