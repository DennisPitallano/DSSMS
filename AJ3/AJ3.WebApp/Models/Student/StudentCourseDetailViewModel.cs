using System;
using AJ3.WebApp.Infrastructure.Extensions;
using Humanizer;

namespace AJ3.WebApp.Models.Student
{
    public class StudentCourseDetailViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CourseId { get; set; }
        public string CourseCategory { get; set; }
        public int StudentId { get; set; }
        public int TotalHours { get; set; }
        public int ConsumeHours { get; set; }
        public int FreeHours { get; set; }
        public int ConsumeFreeHours { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayHours => "hours".ToQuantity(TotalHours);
        public string DisplayConsumedHours => "hours".ToQuantity(ConsumeHours);
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public int OrderId { get; set; }
        public string DisplayAmount => Amount.ToPhFormatCurrency();
        public string DisplayDiscount => Discount.ToPhFormatCurrency();
        public decimal TotalPaidAmount { get; set; }
        public string DisplayTotalPaidAmount => TotalPaidAmount.ToPhFormatCurrency();
        public decimal PaymentPercentageComplete => ((TotalPaidAmount) / Amount)*100;
        public string DisplayBalanceAmount => (Amount - (TotalPaidAmount)).ToPhFormatCurrency();
        public decimal BalanceAmount => (Amount - (TotalPaidAmount));
        public string OrderNumber { get; set; }
        public DateTime  DateOrdered { get; set; }
        public int ChangeToCourseId { get; set; }
        public string ChangeToCourseType { get; set; }
        public string ChangeCourseReason { get; set; }
        public bool IsCancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool IsRefunded { get; set; }
        public string CancelledBy { get; set; }
        public DateTime DateCancelled { get; set; }
        public string ChangeCourseToName { get; set; }
        public int CategoryId { get; set; }
        public decimal AdjustmentAdditional { get; set; }
        public decimal AdjustmentMinus { get; set; }
        public string DisplayAdjustmentAmount => (AdjustmentAdditional-AdjustmentMinus).ToPhFormatCurrency();
        public decimal TotalPayableAmount => (BalanceAmount + (AdjustmentAdditional - AdjustmentMinus));
        public string DisplayTotalPayableAmount => TotalPayableAmount.ToPhFormatCurrency();
    }
}
