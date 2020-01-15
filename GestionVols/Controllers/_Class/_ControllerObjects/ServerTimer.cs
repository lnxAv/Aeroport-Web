using DAL;
using GestionVols.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Timers;
using System.Web;

namespace GestionVols.Controllers.Class.ControllerObjects
{
    public class ServerTimer : TimedApp
    {
        public int repeated = 0;
        public ConcurrentQueue<Notification> listNotification { get { return messengerApp.pingingList; } }
        private MessengerApp messengerApp;

        public ServerTimer(TimeSpan _timeToAction, bool _autoReset = false) : base(_timeToAction, _autoReset)
        {
            messengerApp = new MessengerApp();
        }

        public override void TimedAction()
        {
            //RefreshDB();
            messengerApp.LaunchNotification();
            messengerApp.MAJVolsCedules();
            repeated++;
        }

        public void RefreshDB()
        {
            var context = ((IObjectContextAdapter)DB2.Aeroport()).ObjectContext;
            var refreshableObjects = (from entry in context.ObjectStateManager.GetObjectStateEntries(
                                                       EntityState.Added
                                                       | EntityState.Deleted
                                                       | EntityState.Modified
                                                       | EntityState.Unchanged)
                                      where entry.EntityKey != null
                                      select entry.Entity).ToList();

            context.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }

        public void WelcomeUser(DAL.Notification notification)
        {
            messengerApp.WelcomeMessage(notification);
        }
    }
}