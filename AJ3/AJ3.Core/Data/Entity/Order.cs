using System;

namespace AJ3.Core.Data.Entity
{
    public class Order : EntityBase
    {
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string Status { get; set; }
    }
}
