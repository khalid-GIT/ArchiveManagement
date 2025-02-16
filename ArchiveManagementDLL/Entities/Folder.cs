﻿using ArchiveManagement.DAL.Entities.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Entities
{
    public class Folder
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
       
        public DateTime? CreatedOn { get; set; }
        
        public string FolderPath { get; set; }

        [ForeignKey("Folder")]
        public string? idParent { get; set; }

        [ForeignKey("[FamilleDocuments]")]
        public string? idfamilleDocuments { get; set; }
        public FamilleDocuments FamilleDocuments { get; set; }
    }
}
