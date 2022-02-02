using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class OrderPaymentHistoryDetailViewComponent : ViewComponent
    {
        private readonly IOrderPaymentManager _orderPaymentManager;
        private readonly IMapper _mapper;
        public OrderPaymentHistoryDetailViewComponent(IMapper mapper, IOrderPaymentManager orderPaymentManager)
        {
            _mapper = mapper;
            _orderPaymentManager = orderPaymentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId,OrderView view =OrderView.Student)
        {
            var orderPayments = _mapper.Map<IEnumerable<StudentOrderPaymentViewModel>>(
                await _orderPaymentManager.GetOrderPaymentHistoryByOrderId(orderId).ConfigureAwait(false));
            if (view==OrderView.Order)
            {
                return await Task.FromResult<IViewComponentResult>(View("OrderDetailPayments",orderPayments)).ConfigureAwait(false);
            }
            return await Task.FromResult<IViewComponentResult>(View("PaymentDetail",orderPayments)).ConfigureAwait(false);
        }
    }
}
