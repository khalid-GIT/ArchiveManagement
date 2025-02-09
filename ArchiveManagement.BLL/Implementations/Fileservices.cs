using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.DAL.Context;
using ArchiveManagement.DAL.Entities;

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

        public Fileservices(ArchivesDbContext archivesDbContext, IFolderServices folderServices)
        {
            _context = archivesDbContext;
            _folderServices = folderServices;
        }
        public bool SavePath(string id, string desc, string name, string idParent,string typeDocument)
        {
            try
            {
                Files folderdto = new Files
                {
                    id = Guid.NewGuid().ToString(),
                    //FolderPath = path,
                    Name = name,
                    Description = name,
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
    }
}
