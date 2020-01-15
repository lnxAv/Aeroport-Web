using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionVols.Models
{
    public class DB
    {
        private static aeroportEntities instance = null;

        private DB()
        {
            
        }

        /// <summary>
        /// Obtenir la connexion avec EntityFramework
        /// </summary>
        /// <returns></returns>
        public static aeroportEntities Instance()
        {
            if (instance == null)
            {
                instance = new aeroportEntities();
            }

            return instance;
        }

        
        /// <summary>
        /// Deconnexion avec EntityFramework
        /// </summary>
        public static void disconnect()
        {
            instance = null;
        }
    }
}