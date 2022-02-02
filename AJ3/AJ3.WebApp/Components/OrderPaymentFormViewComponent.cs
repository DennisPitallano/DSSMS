using System.Threading.Tasks;
using AJ3.Core.Common;
using AJ3.Core.Contracts;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class OrderPaymentFormViewComponent : ViewComponent
    {
        private readonly IOrderPaymentManager _orderPaymentManager;
        private readonly IMapper _mapper;
        public OrderPaymentFormViewComponent(IOrderPaymentManager orderPaymentManager, IMapper mapper)
        {
            _orderPaymentManager = orderPaymentManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int studentId,decimal balance = 0, FormView view = FormView.New)
        {
            //note : id value vary from type of action
            //      f new id = OrderId and Id =0 if update Id = id and OrderId from db
            var model = new OrderPaymentFormViewModel
            {
                Id =0,
                OrderId = id,
                StudentId = studentId,
                Amount = balance,
                BalanceAmount = 0,
                Type = "New"
            };
            if (view ==FormView.Update)
            {
                var orderPayment =
                    _mapper.Map<OrderPaymentFormViewModel>(await _orderPaymentManager.GetByIdAsync(id)
                        .ConfigureAwait(false));
                orderPayment.StudentId = studentId;
                orderPayment.BalanceAmount = balance;
                orderPayment.Type = "Update";
                return await Task.FromResult<IViewComponentResult>(View("Form",orderPayment)).ConfigureAwait(false);
            }
            return await Task.FromResult<IViewComponentResult>(View("Form",model)).ConfigureAwait(false);
        }
    }
}
