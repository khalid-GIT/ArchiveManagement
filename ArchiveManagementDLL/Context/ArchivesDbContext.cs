using ArchiveManagement.DAL.Entities;
using ArchiveManagement.DAL.Entities.Business;
using ArchiveManagement.DAL.Entities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArchiveManagement.DAL.Context
{
    public class ArchivesDbContext : IdentityDbContext<IdentityUser>
    {
        protected UserManager<IdentityUser> _userManager;
        //public ArchivesDbContext()
        //{
        //}

        public ArchivesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<TypeDocuments> TypeDocumetsBusiness { get; set; }
        public DbSet<DocumentBusiness> DocumentBusiness { get; set; }
        public DbSet<ModeReglement> ModeReglements { get; set; } 
        public DbSet<FamilleDocuments> FamilleDocuments { get; set; }
        public DbSet<ReglementsDocumentsBusiness> ReglementsDocumentsBusiness { get; set; }
        public DbSet<City> Citys { get; set; }
        
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Tier>();
        //    builder.Entity<Folder>();
        //    //  this.Seedroles(builder);


        //}


    }
}
