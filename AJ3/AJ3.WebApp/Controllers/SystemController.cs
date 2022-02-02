using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AJ3.Core.Contracts;
using AJ3.Core.DTO;
using AJ3.WebApp.Infrastructure.Configs;
using AJ3.WebApp.Models.Setting;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AJ3.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class SystemController : Controller
    {
        private readonly SystemSetupConfiguration _systemSetupConfiguration;
        private readonly ICompanyProfileManager _companyProfileManager;
        private readonly IMapper _mapper;
        public SystemController(IOptionsSnapshot<SystemSetupConfiguration> systemSetupConfiguration, ICompanyProfileManager companyProfileManager, IMapper mapper)
        {
            _companyProfileManager = companyProfileManager;
            _mapper = mapper;
            _systemSetupConfiguration = systemSetupConfiguration.Value;
        }

        public async Task<IActionResult> Index()
        {
            CompanyProfileViewModel model;
            var fromDb = await _companyProfileManager.GetByIdAsync(0);
            if (fromDb == null)
            {
                //insert from config
                var result = await _companyProfileManager.CreateAsync(new CompanyProfileRequest
                {
                    Address = _systemSetupConfiguration.Address,
                    Manager = _systemSetupConfiguration.Manager,
                    MobileNumber = _systemSetupConfiguration.MobileNumber,
                    Name = _systemSetupConfiguration.CompanyName,
                    PhoneNumber = _systemSetupConfiguration.PhoneNumber,
                    TagLine = _systemSetupConfiguration.TagLine,
                    UserName = User.Identity?.Name??"SYSTEM_USER"
                });
                model = _mapper.Map<CompanyProfileViewModel>(result);
            }
            else
            {
                model = _mapper.Map<CompanyProfileViewModel>(fromDb);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCompanyProfile(CompanyProfileViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _companyProfileManager.UpdateAsync(new CompanyProfileRequest
                    {
                        Id = model.Id,
                        Address = model.Address,
                        Manager = model.Manager,
                        MobileNumber = model.MobileNumber,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        TagLine = model.TagLine,
                        UserName = User.Identity?.Name??"SYSTEM_USER"
                    });
                   var returnMap = _mapper.Map<CompanyProfileViewModel>(result);
                    return Ok(new
                    {
                        Status = "Success",
                        Description = $"Company Profile for {returnMap.Name} has been saved!"
                    });
                }
                return Ok(new
                {
                    Status = "Success",
                    Description = $"Failed to save company profile!"
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Description = $"Failed to save company profile! {e.Message}"
                });
            }
        }
    }
}
