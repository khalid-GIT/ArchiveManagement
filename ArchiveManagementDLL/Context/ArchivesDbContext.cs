using ArchiveManagement.DAL.Entities;
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
        public ArchivesDbContext()
        {
        }

        public ArchivesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Tier> Tiers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Tier>();
            this.Seedroles(builder);


        }
        private void Seedroles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
               new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = "1" },
               new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "User", ConcurrencyStamp = "2" },
               new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "RH", NormalizedName = "RH", ConcurrencyStamp = "3" },
                new IdentityRole() {Name = "Guist", NormalizedName = "Guist", ConcurrencyStamp = "4" }
            );
        }

        public void CreateUsers()
        {
            

        var _identityUser =  _userManager.FindByEmailAsync("khalid.elkettani@gmail.com");


            if (_identityUser == null)

            {
               IdentityUser _user = new IdentityUser
                {
                    Email = "khalid.elkettani@gmail.com",
                    UserName = "khalid.elkettani@gmail.com"
               };
                  _userManager.CreateAsync(_user, "User@123");
            }
           var  result = _userManager.FindByEmailAsync("khalid.elkettani@gmail.com");
            //IdentityUser _user_ = new IdentityUser();
            //if (result!=null)
            //{
            //    _userManager.AddToRoleAsync (_user_.Id, "Admin");
            //}
        }

    }
}
