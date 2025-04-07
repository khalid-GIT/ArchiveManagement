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
using System.Reflection.Metadata;
using ArchiveManagement.BLL.Dtos;


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

        [HttpGet("DownloadFile/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadFile(string id)
        {
            try
            {
                // 🔹 Récupérer le fichier depuis la base de données ou le système de fichiers
                var file = await _fileservices.GetFileById(id);
                if (file == null)
                {
                    return NotFound(new { message = "Fichier introuvable" });
                }

                // 🔹 Construire le chemin complet du fichier
                //string filePath = Path.Combine("wwwroot/uploads", file.FileName);
                string filePath = _fileservices.GetFilePath(id);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { message = "Fichier non trouvé sur le serveur" });
                }

                // 🔹 Lire le fichier et le retourner en réponse
                var fileBytes = System.IO.File.ReadAllBytes(filePath);


               // string MyExtention = file.extension.Substring(1,4);

                //     byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                //Suitch MyExtention
                //    case "pdf"



                        return File(fileBytes, "application/pdf",  filePath);
          //      return File(fileBytes, "application/octet-stream", filePath);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne", details = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] Document updatedDoc)
        {
            //if (id != updatedDoc.Name)
            //{
            //    return BadRequest("L'ID du document ne correspond pas.");
            //}
            var documentsDto = new DocumentsDto
            {
               // id = updatedDoc.Name
               
            };



            return Ok();
        }
    }
}
