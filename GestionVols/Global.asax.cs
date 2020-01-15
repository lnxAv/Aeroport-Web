using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GestionVols.Controllers.Class.ControllerObjects;

namespace GestionVols
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code qui s’exécute au démarrage de l’application
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            ServerTimer timer = new ServerTimer(TimeSpan.FromSeconds(15), true);
            timer.BeginTimer();
            timer.StopTask();
            Application["ServerTimer"] = timer;
        }
    }
}