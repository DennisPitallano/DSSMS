using System;

namespace AJ3.Core.Data.Entity
{
    public class Student : EntityBase
    {
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
    }
}
