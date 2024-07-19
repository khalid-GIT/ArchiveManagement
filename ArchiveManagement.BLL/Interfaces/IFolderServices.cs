using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Interfaces
{
    public interface IFolderServices
    {
        bool SavePath(string path, string name, string idParent);
        string GetFolderById(string id);
        bool IfExistfolderByid(string id);
        string  GetIdFolderByName(string id);
    }
}
