using AJ3.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Infrastructure.Configs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AJ3.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICourseCategoryManager _courseCategoryManager;
        private readonly SystemSetupConfiguration _systemSetupConfiguration;
        private readonly ICompanyProfileManager _companyProfileManager;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, ICourseCategoryManager courseCategoryManager,
            IOptionsSnapshot<SystemSetupConfiguration> systemSetupConfiguration, ICompanyProfileManager companyProfileManager, IMapper mapper)
        {
            _logger = logger;
            _courseCategoryManager = courseCategoryManager;
            _companyProfileManager = companyProfileManager;
            _mapper = mapper;
            _systemSetupConfiguration = systemSetupConfiguration.Value;
        }

        public async Task<IActionResult> Index()
        {
            var fromDb = await _companyProfileManager.GetByIdAsync(0);
            if (fromDb == null)
            {
                var result = await _companyProfileManager.CreateAsync(new CompanyProfileRequest
                {
                    Address = _systemSetupConfiguration.Address,
                    Manager = _systemSetupConfiguration.Manager,
                    MobileNumber = _systemSetupConfiguration.MobileNumber,
                    Name = _systemSetupConfiguration.CompanyName,
                    PhoneNumber = _systemSetupConfiguration.PhoneNumber,
                    TagLine = _systemSetupConfiguration.TagLine,
                    UserName = User.Identity?.Name ?? "SYSTEM_USER"
                });
            }
            return View();
        }

        public async Task<IActionResult> Privacy([FromServices] IDbManager dbManager)
        {
            await dbManager.BackUpDb("C:\\Users\\denni\\OneDrive\\Documents\\cert\\TPS.bak");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Students()
        {
            return View();
        }
    }
}
