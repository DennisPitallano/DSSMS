using System;

namespace AJ3.WebApp.Models.Student
{
    public class StudentApplicationFormViewModel
    {
        public int Id { get; set; }
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
        //
        public int CourseId { get; set; }
        public int TotalHours { get; set; }

        public string OrderNumber => $"AJ3-{DateTime.Now:yyMMddHHmmss}";

        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string OrNumber { get; set; }
        public Guid PaymentId { get; set; }
        public decimal PayAmount { get; set; }
    }
}
