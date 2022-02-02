using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Course;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class CourseDetailViewComponent : ViewComponent
    {
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;
        public CourseDetailViewComponent(IMapper mapper, ICourseManager courseManager)
        {
            _mapper = mapper;
            _courseManager = courseManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var courseDetail =
                _mapper.Map<CourseMasterListViewModel>(await _courseManager.GetByIdAsync(id).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("CourseDetail",courseDetail)).ConfigureAwait(false);
        }
    }
}
