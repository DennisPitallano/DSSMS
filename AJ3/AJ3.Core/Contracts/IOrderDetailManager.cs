using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IOrderDetailManager : IRepository<OrderDetail,OrderDetailRequest>
    {
        Task<IEnumerable<OrderDetailList>> GetOrderDetailsByOrderId(int orderId);
    }
}
