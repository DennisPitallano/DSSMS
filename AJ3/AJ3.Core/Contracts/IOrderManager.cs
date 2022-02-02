using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface IOrderManager : IRepository<Order,OrderRequest>
    {
        Task<IEnumerable<Order>> GetOrdersByStudentId(int studentId);
        Task<IEnumerable<OrderMasterList>> GetOrderMasterList(OrderMasterListFilter filter);
    }
}
