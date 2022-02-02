using AJ3.Core.Data.Entity;

namespace AJ3.Core.DTO
{
    public class CourseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Hours { get; set; }
        public string UserName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsUpdatedPrice { get; set; }
    }

    public class CourseDetail : Course
    {
        public string TypeName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }

    public class AvailableCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string CategoryName { get; set; }
    }

    public class CourseMasterList : Course
    {
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int CoursePriceId { get; set; }
        public string CourseCategoryName { get; set; }
    }
}
