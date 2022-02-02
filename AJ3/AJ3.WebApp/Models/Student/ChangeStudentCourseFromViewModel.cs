using System.ComponentModel.DataAnnotations;
using AJ3.WebApp.Infrastructure.Extensions;

namespace AJ3.WebApp.Models.Student
{
    public class ChangeStudentCourseFromViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [Required]
        public int ToCourseId { get; set; }
        public string ChangeType { get; set; }
        public int OrderId { get; set; }
        [Required]
        public string ChangeCourseReason { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Value")]
        public decimal AdjustmentAmount { get; set; }
        public bool IsCredit { get; set; }
        public string UserName { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public bool IsRefunded { get; set; }
        public bool IsUpgrade { get; set; }
        public string DisplayTotalPaidAmount => TotalPaidAmount.ToPhFormatCurrency();
    }
}
