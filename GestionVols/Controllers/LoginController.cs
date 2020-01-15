using GestionVols.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GestionVols.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autorise(Admin admin)
        {
            using (var db = DB2.Aeroport())
            {
                try
                {
                    var detail = db.Admins.Where(x => x.NomUtilisateur == admin.NomUtilisateur && x.MotPasse == admin.MotPasse).FirstOrDefault();
                    if (detail == null)
                    {
                        admin.LoginErrorMessage = "mauvais nom d'utilisateur ou mot de passe";
                        return View("Index", admin);
                    }
                    else
                    {
                        Session["Id_Admin"] = detail.Id_Admin;
                        Session["NomUtilisateur"] = detail.NomUtilisateur;
                        return RedirectToAction("DashBoard", "Admin");
                    }
                }
                catch(Exception e)
                {

                }
                
            }


            return View();
        }

        // GET: Deconnecter
        public ActionResult Deconnecter()
        {
            int userid = (int)Session["Id_Admin"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}