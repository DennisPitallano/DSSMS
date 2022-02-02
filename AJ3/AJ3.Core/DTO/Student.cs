using System;
using AJ3.Core.Data.Entity;

namespace AJ3.Core.DTO
{
    public class StudentRequest
    {
        public int Id { get; set; }
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
        public string UserName { get; set; }
    }

    public class NewStudent
    {
        public int  Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Picture { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public int HeightCm { get; set; }
        public int Weight { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public int CourseId { get; set; }
        public int TotalHours { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string OrNumber { get; set; }
        public Guid PaymentId { get; set; }
        public decimal PayAmount { get; set; }
    }

    public class StudentMasterList : Student
    {
        public int CourseTotalHours { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseStatus { get; set; }
    }

    public class StudentMasterListFilter
    {
        public int?  CourseId { get; set; }
        public string Status { get; set; }
    }
}
