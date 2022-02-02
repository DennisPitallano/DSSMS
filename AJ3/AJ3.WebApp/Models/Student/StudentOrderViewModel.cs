using System;
using Humanizer;

namespace AJ3.WebApp.Models.Student
{
    public class StudentOrderViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string Status { get; set; }
    }

    public class StudentOrderPaymentViewModel
    {
        public int OrderId { get; set; }
        public string OrNumber { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime PaymentDate { get; set; }

        public string DisplayDateRecorded => DateCreated.Humanize(false,DateTime.Now);
    }
}
