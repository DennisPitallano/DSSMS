using System;

namespace AJ3.WebApp.Models.Student
{
    public class LicenseNoteViewModel
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
