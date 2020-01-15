using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionVols.Models
{
    public class VolPlannifie
    {
        public string NUMERO { get; set; }
        public string ETAT { get; set; }
        public string CODE { get; set; }
        public DateTime HEUREPREVUE { get; set; }
        public DateTime? HEUREREVISE { get; set; }
        public string PROVENANCE { get; set; }
        public string DESTINATION { get; set; }
        public string COMPAGNIE { get; set; }
        public string STATUT { get; set; }


    }
}