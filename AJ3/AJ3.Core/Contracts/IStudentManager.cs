using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IStudentManager : IRepository<Student,StudentRequest>, IDelete
    {
        Task<Student> UpdateStatus(int id,bool isDeleted, string userName);
        Task<Student> CreateNewStudent(NewStudent model);
        Task<IEnumerable<StudentMasterList>> StudentMasterList(StudentMasterListFilter filter);
    }
}
