using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Entities
{
    public  class BusinessDocuments
    {
        [Key]
        public string id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Date { get; set; }
        public double MtTtc { get; set; }
        public double MtHt { get; set; }
        public double MtDt { get; set; }
        public string Objet {  get; set; }
      
        
        [ForeignKey("TypeDocumetsBusiness")]
        public int idTypeBusiness { get; set; } 

        [ForeignKey("Tiers")]
        public int? idTiers { get; set; }
        [ForeignKey("Files")]
        public string idFiles { get; set; }

    }
}
