using AirFlight.Repository;
using AirFlight.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirFlight.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var storeLocation = Server.MapPath("~/App_Data/");
            var service = new AirCraftService(new UnitOfWork(storeLocation));
            var airCrafts = service.GetAllAirCrafts();
            return View();
        }
    }
}