using ArchiveManagement.BLL.Dtos;
using ArchiveManagement.BLL.Interfaces.BusinessDocuments;
using ArchiveManagement.DAL.Entities;
using ArchiveManagement.DAL.Implementations.BusinessDocuments;
using ArchiveManagement.DAL.Interfaces.BusinessDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Implementations.BusinessDocuments
{
    public class TypeBusinessDocumentservices: ITypeBusinessDocumentservices
    {

        private readonly ITypeBusinessDocumentDAL _typeBusinessDocumentsDAL;

        public TypeBusinessDocumentservices(ITypeBusinessDocumentDAL typeBusinessDocumentdal)
        {
            _typeBusinessDocumentsDAL = typeBusinessDocumentdal;
        }
        public IQueryable<TypeDocumetsBusiness> GetTypeBusinessDocuments()
        {
            return _typeBusinessDocumentsDAL.GetTypeBusinessDocuments();
        }

        public bool AddOrUpdateTypeBusiness(TypeDocumetsBusiness type)
        {
          return   _typeBusinessDocumentsDAL.AddOrUpdateTypeBusiness(type);
        }

        public bool DeleteTypeBusinessDocuments(int typeId)
        {
            return _typeBusinessDocumentsDAL.DeleteTypeBusinessDocuments(typeId);
        }
        public object  GetTypeBusinessDocumentsById(int id)
        {
            return _typeBusinessDocumentsDAL.GetTypeBusinessDocumentsById(id);

        }
    }
}
