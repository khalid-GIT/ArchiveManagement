using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.DAL.Context;
using ArchiveManagement.DAL.Entities;
using ArchiveManagement.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Implementations
{
    public class Fileservices : IFileservices
    {
        private ArchivesDbContext _context;
        private IFolderServices _folderServices;
      
        private IFilesDal  _filesDal;

        public Fileservices(ArchivesDbContext archivesDbContext, IFolderServices folderServices, IFilesDal filesDal)
        {
            _context = archivesDbContext;
            _folderServices = folderServices;
            _filesDal = filesDal;
        }
        public bool SavePath(string id,  string name, string idParent)
        {
            try
            {
                Files folderdto = new Files
                {
                    id = Guid.NewGuid().ToString(),
                    //FolderPath = path,
                    //Name = name,
                  //  Description = name,
                   // TypeDocument= typeDocument,
                    idParent = idParent
                };
                _context.Add(folderdto);
                _context.SaveChanges();
                return false;
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            return true;
        }
        public async Task<List<Files>> GetFilsByIdPrentFolder(string id)
        {
            return await _filesDal.GetFilsByIdPrentFolder(id);
          
        }
        public async Task<bool> DeleteFile(string id)
        {
            return await _filesDal.DeleteFile(id);

        }
    }
}
