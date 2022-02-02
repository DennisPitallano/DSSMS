using System;
using Humanizer;

namespace AJ3.WebApp.Models.Student
{
    public class StudentMasterListViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Picture { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public int HeightCm { get; set; }
        public int Weight { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public int CourseTotalHours { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseStatus { get; set; }
        public string FullName => $"{LastName}, {FirstName} {MiddleName}";
        public string StudentId =>  $"AJ3 {Id:D10}";
        public string DisplayCourseHours => "hours".ToQuantity(CourseTotalHours);
    }
}
