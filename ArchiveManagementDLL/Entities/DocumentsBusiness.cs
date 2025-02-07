using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Entities
{
    public class DocumentsBusiness
    {
        [Key]
        public string id { get; set; }
        public string LastUpdate { get; set; }
    }
}
