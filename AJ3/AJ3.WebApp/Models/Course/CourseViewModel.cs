using System;
using System.ComponentModel.DataAnnotations;
using AJ3.WebApp.Infrastructure.Extensions;
using Humanizer;

namespace AJ3.WebApp.Models.Course
{
    public class CourseMasterListViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid hours greater than {1}")]
        public int Hours { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int CoursePriceId { get; set; }
        public string DisplayUnitPrice => UnitPrice.ToPhFormatCurrency();
        public string DisplayDiscount => Discount.ToPhFormatCurrency();
        public string DisplayHours =>  "hours".ToQuantity(Hours);
        public string CourseCategoryName { get; set; }
        public decimal OrigUnitPrice { get; set; }
        public decimal OrigDiscount { get; set; }
    }
}
