using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using CodeComb.AspNet.Localization.EntityFramework;
using Microsoft.Data.Entity;

namespace PipeCI.Website.Models
{
    public class PipeCIContext : LocalizationAndIdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e =>
            {
                e.HasIndex(x => x.Sex);
                e.HasIndex(x => x.RegisteryTime);
            });
        }
    }
}
