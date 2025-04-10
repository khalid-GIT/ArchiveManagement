using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Dtos
{
    public class CreateFolderDto
    {
        public string FolderName { get; set; }
        public string? ParentFolderId { get; set; }
    }
}
