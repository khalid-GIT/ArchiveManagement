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
        bool SavePath(string path, string name, string idParent,string typeDocument);
        string GetFolderPathById(string id);
        bool IfExistfolderByid(string id);
        string  GetIdFolderByName(string id);
        List<Folder> GetAllFolder();
        List<Folder> GetAllFolderOfThisfolder(string id);
    }
}
