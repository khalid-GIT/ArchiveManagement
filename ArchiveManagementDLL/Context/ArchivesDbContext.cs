using ArchiveManagement.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Context
{
    public class ArchivesDbContext :IdentityDbContext
    {
        public ArchivesDbContext()
        {
        }

        public ArchivesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Tier> Tiers { get; set; }
        //public DbSet<User> aspnetusers { get; set; }

    }
}
