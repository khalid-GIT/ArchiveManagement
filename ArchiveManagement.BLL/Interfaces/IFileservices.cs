using ArchiveManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManagement.BLL.Interfaces
{
    public interface IFileservices
    {
        bool SavePath(string id, string name, string idParent);
         Task<List<Files>> GetFilsByIdPrentFolder(string id);


    }
}
