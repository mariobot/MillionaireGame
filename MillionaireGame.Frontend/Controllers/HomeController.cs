using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillionaireGame.Frontend.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Game(string PlayerName)
        {
            Session["name"] = PlayerName;
            return View();
        }
    }
}