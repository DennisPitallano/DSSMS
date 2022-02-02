using System.ComponentModel.DataAnnotations;

namespace AJ3.WebApp.Models.Setting
{
    public class CompanyProfileViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Company Name is required!")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string TagLine { get; set; }
        public string Manager { get; set; }
        public string UserName { get; set; }
    }
}
