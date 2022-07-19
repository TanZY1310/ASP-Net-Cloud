using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DDACAssignment.Data;
using DDACAssignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DDACAssignment.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DDACAssignmentContext(
            serviceProvider.GetRequiredService<DbContextOptions<DDACAssignmentContext>>()))
            {
                // Look for any users. 
                if (context.Users.Any())
                {
                    return;
                }
                else 
                {
                    var user= new DDACAssignmentUser();
                    var hasher = new PasswordHasher<DDACAssignmentUser>();
                    var users = new List<DDACAssignmentUser>() {
                    new DDACAssignmentUser{
                            UserName="Admin@gmail.com",
                            NormalizedUserName="Admin@gmail.com",
                            Name="Admin",
                            Email="Admin@gmail.com",
                            NormalizedEmail="Admin@gmail.com",
                            PasswordHash = hasher.HashPassword(user, "Admin12345()"),
                            Role="Admin",
                            EmailConfirmed=true,
                            LockoutEnabled = false,
                            SecurityStamp = Guid.NewGuid().ToString()}
                    };


                    context.AddRange(users);
                    context.SaveChanges();

                }

               



            }
        }
    }
}
