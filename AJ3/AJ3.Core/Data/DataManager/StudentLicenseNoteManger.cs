using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class StudentLicenseNoteManger : DbFactoryBase, IStudentLicenseNoteManager
    {
        public StudentLicenseNoteManger(IConfiguration config) : base(config)
        {
        }

        public async Task<StudentLicenseNoteListItem> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentLicenseNoteListItem> CreateAsync(StudentLicenseNote entity)
        {
            return await DbQuerySingleAsync<StudentLicenseNoteListItem>("[dbo].[usp_StudentLicenseNote_Insert]",new
            {
                entity.StudentId,
                entity.LicenseNoteId,
                UserName=entity.UpdatedBy
            }).ConfigureAwait(false);
        }

        public async Task<StudentLicenseNoteListItem> UpdateAsync(StudentLicenseNote entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentLicenseNoteListItem>> GetStudentLicenseNotesByStudentId(int studentId)
        {
            return await DbQueryAsync<StudentLicenseNoteListItem>("[dbo].[usp_StudentLicenseNote_Get]",new
            {
                StudentId = studentId
            }).ConfigureAwait(false);
        }
    }
}
