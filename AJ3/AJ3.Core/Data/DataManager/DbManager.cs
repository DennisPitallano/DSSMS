using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class DbManager: DbFactoryBase, IDbManager
    {
        public DbManager(IConfiguration config) : base(config)
        {
        }

        public async Task<bool> BackUpDb(string Location)
        {
            return await DbQuerySingleAsync<bool>("[dbo].[usp_DbBackUp]", new
            {
                Location
            }).ConfigureAwait(false);
        }
    }
}
