using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Infrastructure.Configs;
using AJ3.WebApp.Models.Student;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AJ3.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;
        private readonly ICompanyProfileManager _companyProfileManager;
        public OrdersController(IOrderManager orderManager, IMapper mapper, ILogger<OrdersController> logger,
            ICompanyProfileManager companyProfileManager)
        {
            _orderManager = orderManager;
            _mapper = mapper;
            _logger = logger;
            _companyProfileManager = companyProfileManager;
        }

        public async Task<IActionResult> Index()
        {
            var orderMasterList = _mapper.Map<IEnumerable<OrderMasterListViewModel>>(
                await _orderManager.GetOrderMasterList(new OrderMasterListFilter()).ConfigureAwait(false));
            return View(orderMasterList);
        }

        [HttpGet,Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.OrderId = id;
            ViewBag.CompanyProfile = await _companyProfileManager.GetByIdAsync(0).ConfigureAwait(false);
            return View();
        }
    }
}
