using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class CourseStudentsViewComponent : ViewComponent
    {
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;
        public CourseStudentsViewComponent(IStudentManager studentManager, IMapper mapper)
        {
            _studentManager = studentManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id,string studentCourseStatus)
        {
            var studentDetails =
                _mapper.Map<IEnumerable<StudentMasterListViewModel>>(await _studentManager.StudentMasterList(new StudentMasterListFilter
                {
                    CourseId = id,
                    Status = studentCourseStatus
                }).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("Students",studentDetails)).ConfigureAwait(false);
        }
    }
}