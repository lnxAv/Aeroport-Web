using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionVols.Models;
using DAL;

namespace GestionVols.Controllers
{
    public class TestController : Controller
    {

        
        // GET: Test
        [HttpGet]
        public ActionResult Index()
        {
            DB.disconnect();
            var contextEF = DB.Instance();

            var villes = contextEF.Vols.ToList();
            return View(villes);
        }
    }
}