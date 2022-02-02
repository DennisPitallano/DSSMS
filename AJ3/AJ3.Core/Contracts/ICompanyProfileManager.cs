using System;
using System.Collections.Generic;
using System.Text;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface ICompanyProfileManager : IRepository<CompanyProfile,CompanyProfileRequest>
    {
    }
}
