using System;

namespace AJ3.Core.DTO
{
    public class OrderPaymentReportList
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string OrNumber { get; set; }
        public int Id { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int OrderPaymentId { get; set; }
        public string CreatedBy { get; set; }
    }

    public class OrderPaymentReportListFilter
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
