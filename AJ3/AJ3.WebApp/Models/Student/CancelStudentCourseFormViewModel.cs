using System.ComponentModel.DataAnnotations;
using AJ3.WebApp.Infrastructure.Extensions;

namespace AJ3.WebApp.Models.Student
{
    public class CancelStudentCourseFormViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int OrderId { get; set; }
        [Required]
        public string CancellationReason { get; set; }
        public bool IsRefunded { get; set; }
        [Range(0,double.MaxValue,ErrorMessage = "Invalid Value")]
        public decimal RefundedAmount { get; set; }
        public string UserName { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public string DisplayTotalPaidAmount => TotalPaidAmount.ToPhFormatCurrency();
    }
}
