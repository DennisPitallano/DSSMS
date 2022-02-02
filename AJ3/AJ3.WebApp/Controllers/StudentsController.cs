using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AJ3.WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class StudentsController : Controller
    {
        private readonly IStudentManager _studentManager;
        private readonly ICourseCategoryManager _courseCategoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;
        private readonly IOrderPaymentManager _orderPaymentManager;
        private readonly IStudentCourseManager _studentCourseManager;
        private readonly IStudentLicenseNoteManager _studentLicenseNoteManager;
        private readonly ICompanyProfileManager _companyProfileManager;
        public StudentsController(ICourseCategoryManager courseCategoryManager,
            IStudentManager studentManager, IMapper mapper, ILogger<StudentsController> logger, IOrderPaymentManager orderPaymentManager, IStudentCourseManager studentCourseManager, IStudentLicenseNoteManager studentLicenseNoteManager, ICompanyProfileManager companyProfileManager)
        {
            _courseCategoryManager = courseCategoryManager;
            _studentManager = studentManager;
            _mapper = mapper;
            _logger = logger;
            _orderPaymentManager = orderPaymentManager;
            _studentCourseManager = studentCourseManager;
            _studentLicenseNoteManager = studentLicenseNoteManager;
            _companyProfileManager = companyProfileManager;
        }

        #region Views

        public async Task<IActionResult> Index()
        {
            var masterList = _mapper.Map<IEnumerable<StudentMasterListViewModel>>(
                await _studentManager.StudentMasterList(new StudentMasterListFilter()).ConfigureAwait(false));
            return View(masterList);
        }

        [Route("{id?}")]
        public async Task<IActionResult> New(int? id)
        {
            var model = new StudentApplicationFormViewModel
            {
                Gender = false,
                DateOfBirth = DateTime.Now,
                Weight = 0,
                HeightCm = 0,
                Nationality = "FILIPINO"
            };
            if (id!=null)
            {
                var existingStudent = await _studentManager.GetByIdAsync(id).ConfigureAwait(false);
                model.Id = existingStudent.Id;
                model.Address = existingStudent.Address;
                model.DateOfBirth = existingStudent.DateOfBirth;
                model.Email = existingStudent.Email;
                model.FirstName = existingStudent.FirstName;
                model.Gender = existingStudent.Gender;
                model.HeightCm = existingStudent.HeightCm;
                model.LastName = existingStudent.LastName;
                model.MiddleName = existingStudent.MiddleName;
                model.Nationality = existingStudent.Nationality;
                model.PhoneNumber = existingStudent.Phone;
                model.Weight = existingStudent.Weight;
            }
            ViewBag.OrderNumber = model.OrderNumber;
            var courseCategories = new SelectList(await _courseCategoryManager.GetCategories(), "Id", "Name");
            ViewBag.CourseCategories = courseCategories;
            ViewBag.CompanyProfile = await _companyProfileManager.GetByIdAsync(0).ConfigureAwait(false);
            ViewBag.DisabledClass = model.Id > 0 ? "disabled" : "";
            return View(model);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Student([FromRoute] int id)
        {
            ViewBag.StudentId = id;
            return await Task.FromResult(View()).ConfigureAwait(false);
        }
        #endregion

        #region ViewComponents

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetAvailableCoursesByCategoryId([FromRoute] int id)
        {
            return await Task.FromResult(ViewComponent("AvailableCourses", new { id })).ConfigureAwait(false);
        }

        [HttpGet, Route("{id}/{sid}/{balance}/{type}")]
        public async Task<IActionResult> GetOrderPaymentForm(int id, int sid, decimal balance, FormView type)
        {
            return await Task.FromResult(ViewComponent("OrderPaymentForm", new { id, studentId = sid, balance, view = type })).ConfigureAwait(false);
        }

        
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetStudentCourseDetail(int id)
        {
            return await Task.FromResult(ViewComponent("StudentCourseDetail", new { id })).ConfigureAwait(false);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetOrderPaymentHistoryDetail(int id)
        {
            return await Task.FromResult(ViewComponent("OrderPaymentHistoryDetail", new { orderId = id })).ConfigureAwait(false);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetMiniProfile(int id)
        {
            return await Task.FromResult(ViewComponent("StudentMiniProfile", new { id })).ConfigureAwait(false);
        }

        [HttpGet, Route("{id}/{sid}/{orid}/{amt}")]
        public async Task<IActionResult> GetCancelCourseForm(int id,int sid,int orid,decimal amt)
        {
            return await Task.FromResult(ViewComponent("CancelStudentCourse", new { id,studentId=sid, orderId=orid,totalPaidAmount =amt})).ConfigureAwait(false);
        }

        [HttpGet, Route("{id}/{sid}/{orid}/{amt}/{catid}/{curcid}")]
        public async Task<IActionResult> GetChangeCourseForm(int id, int sid, int orid, decimal amt,int catid,int curcid)
        {
            return await Task.FromResult(ViewComponent("ChangeStudentCourseForm", new { id, studentId = sid,
                orderId = orid, totalPaidAmount = amt, categoryId = catid, currentCourseId = curcid })).ConfigureAwait(false);
        }

        #endregion

        #region Processes

        [HttpPost]
        public async Task<IActionResult> SaveStudent(StudentApplicationFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStudent = _mapper.Map<NewStudent>(model);
                    newStudent.PaymentId = Guid.NewGuid();
                    newStudent.UserName = User.Identity?.Name ?? "SYSTEM_USER";
                    var result = await _studentManager.CreateNewStudent(newStudent).ConfigureAwait(false);
                    _logger.LogInformation("new student", result);
                    return RedirectToAction("Student", new { id = result.Id });
                }
                return View("New");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return View("New");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrderPayment(OrderPaymentFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrderPayment = _mapper.Map<OrderPaymentRequest>(model);
                    newOrderPayment.PaymentId = Guid.NewGuid();
                    newOrderPayment.UserName = User.Identity?.Name ?? "SYSTEM_USER";
                    if (model.Type == "New")
                    {
                        var result = await _orderPaymentManager.CreateAsync(newOrderPayment).ConfigureAwait(false);
                        _logger.LogInformation("new payment", result);
                        return Ok(new
                        {
                            status = "Success",
                            Model = "",
                            model.StudentId,
                            model.OrderId
                        });
                    }
                    else
                    {
                        var result = await _orderPaymentManager.UpdateAsync(newOrderPayment).ConfigureAwait(false);
                        _logger.LogInformation("update payment", result);
                        return Ok(new
                        {
                            status = "Success",
                            Model = "",
                            model.StudentId,
                            model.OrderId
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
        public async Task<IActionResult> UpdateStudentCourseStatus(int id, string status)
        {
            try
            {
                var result = await _studentCourseManager.UpdateCourseStatus(id, status, User.Identity?.Name ?? "SYSTEM_USER").ConfigureAwait(false);
                if (result.Id <= 0)
                    return Ok(new
                    {
                        status = "Failed",
                        description = "Failed to update status!"
                    });
                _logger.LogInformation($"set status course to {status}", result);
                return Ok(new
                {
                    status = "Success",
                    description = "Status has been updated!",
                    CourseStatus = result.Status,
                    CourseStatusId = result.Id,
                    result.StudentId
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception:", e.Message);
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to update status!"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentInfo(StudentDetailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentRequest = _mapper.Map<StudentRequest>(model);
                    studentRequest.UserName = User.Identity?.Name ?? "SYSTEM_USER";
                    var result = await _studentManager.UpdateAsync(studentRequest).ConfigureAwait(false);
                    var modelResult = _mapper.Map<StudentDetailViewModel>(result);
                    _logger.LogInformation("update student", result);
                    return await Task.FromResult(ViewComponent("StudentDetail", new { id = modelResult.Id, model = modelResult })).ConfigureAwait(false);
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
        public async Task<IActionResult> UpdateStudentLicenseNote(int id, int studentId)
        {
            try
            {
                var result = await _studentLicenseNoteManager.CreateAsync(new StudentLicenseNote
                {
                    StudentId = studentId,
                    UpdatedBy = User.Identity?.Name ?? "SYSTEM_USER",
                    LicenseNoteId = id
                }).ConfigureAwait(false);
                if (result == null)
                {
                    return Ok(new
                    {
                        status = "Success",
                        description = "Remove Note Successful!"
                    });
                }
                //_logger.LogInformation($"set status course to {status}", result);
                return Ok(new
                {
                    status = "Success",
                    description = "Added Note Successful!",
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception:", e.Message);
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to update status!"
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudentStatus(int id, bool isDeleted)
        {
            try
            {
                var result = await _studentManager.UpdateStatus(id,isDeleted,User.Identity?.Name ?? "SYSTEM_USER").ConfigureAwait(false);
                if (result.Id>0)
                {
                    return Ok(new
                    {
                        status = "Success",
                        description = "Update Successful!"
                    });
                }
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to Update!",
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception:", e.Message);
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to update status!"
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelStudentCourse(CancelStudentCourseFormViewModel model)
        {
            try
            {
                var request = _mapper.Map<CancelStudentCourseRequest>(model);
                request.UserName = User.Identity?.Name ?? "SYSTEM_USER";
                var result = await _studentCourseManager.CancelStudentCourse(request).ConfigureAwait(false);
                if (result.ResultCode=="00")
                {
                    return Ok(new
                    {
                        status = "Success",
                        description = "Cancellation Successful!",
                        model.StudentId,
                        model.OrderId
                    });
                }
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to student course!",
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception:", e.Message);
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to student course!",
                    model.StudentId,
                    model.OrderId
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStudentCourse(ChangeStudentCourseFromViewModel model)
        {
            try
            {
                var request = _mapper.Map<ChangeStudentCourseRequest>(model);
                request.UserName = User.Identity?.Name ?? "SYSTEM_USER";
                request.ChangeType = model.IsUpgrade ? "UPGRADE" : "DOWNGRADE";
                var result = await _studentCourseManager.ChangeStudentCourse(request).ConfigureAwait(false);
                if (result.ResultCode == "00")
                {
                    return Ok(new
                    {
                        status = "Success",
                        description = "Change Course Successful!",
                        model.StudentId,
                        model.OrderId
                    });
                }
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed update course!",
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception:", e.Message);
                return Ok(new
                {
                    status = "Failed",
                    description = "Failed to update course!",
                    model.StudentId,
                    model.OrderId
                });
            }
        }
        #endregion
    }
}
