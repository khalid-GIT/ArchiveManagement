using ArchiveManagement.BLL.Dtos;
using ArchiveManagement.BLL.Interfaces.Business;
using ArchiveManagement.DAL.Entities;
using ArchiveManagement.DAL.Entities.Business;
using ArchiveManagement.DAL.Implementations.Business;
using ArchiveManagement.DAL.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Implementations.Business
{
    public  class DocumentBusinessServices: IDocumentBusinessServices
    {
        private IDocumentBusinessDal _documentBusinessDal;

        public DocumentBusinessServices(IDocumentBusinessDal documentBusinessDal)
        {
            _documentBusinessDal = documentBusinessDal;


        }
        public Task<bool> UpdateDocuments(DocumentsDto updatedDoc)
        {
            //Mapper Dto to Entity
            var document = new DocumentBusiness
            {
                number = updatedDoc.number,
                date = updatedDoc.date,
                Tiersid = updatedDoc.Tiersid,
                Mht = updatedDoc.Mht,
                Mdt = updatedDoc.Mdt,
                Mttc = updatedDoc.Mttc

            };

            return _documentBusinessDal.UpdateDocuments(document);

        }
        public async Task<List<DocumentBusiness>> GetDcumetsBusinessByIdPrentFolder(string id)
        {
            return await _documentBusinessDal.GetDocumentBusinessByParent(id);

        }
    }
}
