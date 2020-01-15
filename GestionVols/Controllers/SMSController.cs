using DAL;
using GestionVols.Controllers.Class.ControllerObjects;
using GestionVols.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Lookups.V1;

namespace GestionVols.Controllers
{
    
    public class SMSController : TwilioController
    {
        TwilloInstance twilloInstance;
        aeroportEntities dbcontext;

        public SMSController()
        {
            dbcontext = DB.Instance();
            twilloInstance = new TwilloInstance();
        }

        [HttpPost]
        public TwiMLResult Response()
        {
                        var phoneNumber = PhoneNumberResource.Fetch(
                       countryCode: "US",
                       pathPhoneNumber: new Twilio.Types.PhoneNumber(Request.Form["From"])
                   );
            string requestBody = Request.Form["Body"];
            var requestPhone = phoneNumber.PhoneNumber.ToString();

            var response = twilloInstance.GetResponse(requestBody,requestPhone);
            return TwiML(response);
        }
    }
}