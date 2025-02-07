
using ArchiveManagement.BLL.Interfaces.IDocumentsVentesServices;
using ArchiveManagement.DAL.Context;
using ArchiveManagement.DAL.Entities;

namespace ArchiveManagement.BLL.Implementations.DocumentsVentesServices
{
    public  class DocumentsVentesServices: IDocumentsVentesServices
    {
        private readonly ArchivesDbContext _context;
       

        public DocumentsVentesServices(ArchivesDbContext archivesDbContext)
        {
            _context = archivesDbContext;
         

        }
        public bool SavePath(string id, string desc, string name, string idParent)
        {
            try
            {
                Files folderdto = new Files
                {
                    id = Guid.NewGuid().ToString(),
                    //FolderPath = path,
                    Name = name,
                    Description = name,
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
