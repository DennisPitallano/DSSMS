using System;
using AJ3.WebApp.Infrastructure.Extensions;
using Humanizer;

namespace AJ3.WebApp.Models.Student
{

    public class AvailableCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string CategoryName { get; set; }
        public string DisplayUnitPrice => UnitPrice.ToPhFormatCurrency();
        public string DisplayDiscount => Discount.ToPhFormatCurrency();
        public decimal DiscountedPrice => (UnitPrice - Discount);
        public string DisplayDiscountedPrice => (UnitPrice - Discount).ToPhFormatCurrency();
        public string DisplayHours => "hours".ToQuantity(Hours);
    }
}
