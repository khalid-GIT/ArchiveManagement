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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ArchiveManagement.DAL.Context
{
    public class ArchivesDbContext : IdentityDbContext<
    IdentityUser,
    IdentityRole,
    string,
    IdentityUserClaim<string>,
    IdentityUserRole<string>,
    IdentityUserLogin<string>,
    IdentityRoleClaim<string>,
    IdentityUserToken<string>>

    {
        protected UserManager<IdentityUser> _userManager;
        //public ArchivesDbContext()
        //{
        //}
     

        public ArchivesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Files> Files { get; set; }
        //public DbSet<TypeDocuments> TypeDocumetsBusiness { get; set; }
        public DbSet<DocumentBusiness> DocumentBusiness { get; set; }
        public DbSet<ModeReglement> ModeReglements { get; set; } 
        public DbSet<FamilleDocuments> FamilleDocuments { get; set; }
        public DbSet<ReglementsDocumentsBusiness> ReglementsDocumentsBusiness { get; set; }
        public DbSet<City> Citys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //est obligatoire pour qu'EF configure correctement les entités Identity par défaut.
            base.OnModelCreating(builder);
            //Asavoir sur heritage 
            // Et si tu veux deux tables séparées, il faut éviter le mode TPH(Table Per Hierarchy) et opter pour TPT(Table Per Type) ou composition.

            //TPH pour avoir les champs de la tables filles ds la table mere
            //builder.Entity<Files>()
            // .HasDiscriminator<string>("Discriminator")
            // .HasValue<Files>("Files")
            // .HasValue<DocumentBusiness>("DocumentBusiness");
            //TPT – Table Per Type (héritage avec tables séparées)  si on veut avoir deux tables separée 
            builder.Entity<Files>()
                    .ToTable("Files");

            builder.Entity<DocumentBusiness>()
                            .ToTable("DocumentBusiness");
            builder.Entity<ReglementsDocumentsBusiness>()
                           .ToTable("ReglementsDocumentsBusiness");
            
        }


    }
}
