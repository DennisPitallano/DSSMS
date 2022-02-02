using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class StudentCourseManager :DbFactoryBase, IStudentCourseManager
    {
        public StudentCourseManager(IConfiguration config) : base(config)
        {
        }
        public async Task<StudentCourse> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentCourse> CreateAsync(StudentCourseRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentCourse> UpdateAsync(StudentCourseRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentCourseDetail> GetDetails(int? studentId, int? orderId = null)
        {
            return await DbQuerySingleAsync<StudentCourseDetail>("[dbo].[usp_StudentCourse_Get]", new
            {
                StudentId = studentId,
                OrderId = orderId
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<StudentCourseDetail>> GetCourses(int? studentId, int? orderId = null)
        {
            return await DbQueryAsync<StudentCourseDetail>("[dbo].[usp_StudentCourse_Get]", new
            {
                StudentId = studentId,
                OrderId = orderId
            }).ConfigureAwait(false);
        }

        public async Task<StudentCourse> UpdateCourseStatus(int id, string status, string userName)
        {
            return await DbQuerySingleAsync<StudentCourse>("[dbo].[usp_Student_Course_Update_Status]", new
            {
                Id = id,
                Status = status,
                UserName=userName
            }).ConfigureAwait(false);
        }

        public async Task<DbResult> CancelStudentCourse(CancelStudentCourseRequest cancelStudentCourseRequest)
        {
            return await DbQuerySingleAsync<DbResult>("[dbo].[usp_Cancel_Student_Course]", cancelStudentCourseRequest).ConfigureAwait(false);
        }

        public async Task<DbResult> ChangeStudentCourse(ChangeStudentCourseRequest changeStudentCourseRequest)
        {
            return await DbQuerySingleAsync<DbResult>("[dbo].[usp_ChangeCourse_Student_Course]", changeStudentCourseRequest).ConfigureAwait(false);
        }
    }
}
