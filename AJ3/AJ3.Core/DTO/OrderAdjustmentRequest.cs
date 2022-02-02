namespace AJ3.Core.DTO
{
    public class OrderAdjustmentRequest
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Detail { get; set; }
        public decimal Amount { get; set; }
        public bool IsCredit { get; set; }
        public string UserName { get; set; }
    }
}
