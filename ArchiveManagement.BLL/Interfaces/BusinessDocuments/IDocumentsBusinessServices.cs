using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveManagement.DAL.Entities;

namespace ArchiveManagement.BLL.Interfaces.BusinessDocuments
{
    public  interface IDocumentsBusinessServices
    {
        IQueryable<DocumentsBusiness>  GetBusinessDocuments();
    }
}
