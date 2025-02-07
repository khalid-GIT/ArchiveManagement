using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;
using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.BLL.Implementations;
using Microsoft.AspNetCore.Authorization;


namespace ArchiveManagement.WEBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileservices _fileservices;
        private readonly IFolderServices _folderServices;

        public FileUploadController(IFileservices fileservices, IFolderServices folderServices)
        {
            _fileservices = fileservices;
            _folderServices = folderServices;
        }

        [HttpPost("upload")]
        [AllowAnonymous]
        //public async Task<IActionResult> Upload(IFormFile file, string _path,string descr,string idparent)
         public async Task<IActionResult> Upload(IFormFile file,  string descr, string idparent,string typeDocument)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            //extrair l'extention 
            string extension = Path.GetExtension(file.FileName);
            //donner un nouveau nom avec guid
            var idFile= Guid.NewGuid().ToString(); 
            var fileName=Path.GetFileName(file.FileName);
            //get folder by id
            string pathparent = _folderServices.GetFolderPathById(idparent);
            var found = fileName.IndexOf(".");
            fileName = fileName.Substring(0, found);
            var filePath = @pathparent + "\\"+ idFile + extension; // Path.GetTempFileName();
            //https://dotnettutorials.net/lesson/filestream-class-in-csharp/
            using (var stream = new FileStream(filePath , FileMode.Create,FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }
            //SAVE PATH FILES
            var resultsave = _fileservices.SavePath(idFile, fileName,descr, idparent, typeDocument);
            return Ok(new { filePath });
        }
    }
}
