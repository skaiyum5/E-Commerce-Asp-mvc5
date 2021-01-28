using FashionClub.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Data
{
    public class FashionClubInitializer : CreateDatabaseIfNotExists<FashionClubDBContext>
    {
        protected override void Seed(FashionClubDBContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
        }

       
        private void SeedRoles(FashionClubDBContext context)
        {
            List<IdentityRole> rolesInFashionClub = new List<IdentityRole>();
            rolesInFashionClub.Add(new IdentityRole { Name = "Administrator" });
            rolesInFashionClub.Add(new IdentityRole { Name = "Moderator" });

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore);

            foreach (var role in rolesInFashionClub)
            {
                if (!rolesManager.RoleExists(role.Name))
                {
                    var result = rolesManager.Create(role);
                    if (result.Succeeded) continue;
                }
            }
        }
        private void SeedUsers(FashionClubDBContext context)
        {
            var usersStore = new UserStore<FashionClubUser>(context);
            var usersManager = new UserManager<FashionClubUser>(usersStore);

            FashionClubUser admin = new FashionClubUser();
            admin.Email = "admin@email.com";
            admin.UserName = "admin";
            var password = "admin123";
            if (usersManager.FindByEmail(admin.Email)==null)
            {
                var result = usersManager.Create(admin, password);
                if (result.Succeeded)
                {
                    usersManager.AddToRole(admin.Id, "Administrator");
                }
            }
        }
    }
}
