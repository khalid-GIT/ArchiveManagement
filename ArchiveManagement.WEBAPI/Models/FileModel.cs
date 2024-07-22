namespace ArchiveManagement.WEBAPI.Models
{
    public class FileModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string FolderPath { get; set; }
       
    }
}
