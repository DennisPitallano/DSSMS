using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Order;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class OrderAdjustmentListViewComponent : ViewComponent
    {
        private readonly IOrderAdjustmentManager _orderAdjustmentManager;
        private readonly IMapper _mapper;
        public OrderAdjustmentListViewComponent(IOrderAdjustmentManager orderAdjustmentManager, IMapper mapper)
        {
            _orderAdjustmentManager = orderAdjustmentManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var adjustments = _mapper.Map<IEnumerable<OrderAdjustmentViewModel>>(
                await _orderAdjustmentManager.GetOrderAdjustmentByOrderId(orderId).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("List",adjustments)).ConfigureAwait(false);
        }
    }
}