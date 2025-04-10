using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveManagement.DAL.Entities;
using ArchiveManagement.BLL.Dtos;
using System.Xml.Linq;
using ArchiveManagement.DAL.Interfaces;
using ArchiveManagement.DAL.Implementations;

namespace ArchiveManagement.BLL.Implementations
{
    public class FolderServices : IFolderServices
    {
        private ArchivesDbContext _context;
        private IFolderDal _folderDal;


        public FolderServices(ArchivesDbContext archivesDbContext, IFolderDal folderDal)
        {
            _context = archivesDbContext;
            _folderDal = folderDal;
        }
        public bool SavePath(string path, string name, string idParent, string idfamilleDocuments)
        {
            try
            {
                //FolderDto folderdto = new FolderDto();
                //folderdto = new FolderDto
                //{
                //    id = Guid.NewGuid().ToString(),
                //    FolderPath=path,
                //    Name= name
                //};

                Folder folderdto = new Folder
                {
                    id = Guid.NewGuid().ToString(),
                    FolderPath = path,
                    Name = name,
                    Description = name,
                    FamilleDocumentsid = idfamilleDocuments,
                    idParent = idParent
                };

                _context.Add(folderdto);
                _context.SaveChanges();

                return true;
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
                return false;
            }

        }

        public string GetFolderPathById(string idParent)
        {
            return _folderDal.GetFolderPathById(idParent);
        }
        public bool IfExistfolderByid(string id)
        {
            return _folderDal.IfExistfolderByid(id);

        }
        public async Task<bool> UpdateFolderAsync(string id, UpdateFolderDto dto)
        {
            var folder = await _folderDal.FolderByid(id);
            if (folder == null) return false;

            folder.Name = dto.Name;
            await _folderDal.UpdateAsync(folder);
            return true;
        }
        public string GetIdFolderByName(string name)
        {

            return _folderDal.GetIdFolderByName(name);
        }
        public object GetAllFolder()
        {
            
            return _folderDal.GetAllFolder();

        } public List<Folder> GetAllFolderOfThisfolder(string id)
        {
            
            return _folderDal.GetAllFolderOfThisfolder( id);

        }
    }
}
