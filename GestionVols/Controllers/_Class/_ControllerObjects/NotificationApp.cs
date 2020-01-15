using GestionVols.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GestionVols.Controllers.Class.ControllerObjects
{
    public class NotificationApp
    {
        public Config config;
        ServerTimer _APPTIMER;
        public NotificationApp()
        {
             _APPTIMER = (ServerTimer)(HttpContext.Current.ApplicationInstance).Application["ServerTimer"];
            config = new Config();
        }

        public static string PhoneRegex { get; private set; } = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
        public static string NumVolRegex { get; private set; } = @"(?<![A-Z])[A-Z]{2}\d{3,4}(?!\d)";

        public class Config
        {
            public string[] Language { get; private set; } = new string[]
            {
                "Francais",
                "Anglais"
            };
        }

        public void AddNotification(DAL.Notification notification)
        {
            var dbContext = DB.Instance();
            notification.Date_Notification = DateTime.Now;
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();
            _APPTIMER.WelcomeUser(notification);
            _APPTIMER.listNotification.Enqueue(notification);
        }
    }
}