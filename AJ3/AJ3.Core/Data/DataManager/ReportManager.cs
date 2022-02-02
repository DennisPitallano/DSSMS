using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class ReportManager: DbFactoryBase, IReportManager
    {
        public ReportManager(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<OrderPaymentReportList>> GetPaymentsReportDetail(OrderPaymentReportListFilter filter)
        {
            return await DbQueryAsync<OrderPaymentReportList>("[dbo].[usp_GetPaymentReport]", filter).ConfigureAwait(false);
        }
    }
}
