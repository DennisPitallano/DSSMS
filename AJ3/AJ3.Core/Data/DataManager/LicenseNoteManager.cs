using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class LicenseNoteManager :DbFactoryBase, ILicenseNoteManager
    {
        public LicenseNoteManager(IConfiguration config) : base(config)
        {
        }

        public async Task<LicenseNoteListItem> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<LicenseNoteListItem> CreateAsync(LicenseNote entity)
        {
            throw new NotImplementedException();
        }

        public async Task<LicenseNoteListItem> UpdateAsync(LicenseNote entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LicenseNote>> GetLicenseNoteByType(string type)
        {
            return await DbQueryAsync<LicenseNote>("[dbo].[usp_LicenseNote_Get_By_Type]",new
            {
                Type = type
            }).ConfigureAwait(false);
        }
    }
}
