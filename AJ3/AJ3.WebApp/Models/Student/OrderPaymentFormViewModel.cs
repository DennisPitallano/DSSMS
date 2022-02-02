using System;
using System.ComponentModel.DataAnnotations;

namespace AJ3.WebApp.Models.Student
{
    public class OrderPaymentFormViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Required(ErrorMessage = "OR Number is required")]
        public string OrNumber { get; set; }
        public Guid PaymentId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string UserName { get; set; }
        public int StudentId { get; set; }
        public decimal BalanceAmount { get; set; }
        public string Type { get; set; }
    }
}
