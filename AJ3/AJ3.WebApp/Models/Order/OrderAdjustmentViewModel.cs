using System;
using AJ3.WebApp.Infrastructure.Extensions;

namespace AJ3.WebApp.Models.Order
{
    public class OrderAdjustmentViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int OrderId { get; set; }
        public string Detail { get; set; }
        public decimal Amount { get; set; }
        public bool IsCredit { get; set; }
        public string DisplayAmount => Amount.ToPhFormatCurrency();
    }
}
