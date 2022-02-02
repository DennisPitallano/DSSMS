using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class OrderAdjustmentManager : DbFactoryBase, IOrderAdjustmentManager
    {
        public OrderAdjustmentManager(IConfiguration config) : base(config)
        {
        }

        public async Task<OrderAdjustment> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<OrderAdjustment>("[dbo].[usp_OrderAdjustment_Get]", new
            {
                Id = id
            }).ConfigureAwait(false);
        }

        public async Task<OrderAdjustment> CreateAsync(OrderAdjustmentRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderAdjustment> UpdateAsync(OrderAdjustmentRequest entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderAdjustment>> GetOrderAdjustmentByOrderId(int oderId)
        {
            return await DbQueryAsync<OrderAdjustment>("[dbo].[usp_OrderAdjustment_Get]", new
            {
                OrderId = oderId
            }).ConfigureAwait(false);
        }
    }
}
