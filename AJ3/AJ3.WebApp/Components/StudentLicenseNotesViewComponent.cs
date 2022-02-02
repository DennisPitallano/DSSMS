using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AJ3.WebApp.Components
{
    public class StudentLicenseNotesViewComponent : ViewComponent
    {
        private readonly ILicenseNoteManager _licenseNoteManager;
        private readonly IStudentLicenseNoteManager _studentLicenseNoteManager;
        private readonly IMapper _mapper;
        public StudentLicenseNotesViewComponent(ILicenseNoteManager licenseNoteManager, IMapper mapper, IStudentLicenseNoteManager studentLicenseNoteManager)
        {
            _licenseNoteManager = licenseNoteManager;
            _mapper = mapper;
            _studentLicenseNoteManager = studentLicenseNoteManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string licenseNoteType,int studentId=0)
        {
            var studentLicenseNotes = await _studentLicenseNoteManager
                .GetStudentLicenseNotesByStudentId(studentId).ConfigureAwait(false);
            ViewBag.LicenseNoteType = licenseNoteType;
            var licenseNotes =_mapper.Map<IEnumerable<LicenseNoteViewModel>>(
                await _licenseNoteManager.GetLicenseNoteByType(licenseNoteType).ConfigureAwait(false));
            var licenseNoteViewModels = licenseNotes.ToList();
            var selectList =
                new MultiSelectList(licenseNoteViewModels, "Id", "Description", studentLicenseNotes.Select(a => a.LicenseNoteId));
            return await Task.FromResult<IViewComponentResult>(View("LicenseNotes",selectList)).ConfigureAwait(false);
        }
    }
}
