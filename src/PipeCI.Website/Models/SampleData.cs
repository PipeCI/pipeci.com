using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.PlatformAbstractions;

namespace PipeCI.Website.Models
{
    public static class SampleData
    {
        public static async Task InitDB(IServiceProvider services)
        {
            var DB = services.GetRequiredService<PipeCIContext>();
            var UserManager = services.GetRequiredService<UserManager<User>>();
            var RoleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var env = services.GetRequiredService<IApplicationEnvironment>();

            if (DB.Database.EnsureCreated())
            {
                await RoleManager.CreateAsync(new IdentityRole<Guid> { Name = "Root" });
                await RoleManager.CreateAsync(new IdentityRole<Guid> { Name = "Master" });
                await RoleManager.CreateAsync(new IdentityRole<Guid> { Name = "Enterprise" });
                await RoleManager.CreateAsync(new IdentityRole<Guid> { Name = "Member" });
                await RoleManager.CreateAsync(new IdentityRole<Guid> { Name = "Banned" });

                var user1 = new User
                {
                    UserName = "雨宫丶优子",
                    Email = "1@1234.sh",
                    Organization = "Code Comb Co., Ltd.",
                    WebSite = "http://1234.sh",
                    RegisteryTime = DateTime.Now,
                    Sex = Sex.Female
                };
                await UserManager.CreateAsync(user1, "123456");
                await UserManager.AddToRoleAsync(user1, "Root");
                
                var user2 = new User
                {
                    UserName = "wph95",
                    Email = "2636157749@qq.com",
                    Organization = "HDU",
                    WebSite = "http://www.wph95.com",
                    RegisteryTime = DateTime.Now,
                    Sex = Sex.Male
                };
                await UserManager.CreateAsync(user2, "123456");
                await UserManager.AddToRoleAsync(user2, "Root");
            }
        }
    }
}
