using System;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class CompanyProfileManager : DbFactoryBase, ICompanyProfileManager
    {
        public CompanyProfileManager(IConfiguration config) : base(config)
        {
        }

        public async Task<CompanyProfile> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<CompanyProfile>("[dbo].[usp_CompanyProfile_Get]",new
            {
            }).ConfigureAwait(false);
        }

        public async Task<CompanyProfile> CreateAsync(CompanyProfileRequest entity)
        {
            return await DbQuerySingleAsync<CompanyProfile>("[dbo].[usp_CompanyProfile_Insert]",new
            {
                entity.Name,
                entity.Address,
                entity.PhoneNumber,
                entity.MobileNumber,
                entity.TagLine,
                entity.Manager,
                entity.UserName
            }).ConfigureAwait(false);
        }

        public async Task<CompanyProfile> UpdateAsync(CompanyProfileRequest entity)
        {
            return await DbQuerySingleAsync<CompanyProfile>("[dbo].[usp_CompanyProfile_Update]",new
            {
                entity.Id,
                entity.Name,
                entity.Address,
                entity.PhoneNumber,
                entity.MobileNumber,
                entity.TagLine,
                entity.Manager,
                entity.UserName
            }).ConfigureAwait(false);
        }
    }
}
