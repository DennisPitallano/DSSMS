using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Course;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class CourseMasterListViewComponent : ViewComponent
    {
        private readonly ICourseManager _courseManager;
        private readonly IMapper _mapper;
        public CourseMasterListViewComponent(IMapper mapper, ICourseManager courseManager)
        {
            _mapper = mapper;
            _courseManager = courseManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? courseCategoryId)
        {
            var courseList = _mapper.Map<IEnumerable<CourseMasterListViewModel>>(
                await _courseManager.GetCourseListByCategoryId(courseCategoryId).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("CourseList",courseList)).ConfigureAwait(false);
        }
    }
}
