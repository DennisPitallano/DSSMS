using System;

namespace AJ3.Core.Data.Entity
{
    public class OrderPayment : EntityBase
    {
        public int OrderId { get; set; }
        public string OrNumber { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
