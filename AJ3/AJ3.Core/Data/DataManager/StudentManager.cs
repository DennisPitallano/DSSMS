using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class StudentManager :DbFactoryBase, IStudentManager
    {
        public StudentManager(IConfiguration config) : base(config)
        {
        }
        public async Task<Student> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<Student>("[dbo].[usp_Student_Get]", new
            {
                Id = id
            }).ConfigureAwait(false);
        }

        public async Task<Student> CreateAsync(StudentRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> UpdateAsync(StudentRequest entity)
        {
            return await DbQuerySingleAsync<Student>("[dbo].[usp_Student_Update]", entity).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> UpdateStatus(int id, bool isDeleted, string userName)
        {
            return await DbQuerySingleAsync<Student>("[dbo].[usp_Student_Update_To_Deleted]", new
            {
                Id = id,
                IsDeleted = isDeleted,
                UserName=userName
            }).ConfigureAwait(false);
        }

        public async Task<Student> CreateNewStudent(NewStudent model)
        {
            return await DbQuerySingleAsync<Student>("[dbo].[usp_CreateStudent]", model).ConfigureAwait(false);
        }

        public async Task<IEnumerable<StudentMasterList>> StudentMasterList(StudentMasterListFilter filter)
        {
            return await DbQueryAsync<StudentMasterList>("[dbo].[usp_Student_Masterlist_Get]",filter).ConfigureAwait(false);
        }
    }
}
