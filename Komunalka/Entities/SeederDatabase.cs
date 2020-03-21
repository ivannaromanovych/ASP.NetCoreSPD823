using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Entities
{
    public class SeederDatabase
    {
        public static void SeedData(IServiceProvider services, 
            IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                //var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                string [] names = { "Газ", "Світло", "Вода" };
                string [] units = { "Кб", "Кв", "Кб" };
                for (int i = 0; i < names.Length; i++)
                {
                    if (context.Resources.SingleOrDefault(r => r.Name == names[i]) == null)
                    {
                        Resource resource = new Resource
                        {
                            Name = names[i],
                            Units = units[i]
                        };
                        context.Resources.Add(resource);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
