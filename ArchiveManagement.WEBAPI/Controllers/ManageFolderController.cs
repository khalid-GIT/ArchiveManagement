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
using Microsoft.IdentityModel.Tokens;

namespace ArchiveManagement.WEBAPI.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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

        public object CreatFolder(string FolderName, string? parentFolderPath, string TypeDocument)
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
                    var resultsav = _folderServices.SavePath(roots, "root", null, TypeDocument);
                }
            }
            else
            {
                FolderPath = _folderServices.GetFolderPathById(parentFolderPath);
            }
            string pathToNewFolder = string.Empty;
            string creatFolder = string.Empty;
            try
            {
                if (!Directory.Exists(FolderPath != null ? FolderPath + "\\" + FolderName : roots + "\\" + FolderName))
                {
                    if (FolderPath.Trim() != "" && !FolderPath.IsNullOrEmpty() && FolderPath.Trim() !=null)
                    {

                        creatFolder = FolderPath ;

                    }
                    else
                    {
                        creatFolder = roots;
                    }

                    pathToNewFolder = System.IO.Path.Combine(creatFolder, FolderName);
                    if (!Directory.Exists(pathToNewFolder))
                    {
                        DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);
                    }
                }
                var resultsave = _folderServices.SavePath(pathToNewFolder, FolderName, idroot, TypeDocument);
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


                    if (filePaths.Length > 0 || items != null)
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
        [HttpGet("GetAllFolder")]
        public object GetAllFolder()
        {
            var listefolder = _folderServices.GetAllFolder();
            if (listefolder == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "No Folder  exist"
                };
            }

            return listefolder;
        }
        [HttpGet("GetAllFolderOfThisfolder")]
        public object GetAllFolderOfThisfolder(string id)
        {
            var listefolder = _folderServices.GetAllFolderOfThisfolder(id);
            if (listefolder == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "No Folder  exist"
                };
            }

            return listefolder;
        }
    }
}
