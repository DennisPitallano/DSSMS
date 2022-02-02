using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class OrderDetailManager : DbFactoryBase, IOrderDetailManager
    {
        public OrderDetailManager(IConfiguration config) : base(config)
        {
        }
        public async Task<OrderDetail> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<OrderDetail>("[dbo].[usp_OrderDetail_Get]", new
            {
                Id = id
            }).ConfigureAwait(false);
        }

        public async Task<OrderDetail> CreateAsync(OrderDetailRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetailRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetailList>> GetOrderDetailsByOrderId(int orderId)
        {
            return await DbQueryAsync<OrderDetailList>("[dbo].[usp_OrderDetail_Get]", new
            {
                OrderId = orderId
            }).ConfigureAwait(false);
        }
    }
}
