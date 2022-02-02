using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class StudentMiniProfileViewComponent : ViewComponent
    {
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;
        public StudentMiniProfileViewComponent(IStudentManager studentManager, IMapper mapper)
        {
            _studentManager = studentManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var studentDetails =
                _mapper.Map<StudentDetailViewModel>(await _studentManager.GetByIdAsync(id).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("Detail",studentDetails)).ConfigureAwait(false);
        }
    }
}
