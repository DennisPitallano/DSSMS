using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface ICourseManager : IRepository<CourseMasterList,CourseRequest>
    {
        Task<IEnumerable<CourseMasterList>> GetCourseListByCategoryId(int? typeId=null);
        Task<IEnumerable<AvailableCourse>> GetAvailableCoursesByCategoryId(int categoryId);
        Task<CourseMasterList> SetStatus(int id, bool isDeleted, string userName);
        Task<CourseMasterList> DeleteCourse(int id);
    }
}
