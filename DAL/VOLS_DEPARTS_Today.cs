//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class VOLS_DEPARTS_Today
    {
        public string NUMERO_VOL { get; set; }
        public string Etat { get; set; }
        public string Statut { get; set; }
        public string PROVENANCE { get; set; }
        public string DESTINATION { get; set; }
        public string COMPAGNIE { get; set; }
        public Nullable<System.DateTime> DATE_DEPART { get; set; }
        public Nullable<System.DateTime> DATE_DEP_REVISE { get; set; }
        public Nullable<System.DateTime> DATE_ARRIVE { get; set; }
        public Nullable<System.DateTime> DATE_ARR_REVISE { get; set; }
    }
}
