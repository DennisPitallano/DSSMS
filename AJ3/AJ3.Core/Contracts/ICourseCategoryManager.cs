using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface ICourseCategoryManager : IRepository<CourseCategory,CourseCategoryRequest>
    {
        Task<IEnumerable<CourseCategoryDetail>> GetCategories();
    }
}
