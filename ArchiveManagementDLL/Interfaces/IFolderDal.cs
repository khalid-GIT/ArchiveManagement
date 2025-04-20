using ArchiveManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.DAL.Interfaces
{

    public  interface IFolderDal
    {
        string GetFolderPathById(string id);
        string GetIdFolderByName(string name);
        bool IfExistfolderByid(string id);
        List<Folder>  GetAllFolder();
        List<Folder> GetAllFolderOfThisfolder(string id);
        Task<Folder> FolderByid(string id);
        Task UpdateAsync(Folder folder);
        Task<bool> CreatFolder(Folder folder_);
    }
}
