using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionVols.Models
{
    public class Admin
    {
        public int Id_Admin { get; set; }
        public string Nom { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string NomUtilisateur { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string MotPasse { get; set; }
        public bool Autorise { get; set; }
        public string Code_Aeroport { get; set; }

        public virtual Administration Administration { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}