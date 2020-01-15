using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using Twilio.Clients;
using System.Text.RegularExpressions;
using DAL;
using GestionVols.Models;

namespace GestionVols.Controllers.Class.ControllerObjects
{

    //Ultimatlely The SID/Token would be hidden
    public class TwilloInstance
    {
        private const string _TwilloSID = "AC359c3d534790021a35652d8c0cef9498";
        private const string _AuthToken = "0cab46fbce49c0bae50ab32a40c9cb7f";
        private const string _PhoneNumber = "+17784034748";

        private string[] _InputMessages = new string[] { "CLOSE", "FERME", "OPEN", "SUIVRE" , "FOLLOW" };
        private string[] _Answer_French = new string[] { "Bienvenue au centre de notification, pour arreter tous message envoyer FERME suivit du numero de vol."
        +"[ FERME AF1444 ]",
        "Le numero de telephone ou numero vol ne sont pas associer. Rejoigner notre site pour plus d'information & aide.",
        "Vous ne receverez plus de notification pour ce vol.",
        "Les notification sont demarrer.",
        "Le vol {0} est en status {1}, de provenance {2} vers {3} | Heure de depart {4} Heure d'arriver: {5} ."};
        
        private string[] _Answer_English = new string[] { "Welcome to the notification center, in order to stop messages send CLOSE followed by the flight number." +
            "[ CLOSE AF1444 ]",
        "The phone number or flight number are not found. Go to our website for further instructions & help.",
        "Further notifications are stopped.",
        "Notifications are started.",
        "The flight {0} is in state {1}, From {2} To {3} | Heure de depart {4} Heure d'arriver: {5} ."};

        private aeroportEntities db = DB2.Aeroport();

        public TwilloInstance()
        {
            TwilioClient.Init(_TwilloSID, _AuthToken);
        }

        private string MessageDistributor(MessageStatus messageStatus, Notification notification = null)
        {
            if(notification == null)
            {
                notification = new Notification() { Langue = "Anglais" };
            }

            switch(notification.Langue){
                case "Francais":
                    switch (messageStatus)
                    {
                        case MessageStatus.Default:
                            return _Answer_French[0];
                        case MessageStatus.Error:
                            return _Answer_French[1];
                        case MessageStatus.Closed:
                            return _Answer_French[2];
                        case MessageStatus.Followed:
                            return _Answer_French[3];
                        case MessageStatus.MAJ:
                            var vol = db.VolsCedules.SingleOrDefault(m =>m.Numero_Vol == notification.Num_Vol);
                            if(vol != null)
                                return string.Format(_Answer_French[4], notification.Num_Vol, vol.Statut, vol.Vol.Code_IATA_Aeroport_Depart, vol.Vol.Code_IATA_Aeroport_Arrivee, vol.Date_Depart_Revisee.ToString(), vol.Date_Arrivee_Revisee.ToString());
                            else
                                return _Answer_French[1];
                        default:
                            return _Answer_French[0];
                    }

                    break;
                case "Anglais":
                    switch (messageStatus)
                    {
                        case MessageStatus.Default:
                            return _Answer_English[0];
                        case MessageStatus.Error:
                            return _Answer_English[1];
                        case MessageStatus.Closed:
                            return _Answer_English[2];
                        case MessageStatus.Followed:
                            return _Answer_English[3];
                        case MessageStatus.MAJ:
                            var vol = db.VolsCedules.SingleOrDefault(m => m.Numero_Vol == notification.Num_Vol);
                            if (vol != null)
                                return string.Format(_Answer_English[4], notification.Num_Vol, vol.Statut, vol.Vol.Code_IATA_Aeroport_Depart, vol.Vol.Code_IATA_Aeroport_Arrivee, vol.Date_Depart_Revisee, vol.Date_Arrivee_Revisee);
                            else
                                return _Answer_English[1];
                        default:
                            return _Answer_French[0];
                    }

                    break;
            }
            return null;
        }

        public MessagingResponse GetResponse(string messageBody, string messagePhone)
        {
            //Recoit Un message et le numero de telephone, Si le message correspont au norme et est trouver,
            //un MessageStatus.Closed/MessageStatus.Follow  est appliquer sinon envoie un erreur

            var answer = new Message();

            if( messageBody.Length > 5 
                && ( messageBody.ToUpper().StartsWith(_InputMessages[0]) || messageBody.ToUpper().StartsWith(_InputMessages[1])))
            {

                string flight_id = messageBody.ToUpper().Substring(5);
                if (Regex.IsMatch(flight_id, NotificationApp.NumVolRegex))
                {
                    Notification notif = DB2.Aeroport().Notifications.SingleOrDefault( m => m.Num_Phone == messagePhone);

                    MessageStatus status = (notif != null) ? MessageStatus.Closed : MessageStatus.Error;
                    string result =  MessageDistributor(status, notif);
                    answer.Body(result);

                    //If status is Closed then set to True
                    if(status == MessageStatus.Closed)
                    {
                        db.Entry(notif).Property(u => u.Statut).CurrentValue = true;
                    }

                }
                else
                {
                    answer.Body(MessageDistributor(MessageStatus.Error));
                }
            }
            else if (messageBody.Length > 5
            && (messageBody.ToUpper().StartsWith(_InputMessages[2]) || messageBody.ToUpper().StartsWith(_InputMessages[3])))
            {

                string flight_id = messageBody.ToUpper().Substring(5);
                if (Regex.IsMatch(flight_id, NotificationApp.NumVolRegex))
                {
                    Notification notif = db.Notifications.SingleOrDefault(m => m.Num_Phone == messagePhone);

                    MessageStatus status = (notif != null) ? MessageStatus.Followed : MessageStatus.Error;
                    string result = MessageDistributor(status, notif);
                    answer.Body(result);

                    //If status is Followed then set to False
                    if (status == MessageStatus.Followed)
                    {
                        db.Entry(notif).Property(u => u.Statut).CurrentValue = false;
                    }
                }
                else
                {
                    answer.Body(MessageDistributor(MessageStatus.Error));
                }
            }
            else
            {
                answer.Body(MessageDistributor(MessageStatus.Error));
            }
            
            
            var messageResponce = new MessagingResponse();
            messageResponce.Append(answer);
            return messageResponce;
        }

        public MessageResource WriteNewNotification(DAL.Notification notification, MessageStatus messageStatus = MessageStatus.Default)
        {
            string result = MessageDistributor(messageStatus, notification);
            MessageResource message = null;
            try
            {
                message = MessageResource.Create(
                    body: result,
                    from: new Twilio.Types.PhoneNumber(_PhoneNumber),
                    to: new Twilio.Types.PhoneNumber(notification.Num_Phone)
                    );
            }
            catch(Exception e)
            {
                //Twillio Error
            }

            return message;
        }

    }

    public enum MessageStatus
    {
        Default,
        MAJ,
        Error,
        Followed,
        Closed
    }
    


}