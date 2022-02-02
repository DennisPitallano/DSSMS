using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class OrderPaymentHistoryViewComponent : ViewComponent
    {
        private readonly IOrderManager _orderManager;
        private readonly IMapper _mapper;
        public OrderPaymentHistoryViewComponent(IOrderManager orderManager, IMapper mapper)
        {
            _orderManager = orderManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int studentId)
        {
            var studentOrders = _mapper.Map<IEnumerable<StudentOrderViewModel>>(
                await _orderManager.GetOrdersByStudentId(studentId).ConfigureAwait(false));
            return await Task.FromResult<IViewComponentResult>(View("Payments",studentOrders)).ConfigureAwait(false);
        }
    }
}
