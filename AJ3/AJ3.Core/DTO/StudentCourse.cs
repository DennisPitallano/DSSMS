using System;
using AJ3.Core.Data.Entity;

namespace AJ3.Core.DTO
{
    public class StudentCourseRequest
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int TotalHours { get; set; }
        public int ConsumeHours { get; set; }
        public int FreeHours { get; set; }
        public int ConsumeFreeHours { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }

    public class StudentCourseDetail : EntityBase
    {
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
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
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
    }

    public class CancelStudentCourseRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int OrderId { get; set; }
        public string CancellationReason { get; set; }
        public bool IsRefunded { get; set; }
        public decimal RefundedAmount { get; set; }
        public string UserName { get; set; }
    }

    public class ChangeStudentCourseRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ToCourseId { get; set; }
        public string ChangeType { get; set; }
        public int OrderId { get; set; }
        public string ChangeCourseReason { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public bool IsCredit { get; set; }
        public string UserName { get; set; }
    }
}
