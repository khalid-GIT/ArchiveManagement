using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.BLL.Interfaces.IDocumentsVentesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveManagement.WEBAPI.Controllers.DocumentsVentes
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentsVentesController : ControllerBase
    {
        private readonly IDocumentsVentesServices _dcumentsVentesServices;
        private readonly IFolderServices _folderServices;

        public DocumentsVentesController( IDocumentsVentesServices documentsVentesServices, IFolderServices folderServices)
        {
            _dcumentsVentesServices = documentsVentesServices;
            _folderServices = folderServices;
        }

        [HttpPost("DocumentsVentesUpload")]
        [AllowAnonymous]
        //public async Task<IActionResult> Upload(IFormFile file, string _path,string descr,string idparent)
        public async Task<IActionResult> Upload(IFormFile file, string descr, string idparent)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            //extrair l'extention 
            string extension = Path.GetExtension(file.FileName);
            //donner un nouveau nom avec guid
            var idFile = Guid.NewGuid().ToString();
            var fileName = Path.GetFileName(file.FileName);
            //get folder by id
            string pathparent = _folderServices.GetFolderPathById(idparent);
            var found = fileName.IndexOf(".");
            fileName = fileName.Substring(0, found);
            var filePath = @pathparent + "\\" + idFile + extension; // Path.GetTempFileName();
            //https://dotnettutorials.net/lesson/filestream-class-in-csharp/
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }
            //SAVE PATH FILES
        //    var resultsave = _dcumentsVentesServices.SavePath(idFile, fileName, descr, idparent);
            return Ok(new { filePath });

        }
    }
}
