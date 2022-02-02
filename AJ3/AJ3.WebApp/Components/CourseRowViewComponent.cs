using System.Threading.Tasks;
using AJ3.WebApp.Models.Course;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class CourseRowViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CourseMasterListViewModel model)
        {
            return await Task.FromResult<IViewComponentResult>(View("CourseListItem",model)).ConfigureAwait(false);
        }
    }
}
