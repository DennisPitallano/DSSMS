using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class CourseManager : DbFactoryBase, ICourseManager
    {
        public CourseManager(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<CourseMasterList>> GetCourseListByCategoryId(int? typeId = null)
        {
            return await DbQueryAsync<CourseMasterList>("[dbo].[usp_CourseMasterList_Get]",new
            {
                CourseCategoryId=typeId
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<AvailableCourse>> GetAvailableCoursesByCategoryId(int categoryId)
        {
            return await DbQueryAsync<AvailableCourse>("[dbo].[usp_Courses_Get_By_CategoryId]", new
            {
                CategoryId = categoryId
            }).ConfigureAwait(false);
        }

        public async Task<CourseMasterList> SetStatus(int id, bool isDeleted, string userName)
        {
            return await DbQuerySingleAsync<CourseMasterList>("[dbo].[usp_Course_Delete]", new
            {
                Id = id,
                IsDeleted=isDeleted,
                UserName = userName
            }).ConfigureAwait(false);
        }

        public async Task<CourseMasterList> DeleteCourse(int id)
        {
            return await DbQuerySingleAsync<CourseMasterList>("[dbo].[usp_Course_Delete]", new
            {
                Id = id,
                IsPhysical = true
            }).ConfigureAwait(false);
        }

        public async Task<CourseMasterList> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<CourseMasterList>("[dbo].[usp_CourseMasterList_Get]", new
            {
                CourseId = id
            }).ConfigureAwait(false);
        }

        public async Task<CourseMasterList> CreateAsync(CourseRequest entity)
        {
            return await DbQuerySingleAsync<CourseMasterList>("[dbo].[usp_Course_Insert]", new
            {
                entity.Name,
                entity.Description,
                entity.CategoryId,
                entity.Hours,
                entity.UserName,
                entity.UnitPrice,
                entity.Discount
            }).ConfigureAwait(false);
        }

        public async Task<CourseMasterList> UpdateAsync(CourseRequest entity)
        {
            return await DbQuerySingleAsync<CourseMasterList>("[dbo].[usp_Course_Update]", entity)
                .ConfigureAwait(false);
        }
    }
}
