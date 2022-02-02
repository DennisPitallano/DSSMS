namespace AJ3.Core.Data.Entity
{
    public class OrderDetail : EntityBase
    {
        public int OrderId { get; set; }
        public int StudentCourseId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
