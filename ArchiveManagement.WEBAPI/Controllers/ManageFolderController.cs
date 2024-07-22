using ArchiveManagement.BLL.Dtos;
using ArchiveManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using ArchiveManagement.BLL.Implementations;
using ArchiveManagement.DAL.Entities;
using Google.Protobuf.WellKnownTypes;

namespace ArchiveManagement.WEBAPI.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class ManageFolderController : ControllerBase
    {
        private readonly IFolderServices _folderServices;
        private readonly IConfiguration _configuration;
        public ManageFolderController(IFolderServices folderServices, IConfiguration configuration)
        {
            _folderServices = folderServices;
            _configuration = configuration;
        }

        [HttpPost("CreatFolder")]
        public string CreatFolder(string FolderName, string? parentFolderPath)
        {
            string roots = _configuration["RootPath"];
            string idroot = parentFolderPath != null ? parentFolderPath : _folderServices.GetIdFolderByName("root");
            string FolderPath = string.Empty;

            if (parentFolderPath == null)
            {
                if (!Directory.Exists(roots))
                {
                    return "Root not exist";
                }
                if (_folderServices.GetIdFolderByName("root") == null)
                {
                    var resultsav = _folderServices.SavePath(roots, "root", null);
                }
            }
            else
            {
                FolderPath = _folderServices.GetFolderPathById(parentFolderPath);
            }
            string pathToNewFolder = string.Empty;
            try
            {
                if (!Directory.Exists(FolderPath != null ? FolderPath + "\\" + FolderName : roots + "\\" + FolderName))
                {
                    pathToNewFolder = System.IO.Path.Combine(FolderPath != null ? FolderPath : roots, FolderName);
                    if (!Directory.Exists(pathToNewFolder))
                    {
                        DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);
                    }
                }
                var resultsave = _folderServices.SavePath(pathToNewFolder, FolderName, idroot);
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            return pathToNewFolder;
        }


    }
}
