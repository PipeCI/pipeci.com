﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PipeCI.Website.Models
{
    public enum Sex
    {
        Male,
        Female
    }

    public class User : IdentityUser<Guid>
    {
        public byte[] Avatar { get; set; }

        [MaxLength(32)]
        public string AvatarContentType { get; set; }

        public DateTime RegisteryTime { get; set; }

        [MaxLength(512)]
        public string Motto { get; set; }

        [MaxLength(128)]
        public string Organization { get; set; }

        [MaxLength(128)]
        public string WebSite { get; set; }

        public string ExperimentFlags { get; set; }

        public Sex Sex { get; set; }
    }
}
