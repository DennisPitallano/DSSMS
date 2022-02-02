using System;
using AJ3.WebApp.Infrastructure.Extensions;
using Humanizer;

namespace AJ3.WebApp.Models.Student
{
    public class OrderDetailListViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int OrderId { get; set; }
        public int StudentCourseId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int TotalHours { get; set; }
        public string DisplayAmount => Amount.ToPhFormatCurrency();
        public string DisplayDisCount => Discount.ToPhFormatCurrency();
        public string DisplayHours => "hours".ToQuantity(TotalHours);
    }
}
