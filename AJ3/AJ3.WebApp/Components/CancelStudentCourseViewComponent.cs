using System.Threading.Tasks;
using AJ3.WebApp.Models.Student;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class CancelStudentCourseViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id, int studentId, int orderId,decimal totalPaidAmount)
        {
            var model = new CancelStudentCourseFormViewModel
            {
                Id = id,
                StudentId = studentId,
                OrderId = orderId,
                TotalPaidAmount = totalPaidAmount
            };
          
            return await Task.FromResult<IViewComponentResult>(View("Form",model)).ConfigureAwait(false);
        }
    }
}
