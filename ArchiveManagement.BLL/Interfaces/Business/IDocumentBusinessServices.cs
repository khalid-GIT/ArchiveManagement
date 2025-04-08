using ArchiveManagement.BLL.Dtos;
using ArchiveManagement.DAL.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Interfaces.Business
{
    public  interface IDocumentBusinessServices
    {
        Task<bool> UpdateDocuments(DocumentsDto updatedDoc);
        Task<List<DocumentBusiness>> GetDcumetsBusinessByIdPrentFolder(string id);
    }
}
