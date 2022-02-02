using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IStudentCourseManager : IRepository<StudentCourse,StudentCourseRequest>
    {
        Task<StudentCourseDetail> GetDetails(int? studentId,int? orderId=null);
        Task<IEnumerable<StudentCourseDetail>> GetCourses(int? studentId,int? orderId=null);
        Task<StudentCourse> UpdateCourseStatus(int id, string status, string userName);

        Task<DbResult> CancelStudentCourse(CancelStudentCourseRequest cancelStudentCourseRequest);
        Task<DbResult> ChangeStudentCourse(ChangeStudentCourseRequest changeStudentCourseRequest);
    }
}
