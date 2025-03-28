using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.IO;
using System;

using System.Threading.Tasks;
using Google.Protobuf;
using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.BLL.Implementations;
using Microsoft.AspNetCore.Authorization;
using ArchiveManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;


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
         //public async Task<IActionResult> Upload(IFormFile file,  string descr, string idparent, string typeDocumetsBusiness)
         public async Task<IActionResult> Upload(IFormFile file, [FromHeader] string idParent)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            //extrair l'extention 
            string extension = Path.GetExtension(file.FileName);
            //donner un nouveau nom avec guid
            var idFile= Guid.NewGuid().ToString(); 
            var fileName=Path.GetFileName(file.FileName);
            //get folder by id
            string pathparent = _folderServices.GetFolderPathById(idParent);
            var found = fileName.IndexOf(".");
            fileName = fileName.Substring(0, found);
            var filePath = @pathparent + "\\"+ idFile + extension; // Path.GetTempFileName();
            //https://dotnettutorials.net/lesson/filestream-class-in-csharp/
            using (var stream = new FileStream(filePath , FileMode.Create,FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }
            //SAVE PATH FILES
            var resultsave = _fileservices.SavePath(idFile, fileName, idParent,extension);
            return Ok(new { filePath });
        }

        [HttpGet("GetFilesByParent/{folderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilesByParent(string folderId)
        {
            var files = await _fileservices.GetFilsByIdPrentFolder(folderId);

            if (files == null || files.Count == 0)
                return NotFound("Aucun fichier trouvé.");

            return Ok(files);
        }
        [HttpDelete("DeleteFile/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteFile(string id)
        {
            try {
                //Supprimer le fichier physique 
                string filePath = _fileservices.GetFilePath(id);

                if (System.IO.File.Exists(filePath)) // Vérifie si le fichier existe
                {
                    System.IO.File.Delete(filePath); // Supprime le fichier
                    Console.WriteLine($"Fichier supprimé : {filePath}");

                }
                else
                {
                    Console.WriteLine("Fichier introuvable.");

                }

                bool Results = await _fileservices.DeleteFile(id);
            if (!Results)
            {
                return NotFound(new { message = "Fichier introuvable" });
            }

           

            return Ok(new { message = "Fichier supprimé avec succès" });
        }
            catch (UnauthorizedAccessException)
            {
                return Forbid(); // L'utilisateur n'a pas les droits
            }
            catch (IOException ex)
            {
                return Conflict(new { message = "Impossible de supprimer le fichier. Il est peut-être utilisé par un autre processus.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur.", error = ex.Message });
            }
        }
    }
}
