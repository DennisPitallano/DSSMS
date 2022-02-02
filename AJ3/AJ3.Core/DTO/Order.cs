using System;
using AJ3.Core.Data.Entity;

namespace AJ3.Core.DTO
{
    public class OrderRequest
    {
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }

    public class OrderMasterList : Order
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public decimal TotalPaidAmount { get; set; }
    }

    public class OrderMasterListFilter
    {
    }
}
