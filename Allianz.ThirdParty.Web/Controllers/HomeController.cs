using Allianz.ThirdParty.Data.Interfaces;
using Allianz.ThirdParty.Data.Model;
using Allianz.ThirdParty.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.ThirdParty.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IThirdPartyDataService _thirdParty;

        public HomeController(ILogger<HomeController> logger, IThirdPartyDataService thirdParty)
        {
            _logger = logger;
            _thirdParty = thirdParty ?? throw new ArgumentNullException(nameof(thirdParty));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserVehicleModel thirdPartyData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long insertUserData = await _thirdParty.LogUserData(thirdPartyData.Users);
                    
                    if (insertUserData <= 0)
                    {
                        TempData["Message"] = "Third Party Data Could not be submitted at this moment. Try again later!";
                        return View();
                    }
                    var mappedData = new UserVehicle()
                    {
                        UserId = insertUserData,
                        VehicleModel = (int)thirdPartyData.VehicleMake == 1 ? (int)thirdPartyData.Honda : (int)thirdPartyData.Toyota,
                        InsuranceFee = (decimal)thirdPartyData.BodyType,
                        BodyType = thirdPartyData.BodyType.ToString(),
                        VehicleMake = (int)thirdPartyData.VehicleMake,
                        RegistrationNumber = thirdPartyData.RegistrationNumber
                    };
                    

                    if (await _thirdParty.LogThirdPartyData(mappedData))
                    {
                        TempData["Message"] = "Third Party Purchase Successfull!";
                        return View();
                    }

                    thirdPartyData.Users.UserId = insertUserData;
                    await _thirdParty.RemoveUserData(thirdPartyData.Users);
                    TempData["Message"] = "Third Party Data Could not be submitted at this moment. Try again later!";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Third Party Data Could not be submitted at this moment. Try again later!";
                    _logger.LogCritical($"ClassName: {GetThisClassName()}| MethodName: Index| Message: An exception occurred : {ex}");
                }
                
            }
            TempData["Message"] = "Invalid Data supplied! Try again.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected string GetThisClassName() => GetType().Name;
    }
}
