using DAL;
using GestionVols.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace GestionVols.Controllers.Class.ControllerObjects
{
    public class MessengerApp
    {
        private TwilloInstance twilloInstance;
        public ConcurrentQueue<Notification> retirePhoneList { get; private set; }
        public ConcurrentQueue<Notification> notFoundPhoneList { get; private set; }
        public ConcurrentQueue<Notification> pingingList { get; private set; }

        private aeroportEntities db = DB.Instance();

        const string _FILELOCATION = "C:/Users/x/Desktop/Project_Internal/_Good_Repo/GestionVols/JSONFiles/table_VolsCedules.json";

        public MessengerApp()
        {
            twilloInstance = new TwilloInstance();

            List<Notification> tempNoti = db.Notifications.ToList();
            if (tempNoti != null)
            {
                pingingList = new ConcurrentQueue<Notification>(tempNoti);
            }
            else
            {
                pingingList = new ConcurrentQueue<Notification>();
            }

            retirePhoneList = new ConcurrentQueue<Notification>();
            notFoundPhoneList = new ConcurrentQueue<Notification>();
        }

        public void LaunchNotification()
        {
            verifyNotification();
            Retire();
            
            PingNotification();
            //NotFoundNotification();
        }

        public void WelcomeMessage(DAL.Notification notification)
        {
            twilloInstance.WriteNewNotification(notification, MessageStatus.Default);
        }

        private bool Retire()
        {
            int itemsToDequeue = retirePhoneList.Count;

                using(db = DB2.Aeroport())
            {
                try
                {
                    while (itemsToDequeue-- > 0)
                    {
                        Notification item;
                        bool isTaken = retirePhoneList.TryDequeue(out item);

                        if (isTaken)
                        {
                            var dbelem = db.Notifications.Find(item.Id);
                            if (dbelem != null)
                            {
                                twilloInstance.WriteNewNotification(item);
                                item.Statut = true;
                            }
                            else
                            {
                                notFoundPhoneList.Enqueue(item);
                            }
                        }
                        else
                        {
                            if (retirePhoneList.IsEmpty)
                            {
                                db.SaveChanges();
                                return true;
                            }

                        }
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
        }

            return true;
        }

        public void verifyNotification()
        {

            DateTime todayOffset = DateTime.Now.AddDays(-2);
            List<Notification> notifList;
            using(db = DB2.Aeroport())
            {
                notifList = db.Notifications.ToList();
                notifList.ForEach((m) =>
                {
                    if (m.Date_Notification.CompareTo(todayOffset) < 0 && m.Statut != true)
                    {
                        db.Entry(m).Property(u => u.Statut).CurrentValue = true;
                    }

                });

                db.SaveChanges();

            }
         

        }

        private bool PingNotification()
        {
            int itemsToDequeue = pingingList.Count;

                while(itemsToDequeue-- > 0)
                {
                    Notification item;
                    bool isTaken = pingingList.TryDequeue(out item);
                    if (isTaken && item.Statut != true)
                    {
                        try
                        {
                            twilloInstance.WriteNewNotification(item, MessageStatus.MAJ);
                        }
                        catch (Exception e)
                        {
                            //Erreur Twillio (Numero no correspondant, etc...)
                        }
                }
            }


            return true;
        }

        private bool NotFoundNotification()
        {
            int itemsToDequeue = notFoundPhoneList.Count;
            try
            {
                while (itemsToDequeue-- > 0)
                {
                    Notification item;
                    bool isTaken = notFoundPhoneList.TryDequeue(out item);
                    if (isTaken)
                    {
                        twilloInstance.WriteNewNotification(item);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public void MAJVolsCedules()
        {
            List<VolsCedule> vols = new List<VolsCedule>(LoadJson());

            using(db = DB2.Aeroport())
            {

                try
                {

                    for(int i = 0; i< vols.Count; i++)
                    {
                        string num = vols[i].Numero_Vol;
                        VolsCedule vol = db.VolsCedules.SingleOrDefault(m => m.Numero_Vol == num);

                        if (vol != null)
                        {
                            vols[i].Id_Date_Depart = vol.Id_Date_Depart;
                            bool IsEqual = vol.IsEqual(vols[i]);
                            if (!IsEqual)
                            {
                                vol.Date_Depart_Revisee = vols[i].Date_Depart_Revisee;
                                vol.Date_Arrivee_Revisee = vols[i].Date_Arrivee_Revisee;
                                vol.Etat = vols[i].Etat;
                                vol.Statut = vols[i].Statut;


                            }
                            else
                            {
                                vols.Remove(vols[i]);
                            }
                        }
                    }

                    db.SaveChanges();
                    UpdateNotification(vols);
                }
                catch(Exception e){
                    return;
                }
            }

        }



        private void UpdateNotification(List<VolsCedule> vols)
        {

            try
            {

                    vols.ForEach((v) =>
               {
                   List<Notification> notifUpdate = db.Notifications.Where(m => m.Num_Vol == v.Numero_Vol && m.Statut != true).ToList();
                   notifUpdate?.ForEach((m) =>
                   {

                       pingingList.Enqueue(m);
                   });
               });

            }
            catch (Exception e) {
                return;
            }

        }


        private VolsCedule[] LoadJson()
        {
            List<VolsCedule> items;
            using (StreamReader r = new StreamReader(_FILELOCATION))
            {
                string json = r.ReadToEnd();
                items = new List<VolsCedule>(JsonConvert.DeserializeObject<VolsCedule[]>(json));
            }

            using (db = DB2.Aeroport())
            {
                try
                {
                    items.ForEach((item) =>
                    {
                        VolsCedule vol = db.VolsCedules.FirstOrDefault(m => m.Numero_Vol == item.Numero_Vol);
                        if (vol != null)
                        {

                            db.Entry(vol).Property(u => u.Date_Arrivee_Revisee).CurrentValue = item.Date_Arrivee_Revisee;
                            db.Entry(vol).Property(u => u.Date_Depart_Revisee).CurrentValue = item.Date_Depart_Revisee;
                            db.Entry(vol).Property(u => u.Etat).CurrentValue = item.Etat;
                            db.Entry(vol).Property(u => u.Statut).CurrentValue = item.Statut;
                            db.Entry(vol).Property(u => u.Id_Date_Depart).CurrentValue = item.Id_Date_Depart;
                        }
                    });
                    if (items.Count > 0)
                        db.SaveChanges();
                }
                catch (Exception e)
                {

                }
            }

            return items.ToArray();
        }
    }

}