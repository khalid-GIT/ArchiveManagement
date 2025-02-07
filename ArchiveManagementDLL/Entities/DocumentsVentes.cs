using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Entities
{
    public  class DocumentsVentes
    {
        [Key]
        public string id { get; set; }
        public string number {  get; set; }
        public DateTime date { get; set; }
        public Double Mht {  get; set; }
        public Double Mdt {  get; set; }
        public Double Mttc {  get; set; }

        [ForeignKey("Folder")]
        public string idCustomer {  get; set; }

        public string LastUpdate { get; set; }
        [ForeignKey("Folder")]
        public string? idParent { get; set; }

    }
}
