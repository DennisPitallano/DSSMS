using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AJ3.WebApp.Components
{
    public class ChangeStudentCourseFormViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly ICourseManager _courseManager;
        public ChangeStudentCourseFormViewComponent(IMapper mapper, ICourseManager courseManager)
        {
            _mapper = mapper;
            _courseManager = courseManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int studentId, int orderId, decimal totalPaidAmount, int categoryId, int currentCourseId)
        {
            var formModel = new ChangeStudentCourseFromViewModel
            {
                Id = id,
                StudentId = studentId,
                OrderId = orderId,
                TotalPaidAmount = totalPaidAmount
            };
            var availableCourses =
                _mapper.Map<IEnumerable<AvailableCourseViewModel>>(await _courseManager.GetAvailableCoursesByCategoryId(categoryId).ConfigureAwait(false));
            var courses = new SelectList(availableCourses.Where(a=>!a.Id.Equals(currentCourseId)).Select(a=> new {a.Id, Desc=$"{a.Description}[{a.DisplayHours}]({a.DisplayUnitPrice})"}), "Id", "Desc");
            ViewBag.AvailableCourses = courses;
            return await Task.FromResult<IViewComponentResult>(View("Form", formModel)).ConfigureAwait(false);
        }
    }
}
