using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Infrastructure.Configs;
using AJ3.WebApp.Infrastructure.Extensions;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AJ3.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ReportController : Controller
    {
        private readonly SystemSetupConfiguration _systemSetupConfiguration;
        private readonly ICompanyProfileManager _companyProfileManager;
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;
        private readonly IStudentCourseManager _studentCourseManager;
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly IOrderPaymentManager _orderPaymentManager;
        public ReportController(IOptionsSnapshot<SystemSetupConfiguration> systemSetupConfiguration,
            ICompanyProfileManager companyProfileManager, IStudentManager studentManager, IMapper mapper,
            IStudentCourseManager studentCourseManager, IOrderDetailManager orderDetailManager, IOrderPaymentManager orderPaymentManager)
        {
            _companyProfileManager = companyProfileManager;
            _studentManager = studentManager;
            _mapper = mapper;
            _studentCourseManager = studentCourseManager;
            _orderDetailManager = orderDetailManager;
            _orderPaymentManager = orderPaymentManager;
            _systemSetupConfiguration = systemSetupConfiguration.Value;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name.Contains("AJ3"))
            {
                return RedirectToAction("Index", "Home");
            }
            var filter = new OrderPaymentReportListFilter
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now
            };
            ViewBag.SystemConfig = _systemSetupConfiguration;
            ViewBag.CompanyProfile = await _companyProfileManager.GetByIdAsync(0).ConfigureAwait(false);
            return View(filter);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaymentReportResults(OrderPaymentReportListFilter model)
        {
            return await Task.FromResult(ViewComponent("OrderPaymentReportList", model)).ConfigureAwait(false);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Certificate(int orderId)
        {
            ViewBag.OrderId = orderId;
            ViewBag.CompanyProfile = await _companyProfileManager.GetByIdAsync(0).ConfigureAwait(false);
            var studentCourse = _mapper.Map<StudentCourseDetailViewModel>(await _studentCourseManager.GetDetails(null, orderId)
                    .ConfigureAwait(false));
            ViewBag.StudentCourseDetail = studentCourse;
            var studentDetails =
                _mapper.Map<StudentDetailViewModel>(await _studentManager.GetByIdAsync(studentCourse.StudentId).ConfigureAwait(false));
            ViewBag.Student = studentDetails;
            ViewBag.OrderDetails = _mapper.Map<IEnumerable<OrderDetailListViewModel>>(
                await _orderDetailManager.GetOrderDetailsByOrderId(orderId).ConfigureAwait(false));
            var orderPayments = _mapper.Map<IEnumerable<StudentOrderPaymentViewModel>>(
                await _orderPaymentManager.GetOrderPaymentHistoryByOrderId(orderId).ConfigureAwait(false));
            ViewBag.OrderPayments = orderPayments;
            ViewBag.OrderPaymentsSummary = orderPayments.Sum(a => a.Amount).ToPhFormatCurrency();
            return View(studentCourse);
        }
    }
}
