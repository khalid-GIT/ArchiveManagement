using ArchiveManagement.BLL.Interfaces.BusinessDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveManagement.DAL.Interfaces.BusinessDocuments;
using ArchiveManagement.DAL.Implementations.BusinessDocuments;
using ArchiveManagement.DAL.Entities;

namespace ArchiveManagement.BLL.Implementations.BusinessDocuments
{
    public  class DocumentsBusinessServices: IDocumentsBusinessServices
    {
        private readonly IDocumentsBusinessDAL _documentsBusinessDAL;
             public DocumentsBusinessServices(IDocumentsBusinessDAL documentsBusinessDAL)
        {
            _documentsBusinessDAL = documentsBusinessDAL;
        }
        public IQueryable<DocumentsBusiness>  GetBusinessDocuments()
        {
            return _documentsBusinessDAL.GetDocumentsBusiness();

        }
    }
}
