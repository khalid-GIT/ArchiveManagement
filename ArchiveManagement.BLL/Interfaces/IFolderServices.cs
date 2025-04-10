using ArchiveManagement.BLL.Dtos;
using ArchiveManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Interfaces
{
    public interface IFolderServices
    {
        bool SavePath(string path, string name, string idParent,string idfamilleDocuments);
        string GetFolderPathById(string id);
        bool IfExistfolderByid(string id);
        string  GetIdFolderByName(string id);
        object GetAllFolder();
        List<Folder> GetAllFolderOfThisfolder(string id);
        Task<bool> UpdateFolderAsync(string id, UpdateFolderDto dto);
    }
}
