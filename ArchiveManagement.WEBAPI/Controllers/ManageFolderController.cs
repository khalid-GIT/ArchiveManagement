using ArchiveManagement.BLL.Dtos;
using ArchiveManagement.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using ArchiveManagement.BLL.Implementations;
using ArchiveManagement.DAL.Entities;
using Google.Protobuf.WellKnownTypes;
using ArchiveManagement.WEBAPI.Models;

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
        public object CreatFolder(string FolderName, string? parentFolderPath)
        {
            string roots = _configuration["RootPath"];
            string idroot = parentFolderPath != null ? parentFolderPath : _folderServices.GetIdFolderByName("root");
            string FolderPath = string.Empty;

            if (parentFolderPath == null)
            {
                if (!Directory.Exists(roots))
                {
                    return new ResponseModel
                    {
                        Status = "Error",
                        Message = "Root not exist"
                    };
                   // return "Root not exist";
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
            return new ResponseModel
            {
                Status = "Success",
                Message = "Folder : " + pathToNewFolder + " Created"
            };
          //  return pathToNewFolder;
        }
        [HttpDelete("delete")]
        public object folderDelete(string id)
        {
            var pathfolder = _folderServices.GetFolderPathById(id);
            if (pathfolder == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Folder : " + pathfolder + " not exist"
                };
            }
            else
            {
                if (!Directory.Exists(pathfolder))
                {
                    return new ResponseModel
                    {
                        Status = "Error",
                        Message = "Folder  : " + pathfolder + "  not exist"
                    };
                }
                else
                {
                    string[] filePaths = Directory.GetFiles(pathfolder);
            //      var pop= pathfolder.GetFileSystemInfos().Length ; 
                    IEnumerable<string> items = Directory.EnumerateFileSystemEntries(pathfolder);


                    if (filePaths.Length > 0 || items!=null)
                    {
                        return new ResponseModel
                        {
                            Status = "Error",
                            Message = "Folder not Empty"
                        };
                    }
                    else { Directory.Delete(pathfolder); }
                }

            }
            return new ResponseModel
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        [HttpGet("GetFolderPathById")]
        public object GetFolderPathById(string id)
        {
            var pathfolder = _folderServices.GetFolderPathById(id);
            if (pathfolder == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Folder : " + pathfolder + " not exist"
                };
            }
            else
            {
                if (!Directory.Exists(pathfolder))
                {
                    return new ResponseModel
                    {
                        Status = "Error",
                        Message = "Folder  : " + pathfolder + "  not exist"
                    };
                }
                
                

            }
            return pathfolder;
        }
    }
}
