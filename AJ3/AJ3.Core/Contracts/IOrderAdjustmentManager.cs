using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IOrderAdjustmentManager : IRepository<OrderAdjustment,OrderAdjustmentRequest>
    {
        Task<IEnumerable<OrderAdjustment>> GetOrderAdjustmentByOrderId(int oderId);
    }
}
