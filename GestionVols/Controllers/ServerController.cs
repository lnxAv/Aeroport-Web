using DAL;
using GestionVols.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionVols.Controllers
{
    public class ServerController : Controller
    {
        // GET: Server
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewTimer()
        {
            ViewBag.Time = (HttpContext.Application["ServerTimer"] as Class.ControllerObjects.ServerTimer).timerActionCountdown;
            ViewBag.Repeated = (HttpContext.Application["ServerTimer"] as Class.ControllerObjects.ServerTimer).repeated;
            ViewBag.Interogated = (HttpContext.Application["ServerTimer"] as Class.ControllerObjects.ServerTimer).interogated;
            return PartialView("PartialTimerView");
        }

    }
}