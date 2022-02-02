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
    public class CourseCategoryManager : DbFactoryBase, ICourseCategoryManager
    {
        public CourseCategoryManager(IConfiguration config) : base(config)
        {
        }

        public async Task<CourseCategory> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseCategory> CreateAsync(CourseCategoryRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseCategory> UpdateAsync(CourseCategoryRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourseCategoryDetail>> GetCategories()
        {
            return await DbQueryAsync<CourseCategoryDetail>("[dbo].[usp_CourseCategory_Get]").ConfigureAwait(false);
        }
    }
}
