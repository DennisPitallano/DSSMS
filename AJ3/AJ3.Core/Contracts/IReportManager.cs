using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IReportManager
    {
        Task<IEnumerable<OrderPaymentReportList>> GetPaymentsReportDetail(OrderPaymentReportListFilter filter);
    }
}
