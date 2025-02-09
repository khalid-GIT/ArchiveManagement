using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Entities
{
    public  class Files
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
       
        public DateTime? CreatedOn { get; set; }
        public string LastUpdate { get; set; }
        [ForeignKey("Folder")]
        public string? idParent { get; set; }

        // Clé étrangère
        [ForeignKey("TypeDocuments")]
        public int TypeDocumentid { get; set; }
        public TypeDocuments TypeDocuments { get; set; }
    }
}
