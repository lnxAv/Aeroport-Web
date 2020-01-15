using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace GestionVols.Models
{
    public class DB2
    {
        private static aeroportEntities aeroportEntitie = null;

        private DB2()
        {

        }

        public static aeroportEntities Aeroport()
        {
            return new aeroportEntities();
        }
        public static void disconnect()
        {
            aeroportEntitie = null;
        }
    }
}