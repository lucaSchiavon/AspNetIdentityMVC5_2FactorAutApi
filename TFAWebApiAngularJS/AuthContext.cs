using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TFAWebApiAngularJS
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
        }
    }

    public class ApplicationUser : IdentityUser
    {
        //Preshared key generated for this user in our back-end API
        //la chiave univoca per ogni utente dell'applicativo da fornire la prima volta a google autenticator
        //di solito si rappresenta sotto forma di QR code
        [Required]
        [MaxLength(16)]
        public string PSK { get; set; }
    }
}