using System;

namespace AJ3.WebApp.Models.Student
{
    public class StudentDetailViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
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
        public string FullName => $"{LastName}, {FirstName} {MiddleName}";
        public string DisplayId => $"AJ3 {Id:D10}";
        public string StudentStatus => IsDeleted ? "IN ACTIVE" : "ACTIVE";
    }
}
