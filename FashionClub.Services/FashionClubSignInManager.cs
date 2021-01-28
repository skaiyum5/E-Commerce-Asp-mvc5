using FashionClub.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Services
{
    public class FashionClubSignInManager : SignInManager<FashionClubUser, string>
    {
        public FashionClubSignInManager(FashionClubUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(FashionClubUser user)
        {
            return user.GenerateUserIdentityAsync((FashionClubUserManager)UserManager);
        }

        public static FashionClubSignInManager Create(IdentityFactoryOptions<FashionClubSignInManager> options, IOwinContext context)
        {
            return new FashionClubSignInManager(context.GetUserManager<FashionClubUserManager>(), context.Authentication);
        }
    }
}
