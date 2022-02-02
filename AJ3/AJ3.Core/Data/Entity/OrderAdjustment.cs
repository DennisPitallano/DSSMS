namespace AJ3.Core.Data.Entity
{
    public class OrderAdjustment : EntityBase
    {
        public int OrderId { get; set; }
        public string Detail { get; set; }
        public decimal Amount { get; set; }
        public bool IsCredit { get; set; }
    }
}
