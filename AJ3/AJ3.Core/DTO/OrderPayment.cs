using System;

namespace AJ3.Core.DTO
{
    public class OrderPaymentRequest
    {
        public int  Id { get; set; }
        public int OrderId { get; set; }
        public string OrNumber { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string UserName { get; set; }
    }
}
