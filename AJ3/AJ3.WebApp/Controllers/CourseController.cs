using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Models.Course;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AJ3.WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class CourseController : Controller
    {
        private readonly ICourseManager _courseManager;
        private readonly ICourseCategoryManager _courseCategoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ICourseManager courseManager, IMapper mapper, ILogger<CourseController> logger, ICourseCategoryManager courseCategoryManager)
        {
            _courseManager = courseManager;
            _mapper = mapper;
            _logger = logger;
            _courseCategoryManager = courseCategoryManager;
        }

        #region Views
        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name.Contains("AJ3"))
            {
                return RedirectToAction("Index", "Home");
            }
            var courseCategories = await _courseCategoryManager.GetCategories().ConfigureAwait(false);
            var courseCategoryDetails = courseCategories.ToList();
            var first = courseCategoryDetails.FirstOrDefault();
            ViewBag.CourseCategoryId = first?.Id;
            var courseCategoriesSelectList = new SelectList(courseCategoryDetails, "Id", "Name", first?.Id);
            ViewBag.CourseCategories = courseCategoriesSelectList;
            return View();
        }

        /// <summary>
        /// Get Course Details
        /// </summary>
        /// <param name="id">CourseId</param>
        /// <returns></returns>
        [HttpGet,Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.CourseId = id;
            return View();
        }
        #endregion

        #region Process
        [HttpPost]
        public async Task<IActionResult> SaveCourse(CourseMasterListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var course = _mapper.Map<CourseRequest>(model);
                    course.IsUpdatedPrice =
                        (model.OrigUnitPrice != model.UnitPrice || model.OrigDiscount != model.Discount);
                    course.UserName = User.Identity?.Name ?? "SYSTEM_USER";
                    if (model.Id > 0)
                    {
                        var result = await _courseManager.UpdateAsync(course).ConfigureAwait(false);
                        _logger.LogInformation("save course", result);
                        return await Task.FromResult(ViewComponent("CourseRow", _mapper.Map<CourseMasterListViewModel>(result))).ConfigureAwait(false);
                    }
                    else
                    {
                        var result = await _courseManager.CreateAsync(course).ConfigureAwait(false);
                        _logger.LogInformation("save course", result);
                        return Ok(new
                        {
                            status = "Success",
                            description = $"New Course {result.Name} has been save!",
                            categoryId = result.CategoryId
                        });
                    }

                }
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to save payment!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to save payment!"
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourseStatus(int id, bool isDeleted, bool isPhysical = false)
        {
            try
            {
                var result = await _courseManager.SetStatus(id, isDeleted, User.Identity?.Name ?? "SYSTEM_USER").ConfigureAwait(false);
                if (result.Id <= 0)
                    return Ok(new
                    {
                        status = "Failed",
                        description = "Failed to update status!"
                    });
                _logger.LogInformation($"set status course to {isDeleted}", result);
                return await Task.FromResult(ViewComponent("CourseRow", _mapper.Map<CourseMasterListViewModel>(result))).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to update status!"
                });
            }
        }
        #endregion

        #region ViewComponent

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetCoursesByCategoryId([FromRoute] int? id)
        {
            return await Task.FromResult(ViewComponent("CourseMasterList", new { courseCategoryId = (id == null || id == 0) ? null : id })).ConfigureAwait(false);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetCourseForm([FromRoute] int id)
        {
            return await Task.FromResult(ViewComponent("CourseForm", new { id, view = id > 0 ? FormView.Update : FormView.New })).ConfigureAwait(false);
        }

        #endregion
    }
}
