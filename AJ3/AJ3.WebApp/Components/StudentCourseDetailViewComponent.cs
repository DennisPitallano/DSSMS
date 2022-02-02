using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class StudentCourseDetailViewComponent : ViewComponent
    {
        private readonly IStudentCourseManager _studentCourseManager;
        private readonly IMapper _mapper;
        private readonly IStudentManager _studentManager;
        public StudentCourseDetailViewComponent(IStudentCourseManager studentCourseManager, IMapper mapper, IStudentManager studentManager)
        {
            _studentCourseManager = studentCourseManager;
            _mapper = mapper;
            _studentManager = studentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, OrderView view = OrderView.Student)
        {
            StudentCourseDetailViewModel studentCourse;
            switch (view)
            {
                case OrderView.Student:
                    var courses = _mapper.Map<IEnumerable<StudentCourseDetailViewModel>>(await _studentCourseManager.GetCourses(id).ConfigureAwait(false));
                    //studentCourse =
                    //    _mapper.Map<StudentCourseDetailViewModel>(await _studentCourseManager.GetDetails(id)
                    //        .ConfigureAwait(false));
                    //ViewBag.BalanceAmount = studentCourse.BalanceAmount;
                    return await Task.FromResult<IViewComponentResult>(View("Detail", courses)).ConfigureAwait(false);
                case OrderView.Order:
                    studentCourse =
                        _mapper.Map<StudentCourseDetailViewModel>(await _studentCourseManager.GetDetails(null, id)
                            .ConfigureAwait(false));
                    var studentDetails =
                        _mapper.Map<StudentDetailViewModel>(await _studentManager.GetByIdAsync(studentCourse.StudentId).ConfigureAwait(false));
                    ViewBag.Student = studentDetails;
                    ViewBag.TotalAmount = studentCourse.TotalPayableAmount;
                    return await Task.FromResult<IViewComponentResult>(View("OrderDetail", studentCourse)).ConfigureAwait(false);
                default:
                    throw new ArgumentOutOfRangeException(nameof(view), view, null);
            }
        }
    }
}
