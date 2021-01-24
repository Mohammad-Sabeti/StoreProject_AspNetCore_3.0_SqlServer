using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyStore_Core3
{
    public class SeedData
    {
        public static void Seed(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("MyAdmin").Result==null)
            {
                var user = new IdentityUser()
                {
                    UserName = "MyAdmin",
                    Email = "admin123@gmail.com"
                };
                var result = userManager.CreateAsync(user,"MyAdmin@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result==null)
            {
                var user = new IdentityUser()
                {
                    UserName = "user1",
                    Email = "user1@gmail.com"
                };
                var result = userManager.CreateAsync(user, "User1@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
        public static void SeedRoles(
            RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                   var role=new IdentityRole()
                   {
                       Name = "Administrator"
                   };
                   var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "User"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Creator").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "Creator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
