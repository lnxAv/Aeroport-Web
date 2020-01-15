using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace GestionVols.Models
{
    public class DB3
    {
        private static aeroportEntities aeroportEntitie = null;

        private DB3()
        {

        }

        public static aeroportEntities Aeroport()
        {
            if(aeroportEntitie == null)
            {
                aeroportEntitie = new aeroportEntities();
            }
            return aeroportEntitie;

        }
        public static void disconnect()
        {
            aeroportEntitie = null;
        }
    }
}