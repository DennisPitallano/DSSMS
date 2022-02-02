using System;
using AJ3.WebApp.Infrastructure.Extensions;

namespace AJ3.WebApp.Models.Student
{
    public class OrderMasterListViewModel
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public string FullName => $"{LastName}, {FirstName} {MiddleName}";
        public string DisplayTotalAmount => TotalAmount.ToPhFormatCurrency();
        public string DisplayDiscount => TotalDiscount.ToPhFormatCurrency();
        public string DisplayTotalPaidAmount => TotalPaidAmount.ToPhFormatCurrency();
        public decimal Balance => TotalAmount - TotalPaidAmount;
        public string DisplayBalance => Balance.ToPhFormatCurrency();
    }
}
