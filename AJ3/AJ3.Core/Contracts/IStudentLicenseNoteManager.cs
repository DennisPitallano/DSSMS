using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IStudentLicenseNoteManager : IRepository<StudentLicenseNoteListItem,StudentLicenseNote>
    {
        Task<IEnumerable<StudentLicenseNoteListItem>> GetStudentLicenseNotesByStudentId(int studentId);
    }
}
