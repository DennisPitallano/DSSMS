using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class AvailableCoursesViewComponent : ViewComponent
    {
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;
        public AvailableCoursesViewComponent(ICourseManager courseManager, IMapper mapper)
        {
            _courseManager = courseManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var availableCourses =
                _mapper.Map<IEnumerable<AvailableCourseViewModel>>(await _courseManager.GetAvailableCoursesByCategoryId(id).ConfigureAwait(false));
            var availableCourseViewModels = availableCourses.ToList();
            var names = (from avc in availableCourseViewModels select avc.Name).Distinct();
            ViewBag.Names = names;
            return await Task.FromResult<IViewComponentResult>(View("List", availableCourseViewModels)).ConfigureAwait(false);
        }
    }
}
