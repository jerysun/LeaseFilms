using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaseFilms.Controllers
{
    [AllowAnonymous]
    [OutputCache(Duration = 50, Location = System.Web.UI.OutputCacheLocation.Server)]
    public class HomeController : Controller
    {
        // NO Caching!
        // [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        // do the new cache again every 50 seconds to keep sync
        // [OutputCache(Duration = 50, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}