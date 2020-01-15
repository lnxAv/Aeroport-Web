using GestionVols.Controllers.Class.ControllerObjects;
using GestionVols.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Twilio.Rest.Lookups.V1;

namespace GestionVols.Controllers
{
    public class InscriptionController : Controller
    {

        public NotificationApp _APP;

        public InscriptionController()
        {
            _APP = new NotificationApp();
        }

        // GET: Inscription
        public ActionResult Index(string id)
        {
            ViewBag.DefaultNumVol = id ?? "";
            return View();
        }


        [HttpPost]
        public ActionResult Subscribe(DAL.Notification notification)
        {
            ViewBag.RegisterResult = RegisterNotifcation(ref notification);
            TempData["PreviousNotification"] = notification;
            return View("Index");
        }

        private bool RegisterNotifcation(ref DAL.Notification notification)
        {

            List<string> _NotificationMessage = new List<string>();

            if (!isNotificationValid(ref notification, ref _NotificationMessage))
            {
                TempData["NotificationMessage"] = _NotificationMessage;
                return false;
            }
            else if (!isNotificationExist(notification, ref _NotificationMessage))
            {
                notification.Num_Vol = "";
                TempData["NotificationMessage"] = _NotificationMessage;
                return false;
            }

           var phoneNumber = PhoneNumberResource.Fetch(
           countryCode: "US",
           pathPhoneNumber: new Twilio.Types.PhoneNumber(notification.Num_Phone)
       );
            notification.Num_Phone = phoneNumber.PhoneNumber.ToString();

            notification.Date_Notification = DateTime.Now;
            _APP.AddNotification(notification);

            _NotificationMessage.Add("L'inscription au vols est enregistrer.");
            TempData["NotificationMessage"] = _NotificationMessage;
            return true;
        }

        private bool isNotificationValid(ref DAL.Notification notification, ref List<string> _messages)
        {
            if (notification.Langue == null || !_APP.config.Language.Contains(notification.Langue))
            {
                _messages.Add("Langue correspondante ou manquante.");
                notification.Langue = "";
            }

            if (notification.Num_Phone == null || !Regex.IsMatch(notification.Num_Phone, NotificationApp.PhoneRegex))
            {
                _messages.Add("Telephone non correspondante ou manquante.");
                notification.Num_Phone = "";
            }

            if (notification.Num_Vol == null || !Regex.IsMatch(notification.Num_Vol, NotificationApp.NumVolRegex))
            {
                _messages.Add("Numero de Vol non correspondante ou manquante.");
                notification.Num_Vol = "";
            }


            if (_messages.Count > 0)
            {
                return false;
            }

            return true;
        }

        private bool isNotificationExist(DAL.Notification notification, ref List<string> _messages)
        {

            var dbContext = DB2.Aeroport();
            /*if (dbContext.VolsCedules.Find( dbContext.Vols.Find(notification.Num_Vol) ) == null )
            {
                _messages.Add("Le vols na pas etes trouver.");
                return false;
            }*/
            if ((from nt in dbContext.Notifications
                 where nt.Num_Phone == notification.Num_Phone && nt.Num_Vol == notification.Num_Vol
                 select nt).Count() > 0)
            {
                _messages.Add("Le vol " + notification.Num_Vol +" est deja inscript.");
                return false;
            }

            return true;
        }
    }
}