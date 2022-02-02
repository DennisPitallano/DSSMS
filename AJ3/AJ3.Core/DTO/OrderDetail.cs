using AJ3.Core.Data.Entity;

namespace AJ3.Core.DTO
{
    public class OrderDetailRequest
    {
        public int OrderId { get; set; }
        public int StudentCourseId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string UserName { get; set; }
    }

    public class OrderDetailList : OrderDetail
    {
        public string Description { get; set; }
        public int TotalHours { get; set; }
        public string Name { get; set; }
    }
}
