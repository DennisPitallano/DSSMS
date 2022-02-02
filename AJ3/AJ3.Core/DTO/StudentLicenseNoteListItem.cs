using AJ3.Core.Data.Entity;

namespace AJ3.Core.DTO
{
    public class StudentLicenseNoteListItem : StudentLicenseNote
    {
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
