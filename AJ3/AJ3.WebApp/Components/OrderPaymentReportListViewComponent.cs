using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Infrastructure.Extensions;
using AJ3.WebApp.Models.Report;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AJ3.WebApp.Components
{
    public class OrderPaymentReportListViewComponent : ViewComponent
    {
        private readonly IReportManager _reportManager;
        private readonly IMapper _mapper;
        public OrderPaymentReportListViewComponent(IMapper mapper, IReportManager reportManager)
        {
            _mapper = mapper;
            _reportManager = reportManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(OrderPaymentReportListFilter filter)
        {
            filter.DateTo = filter.DateTo.AddHours(12);
            var orderReports = _mapper.Map<IEnumerable<OrderPaymentReportListViewModel>>(
                await _reportManager.GetPaymentsReportDetail(filter).ConfigureAwait(false));
            var orderPaymentReportListViewModels = orderReports.ToList();
            var totalPaid = orderPaymentReportListViewModels.Sum(a => a.Amount);
            var totalPaidDiscount = orderPaymentReportListViewModels.Where(a=>a.OrNumber.Equals("DISCOUNT")).Sum(a => a.Amount);
            var totalCollection = (totalPaid - totalPaidDiscount);
            ViewBag.TotalPaidAmount = totalPaid.ToPhFormatCurrency();
            ViewBag.TotalPaidDiscount = totalPaidDiscount.ToPhFormatCurrency();
            ViewBag.TotalPaidCollection = totalCollection.ToPhFormatCurrency();
            return await Task.FromResult<IViewComponentResult>(View("PaymentReportList", orderPaymentReportListViewModels)).ConfigureAwait(false);
        }
    }
}
