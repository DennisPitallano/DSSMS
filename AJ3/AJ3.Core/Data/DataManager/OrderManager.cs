using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class OrderManager :DbFactoryBase, IOrderManager
    {
        public OrderManager(IConfiguration config) : base(config)
        {
        }
        public async Task<Order> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> CreateAsync(OrderRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateAsync(OrderRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStudentId(int studentId)
        {
            return await DbQueryAsync<Order>("[dbo].[usp_Orders_By_StudentId_Get]", new
            {
                StudentId = studentId
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<OrderMasterList>> GetOrderMasterList(OrderMasterListFilter filter)
        {
            return await DbQueryAsync<OrderMasterList>("[dbo].[usp_Orders_Masterlist_Get]",filter).ConfigureAwait(false);
        }
    }
}
