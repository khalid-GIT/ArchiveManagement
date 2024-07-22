using ArchiveManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveManagement.DAL.Interfaces;
using ArchiveManagement.DAL.Interfaces.BusinessDocuments;
using ArchiveManagement.BLL.Dtos;

namespace ArchiveManagement.BLL.Interfaces.BusinessDocuments
{
    public  interface ITypeBusinessDocumentservices
    {
        bool AddOrUpdateTypeBusiness(TypeDocumetsBusiness type);
        public IQueryable<TypeDocumetsBusiness> GetTypeBusinessDocuments();
        bool DeleteTypeBusinessDocuments(int id);
        object GetTypeBusinessDocumentsById(int id);
    }
}
