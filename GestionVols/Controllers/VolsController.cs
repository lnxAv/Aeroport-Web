using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionVols.Models;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace GestionVols.Controllers
{
    public class VolsController : Controller
    {
        // GET: Vols
        public ActionResult Index(string cible)
        {

            List<VolPlannifie> listeVols = new List<VolPlannifie>();

            DateTime demain = DateTime.Now.AddDays(1);
            DateTime aujourdhui = DateTime.Now.AddHours(16);



            if (cible == null || cible == "Aujourd'hui")
            {
                ViewBag.VolsDeparts = VolDAO.listeVolsDepartsAujourd().ToList();
                ViewBag.VolsArrives = VolDAO.listeVolsArrivesAujourd().ToList(); 
            }else if (cible == "Demain")
            {
                ViewBag.VolsDeparts = VolDAO.listeVolsDepartsDemain().ToList();
                ViewBag.VolsArrives = VolDAO.listeVolsArrivesDemain().ToList();
            }



            

             




            return View();
        }

        // GET: ListeVolsDepart
        public ActionResult ListeVolsDepart(string cible)
        {



            if (cible == null || cible == "Aujourd'hui")
            {
                ViewBag.VolsDeparts = VolDAO.listeVolsDepartsAujourd().ToList();
              

            }
            else if (cible == "Demain")
            {

                ViewBag.VolsDeparts = VolDAO.listeVolsDepartsDemain().ToList();
               
            }

            return View();
        }



        /// <summary>
        /// ok
        /// </summary>
        /// <param name="cible"></param>
        /// <returns></returns>
        // GET: ListeVolsArrive
        public ActionResult ListeVolsArrive(string cible)
        {
            

            if (cible == null || cible == "Aujourd'hui")
            {
                ViewBag.VolsArrives = VolDAO.listeVolsArrivesAujourd().ToList();
               

            }
            else if (cible == "Demain")
            {

                ViewBag.VolsArrives = VolDAO.listeVolsArrivesDemain().ToList();
               
            }

            return View();
        }

        // GET: Rechercher
        [HttpGet]
        public ActionResult Rechercher(Recherche motRechercher)
        {
            var liste = VolDAO.rechercheVol(motRechercher);


            return View(liste);
        }



              
        // GET: Search
        public ActionResult Search(Search cible)
        {
            var liste = VolDAO.rechercheHome(cible);


            return View(liste);
        }


    }
}