using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class OrderDetailListViewComponent : ViewComponent
    {
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly IMapper _mapper;
        public OrderDetailListViewComponent(IMapper mapper, IOrderDetailManager orderDetailManager)
        {
            _mapper = mapper;
            _orderDetailManager = orderDetailManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var orderDetails = _mapper.Map<IEnumerable<OrderDetailListViewModel>>(
                await _orderDetailManager.GetOrderDetailsByOrderId(orderId).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("OrderDetail",orderDetails)).ConfigureAwait(false);
        }
    }
}
