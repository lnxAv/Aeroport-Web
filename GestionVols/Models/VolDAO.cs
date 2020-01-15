using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace GestionVols.Models
{
    public class VolDAO 
    {
        private VolDAO()
        {

        }

        /// <summary>
        /// Methode permettant de faire des recherches des vols sur une periode de 48h
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        public static List<VolPlannifie> rechercheVol(Recherche valeur)
        {
            List<VolPlannifie> list48H = new List<VolPlannifie>();

            switch (valeur.TYPE_VOL)
            {
                case TypeVol.Départ:

                    switch (valeur.OPTION)
                    {
                        case Option.Ville:

                            var volsDepart = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.DESTINATION.Contains(valeur.VALEUR)).ToList();

                            volsDepart.ForEach(v =>
                            {
                                VolPlannifie monVol = new VolPlannifie()
                                {
                                    NUMERO = v.NUMERO_VOL,
                                    ETAT = v.Etat,
                                    CODE = "",
                                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                                    PROVENANCE = v.PROVENANCE,
                                    DESTINATION = v.DESTINATION,
                                    COMPAGNIE = v.COMPAGNIE,
                                    STATUT = v.Statut

                                };

                                list48H.Add(monVol);
                            });

                            break;
                        case Option.Compagnie:

                            var volsDepart1 = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.COMPAGNIE.Contains(valeur.VALEUR)).ToList();

                            volsDepart1.ForEach(v =>
                            {
                                VolPlannifie monVol = new VolPlannifie()
                                {
                                    NUMERO = v.NUMERO_VOL,
                                    ETAT = v.Etat,
                                    CODE = "",
                                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                                    PROVENANCE = v.PROVENANCE,
                                    DESTINATION = v.DESTINATION,
                                    COMPAGNIE = v.COMPAGNIE,
                                    STATUT = v.Statut

                                };

                                list48H.Add(monVol);
                            });

                            break;
                        case Option.Numero:

                            var volsDepart2 = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.NUMERO_VOL.Contains(valeur.VALEUR)).ToList();

                            volsDepart2.ForEach(v =>
                            {
                                VolPlannifie monVol = new VolPlannifie()
                                {
                                    NUMERO = v.NUMERO_VOL,
                                    ETAT = v.Etat,
                                    CODE = "",
                                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                                    PROVENANCE = v.PROVENANCE,
                                    DESTINATION = v.DESTINATION,
                                    COMPAGNIE = v.COMPAGNIE,
                                    STATUT = v.Statut

                                };

                                list48H.Add(monVol);
                            });
                            break;
                        default:
                            break;
                    }

                    break;
                case TypeVol.Arrivé:
                    switch (valeur.OPTION)
                    {
                        case Option.Ville:
                            var volsaArrive = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.PROVENANCE.Contains(valeur.VALEUR)).ToList();

                            volsaArrive.ForEach(v =>
                            {
                                VolPlannifie monVol = new VolPlannifie()
                                {
                                    NUMERO = v.NUMERO_VOL,
                                    ETAT = v.Etat,
                                    CODE = "",
                                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                                    PROVENANCE = v.PROVENANCE,
                                    DESTINATION = v.DESTINATION,
                                    COMPAGNIE = v.COMPAGNIE,
                                    STATUT = v.Statut

                                };

                                list48H.Add(monVol);
                            });
                            break;
                        case Option.Compagnie:

                            var volsaArrive1 = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.COMPAGNIE.Contains(valeur.VALEUR)).ToList();

                            volsaArrive1.ForEach(v =>
                            {
                                VolPlannifie monVol = new VolPlannifie()
                                {
                                    NUMERO = v.NUMERO_VOL,
                                    ETAT = v.Etat,
                                    CODE = "",
                                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                                    PROVENANCE = v.PROVENANCE,
                                    DESTINATION = v.DESTINATION,
                                    COMPAGNIE = v.COMPAGNIE,
                                    STATUT = v.Statut

                                };

                                list48H.Add(monVol);
                            });
                            break;
                        case Option.Numero:

                            var volsaArrive2 = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.NUMERO_VOL.Contains(valeur.VALEUR)).ToList();

                            volsaArrive2.ForEach(v =>
                            {
                                VolPlannifie monVol = new VolPlannifie()
                                {
                                    NUMERO = v.NUMERO_VOL,
                                    ETAT = v.Etat,
                                    CODE = "",
                                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                                    PROVENANCE = v.PROVENANCE,
                                    DESTINATION = v.DESTINATION,
                                    COMPAGNIE = v.COMPAGNIE,
                                    STATUT = v.Statut

                                };

                                list48H.Add(monVol);
                            });
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }


            return list48H;
        }

        public static List<VolPlannifie> rechercheHome(Search valeur)
        {
            List<VolPlannifie> list48H = new List<VolPlannifie>();

            var volsDepart = DB.Instance().VOLS_RECHERCHES48H.Where(v => v.DESTINATION.Contains(valeur.Valeur) || v.PROVENANCE.Contains(valeur.Valeur)).ToList();

            volsDepart.ForEach(v =>
            {
                VolPlannifie monVol = new VolPlannifie()
                {
                    NUMERO = v.NUMERO_VOL,
                    ETAT = v.Etat,
                    CODE = "",
                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                    PROVENANCE = v.PROVENANCE,
                    DESTINATION = v.DESTINATION,
                    COMPAGNIE = v.COMPAGNIE,
                    STATUT = v.Statut

                };

                list48H.Add(monVol);
            });

            return list48H;
        }


        /// <summary>
        /// Liste des vols (DEPARTS ) 1Jr avant et 1 jour aprs
        /// </summary>
        /// <returns></returns>
        public static List<VolPlannifie> listeVolsDepartsAujourd()
        {
            var contextEF = DB.Instance();  
           
            var volsDepart = DB.Instance().VOLS_DEPARTS_Today.ToList();            

            return volsDepartToday(volsDepart);


        }

        public static List<VolPlannifie> listeVolsDepartsDemain()
        {
            var contextEF = DB.Instance();

            var volsDepart = DB.Instance().VOLS_DEPARTS_Tomorrow.ToList();

            return volsDepartTomorrow(volsDepart);


        }



        /// <summary>
        /// Methode pour liste de Vols d'Arrivées
        /// </summary>
        /// <returns></returns>
        public static List<VolPlannifie> listeVolsArrivesAujourd()
        {
            var contextEF = DB.Instance();

                var vols = DB.Instance().VOLS_ARRIVES_Today.ToList();



            return volsArrivesToday(vols);


        }

        public static List<VolPlannifie> listeVolsArrivesDemain()
        {
            var contextEF = DB.Instance();


            var vols = DB.Instance().VOLS_ARRIVES_Tomorrow.ToList();


            return volsArrivesTomorrow(vols);


        }
















        /*
         * ************************************************************
         *Tous nos creation a supprimer cette commentaires
         * 
         * 
         */

        /// <summary>
        /// METHODE PERMETTANT LA CREATION COLLECTION VOL DEPART PLANIFIÉE 
        /// </summary>
        /// <param name="vols"></param>
        /// <returns></returns>
        private static List<VolPlannifie> volsDepartToday(List<VOLS_DEPARTS_Today> vols)
        {
            List<VolPlannifie> list = new List<VolPlannifie>();

            vols.ForEach(v =>
            {
                VolPlannifie monVol = new VolPlannifie()
                {
                    NUMERO = v.NUMERO_VOL,
                    ETAT = v.Etat,
                    CODE = "",
                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                    PROVENANCE = v.PROVENANCE,
                    DESTINATION = v.DESTINATION,
                    COMPAGNIE = v.COMPAGNIE,
                    STATUT = v.Statut

                };

                list.Add(monVol);
            }

                );

            return list;
        }

        private static List<VolPlannifie> volsDepartTomorrow(List<VOLS_DEPARTS_Tomorrow> vols)
        {
            List<VolPlannifie> list = new List<VolPlannifie>();

            vols.ForEach(v =>
            {
                VolPlannifie monVol = new VolPlannifie()
                {
                    NUMERO = v.NUMERO_VOL,
                    ETAT = v.Etat,
                    CODE = "",
                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                    PROVENANCE = v.PROVENANCE,
                    DESTINATION = v.DESTINATION,
                    COMPAGNIE = v.COMPAGNIE,
                    STATUT = v.Statut

                };

                list.Add(monVol);
            }

                );

            return list;
        }

        /// <summary>
        /// METHODE PERMETTANT LA CREATION COLLECTION VOL DEPART PLANIFIÉE 
        /// </summary>
        /// <param name="vols"></param>
        /// <returns></returns>
        private static List<VolPlannifie> volsArrivesToday(List<VOLS_ARRIVES_Today> vols)
        {
            List<VolPlannifie> list = new List<VolPlannifie>();

            vols.ForEach(v =>
            {
                VolPlannifie monVol = new VolPlannifie()
                {
                    NUMERO = v.NUMERO_VOL,
                    ETAT = v.Etat,
                    CODE = "",
                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                    PROVENANCE = v.PROVENANCE,
                    DESTINATION = v.DESTINATION,
                    COMPAGNIE = v.COMPAGNIE,
                    STATUT = v.Statut

                };

                list.Add(monVol);
            }

                );

            return list;
        }

        private static List<VolPlannifie> volsArrivesTomorrow(List<VOLS_ARRIVES_Tomorrow> vols)
        {
            List<VolPlannifie> list = new List<VolPlannifie>();

            vols.ForEach(v =>
            {
                VolPlannifie monVol = new VolPlannifie()
                {
                    NUMERO = v.NUMERO_VOL,
                    ETAT = v.Etat,
                    CODE = "",
                    HEUREPREVUE = Convert.ToDateTime(v.DATE_DEPART),
                    HEUREREVISE = Convert.ToDateTime(v.DATE_DEP_REVISE),
                    PROVENANCE = v.PROVENANCE,
                    DESTINATION = v.DESTINATION,
                    COMPAGNIE = v.COMPAGNIE,
                    STATUT = v.Statut

                };

                list.Add(monVol);
            }

                );

            return list;
        }




    }
}