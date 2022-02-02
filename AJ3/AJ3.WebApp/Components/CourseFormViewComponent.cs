using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Course;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AJ3.WebApp.Components
{
    public class CourseFormViewComponent : ViewComponent
    {
        private readonly ICourseManager _courseManager;
        private readonly ICourseCategoryManager _courseCategoryManager;
        private readonly IMapper _mapper;

        public CourseFormViewComponent(ICourseManager courseManager, IMapper mapper, ICourseCategoryManager courseCategoryManager)
        {
            _courseManager = courseManager;
            _mapper = mapper;
            _courseCategoryManager = courseCategoryManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, FormView view = FormView.Update)
        {
            var model = new CourseMasterListViewModel();
            var courseCategories = await _courseCategoryManager.GetCategories().ConfigureAwait(false);
            var courseCategoryDetails = courseCategories.ToList();
            var selected = courseCategoryDetails.First().Id;
            SelectList courseCategoriesSelectList;
            if (view ==FormView.New)
            {
                courseCategoriesSelectList = new  SelectList(courseCategoryDetails,"Id","Name",selected);
                ViewBag.CourseCategories = courseCategoriesSelectList;
                return await Task.FromResult<IViewComponentResult>(View("Form",model)).ConfigureAwait(false);
            }
            model = _mapper.Map<CourseMasterListViewModel>(await _courseManager.GetByIdAsync(id)
                .ConfigureAwait(false));
            model.OrigDiscount = model.Discount;
            model.OrigUnitPrice = model.UnitPrice;
            courseCategoriesSelectList = new  SelectList(courseCategoryDetails,"Id","Name",model.CategoryId);
            ViewBag.CourseCategories = courseCategoriesSelectList;
            return await Task.FromResult<IViewComponentResult>(View("Form",model)).ConfigureAwait(false);
        }
    }
}
