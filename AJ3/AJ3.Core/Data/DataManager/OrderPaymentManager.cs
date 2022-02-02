using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;
using Microsoft.Extensions.Configuration;

namespace AJ3.Core.Data.DataManager
{
    public class OrderPaymentManager : DbFactoryBase, IOrderPaymentManager
    {
        public OrderPaymentManager(IConfiguration config) : base(config)
        {
        }

        public async Task<OrderPayment> GetByIdAsync(object id)
        {
            return await DbQuerySingleAsync<OrderPayment>("[dbo].[usp_OrderPayment_Get]", new
            {
                Id = id
            }).ConfigureAwait(false);
        }

        public async Task<OrderPayment> CreateAsync(OrderPaymentRequest entity)
        {
            return await DbQuerySingleAsync<OrderPayment>("[dbo].[usp_OrderPayment_Insert]", new
            {
                entity.OrderId,
                entity.OrNumber,
                entity.PaymentId,
                entity.Amount,
                entity.UserName
            }).ConfigureAwait(false);
        }

        public async Task<OrderPayment> UpdateAsync(OrderPaymentRequest entity)
        {
            return await DbQuerySingleAsync<OrderPayment>("[dbo].[usp_OrderPayment_Update]", new
            {
                entity.Id,
                entity.OrderId,
                entity.OrNumber,
                entity.Amount,
                entity.UserName
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<OrderPayment>> GetOrderPaymentHistoryByOrderId(int orderId)
        {
            return await DbQueryAsync<OrderPayment>("[dbo].[usp_OrderPaymentHistory_Get]", new
            {
                OrderId = orderId
            }).ConfigureAwait(false);
        }
    }
}
