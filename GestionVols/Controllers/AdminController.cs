using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionVols.Models;
using System.IO;
using Newtonsoft.Json;
using DAL;
using GestionVols.Controllers.Class.ControllerObjects;

namespace GestionVols.Controllers
{
    public class AdminController : Controller
    {
        ServerTimer _APPTIMER;


        // GET: Adminif
        public ActionResult DashBoard()
        {
            return View("DashBoard", "~/Views/Shared/_AdminLayout.cshtml",null);
        }

        public ActionResult TimerControl()
        {
            _APPTIMER = (ServerTimer)(HttpContext.ApplicationInstance).Application["ServerTimer"];

            if (_APPTIMER.IsRunning)
            {
                _APPTIMER.StopTask();
            }
            else
            {
                _APPTIMER.StartTask();
            }

            return View("DashBoard", "~/Views/Shared/_AdminLayout.cshtml", null);
        }

        public ActionResult Charger_calendrier()
        {
            return View("Charger_calendrier", "~/Views/Shared/_AdminLayout.cshtml", null);
        }

        [HttpPost]

        public ActionResult Import(HttpPostedFileBase jsonFile)
        {
            
            if ( jsonFile == null || !Path.GetFileName(jsonFile.FileName).EndsWith(".json"))
            {
                ViewBag.Error = "Invalid file type";
            }
            else
            {
                jsonFile.SaveAs(Server.MapPath("~/JSONFiles/" + Path.GetFileName(jsonFile.FileName)));

                StreamReader streamReader = new StreamReader(Server.MapPath("~/JSONFiles/" + Path.GetFileName(jsonFile.FileName)));
                string data = streamReader.ReadToEnd();

          
                List<Vol> vols = JsonConvert.DeserializeObject<List<Vol>>(data);

                



                vols.ForEach(v =>
                {
                    Vol vol = new Vol()
                    {
                        Numero = v.Numero,
                        Heure_Depart = v.Heure_Depart,
                        Heure_Arrivee = v.Heure_Arrivee,
                        Id_Date_Depart = v.Id_Date_Depart,
                        Id_Date_Arrivee = v.Id_Date_Arrivee,
                        Code_CompAerien = v.Code_CompAerien,

                        Code_IATA_Aeroport_Arrivee = v.Code_IATA_Aeroport_Arrivee,
                        Code_IATA_Aeroport_Depart = v.Code_IATA_Aeroport_Depart,
                    };

                    if( DB3.Aeroport().Vols != null && DB3.Aeroport().Vols.Find(vol.Numero,vol.Id_Date_Depart) == null){
                        DB3.Aeroport().Vols.Add(vol);
                        DB3.Aeroport().SaveChanges();
                    }
                    

                });
                

                ViewBag.Success = "Success";



               

            }
            return View("Charger_calendrier", "~/Views/Shared/_AdminLayout.cshtml", null);
        }


    }
}