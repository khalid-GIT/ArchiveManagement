using ArchiveManagement.DAL.Context;
using ArchiveManagement.DAL.Entities;
using ArchiveManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArchiveManagement.DAL.Implementations
{
    public class FolderDal : IFolderDal
    {
        private ArchivesDbContext _context;

        public FolderDal(ArchivesDbContext context)
        {
            _context = context;
        }
      
        public string GetFolderPathById(string id)
        {
            var folder = _context.Folders.Where(c => c.id == id).FirstOrDefault();
            return folder.FolderPath;
        }
        public bool IfExistfolderByid(string id)
        {
            var folder = _context.Folders.Where(c => c.id == id).FirstOrDefault();
            if (folder != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Folder> FolderByid(string id)
        {
            var folder = _context.Folders.Where(c => c.id == id).FirstOrDefault();
            return folder;
        }
        public async Task UpdateAsync(Folder folder)
        {
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
        }
        public string GetIdFolderByName(string name)
        {
            var folder = _context.Folders.Where(c => c.Name == name).FirstOrDefault();
            if (folder != null)
            {
                return folder.id;
            }
            return null;
        }
        public async Task<bool> CreatFolder(Folder folder_)
        {

            var folder = new Folder
            {
                id = Guid.NewGuid().ToString(),
                Name = folder_.Name,
                idParent = folder_.idParent,
                CreatedOn = DateTime.Now
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return true;
        }
        public List<Folder> GetAllFolder()
        {
            if (_context.Folders == null)
            {
                // Return an empty list if Folders is null
                return new List<Folder>();
            }

            var folders = _context.Folders.ToList();
            return folders;

        }  
        public List<Folder> GetAllFolderOfThisfolder(string id)
        {
            var folder = _context.Folders.Where(x=> x.idParent == id).ToList();
            return folder;

        }
    }
}
