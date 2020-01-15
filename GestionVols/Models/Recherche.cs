using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionVols.Models
{
    public class Recherche
    {
        public string VALEUR { get; set; }
        public TypeVol TYPE_VOL { get; set; }
        public Option OPTION { get; set; }
    }

    public enum Option
    {
        Ville,
        Compagnie,
        Numero,

    }

    public enum TypeVol
    {
        Départ,
        Arrivé,      

    }

    public class Search
    {
        public string Valeur { get; set; }
    }
}