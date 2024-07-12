using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ArchiveManagement.DAL.Entities
{
    public  class Tier
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        //public  Tiers()
        //{
        //    Name = string.Empty;
        //}
    }
}
