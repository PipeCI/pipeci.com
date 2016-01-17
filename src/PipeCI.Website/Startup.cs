using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PipeCI.Website.Models;

namespace PipeCI.Website
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            IConfiguration Config;
            services.AddConfiguration(out Config);
            #region Add Entity Framework v7
            if (Config["Data:DefaultConnection:Type"] == "PostgreSQL")
            {
                services.AddEntityFramework()
                    .AddNpgsql()
                    .AddDbContext<PipeCIContext>(x => x.UseNpgsql(Config["Data:DefaultConnection:ConnectionString"]));
            }
            else if (Config["Data:DefaultConnection:Type"] == "SQLServer")
            {
                services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<PipeCIContext>(x => x.UseSqlServer(Config["Data:DefaultConnection:ConnectionString"]));
            }
            else if (Config["Data:DefaultConnection:Type"] == "SQLite")
            {
                services.AddEntityFramework()
                    .AddSqlite()
                    .AddDbContext<PipeCIContext>(x => x.UseSqlite(Config["Data:DefaultConnection:ConnectionString"]));
            }
            else
            {
                services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<PipeCIContext>(x => x.UseInMemoryDatabase());
            }
            #endregion
            #region Add Identity v3
            services.AddIdentity<User, IdentityRole<Guid>>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 0;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonLetterOrDigit = false;
                x.Password.RequireUppercase = false;
                x.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<PipeCIContext, Guid>()
                .AddDefaultTokenProviders();
            #endregion
            #region Add Localization
            services.AddEFLocalization<PipeCIContext, Guid>()
                .AddCookieCulture();
            #endregion
            #region Add Mvc
            services.AddMvc();
            #endregion
            #region Add Extensions
            services.AddSmartCookies();
            services.AddSmartUser<User, Guid>();
            services.AddSmtpEmailSender("smtp.ym.163.com", 25, "vNext China", "noreply@vnextcn.org", "noreply@vnextcn.org", "123456");
            services.AddAntiXss();
            #endregion
            #region Add SignalR v3
            services.AddSignalR();
            #endregion
            #region Add PipeCI.Blob
            services.AddBlob()
                .AddEntityFrameworkStorage<PipeCIContext>()
                .AddSignedUserBlobUploadAuthorization();
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseSignalR();
            app.UseIdentity();
            app.UseBlob();
            app.UseAutoAjax("/scripts/shared/jquery.autoajax.js");
            app.UseJavascriptLocalization("/scripts/shared/localization.js");
            app.UseMvcWithDefaultRoute();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
