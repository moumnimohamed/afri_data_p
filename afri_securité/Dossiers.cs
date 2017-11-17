using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace afri_data
{
    public static class Dossiers
    {
        public static string Big_path { get; set; }

        public static List<DirectoryInfo> ls_All_dossier = new List<DirectoryInfo>();

        public static List<DirectoryInfo> ls_devis_years = new List<DirectoryInfo>();
        //contien les sou dossier de chaque dossier dans la aliste  ls_dossier_devis
        public static List<DirectoryInfo> ls_devis = new List<DirectoryInfo>();

        public static List<DirectoryInfo> ls_facture_years = new List<DirectoryInfo>();
        //contien les sou dossier de chaque dossier dans la aliste  ls_dossier_devis
        public static List<DirectoryInfo> ls_Facture = new List<DirectoryInfo>();

        public static List<DirectoryInfo> ls_situa_facturation = new List<DirectoryInfo>();
        //contien les Dossier de la liste Commande
        public static List<DirectoryInfo> ls_situa_commande = new List<DirectoryInfo>();

        


        public static void get_devis()
        {

            // date time now doit etre respecter
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                //  la forme des dossiers
                string pattern = "devis client " + i;
                string[] mot_chercher = pattern.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //chercher dans la liste all_dossier
                var liste_resultat = (from drc in ls_All_dossier
                                      where mot_chercher.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                      select drc);

                // afficher dans list box 3

                foreach (DirectoryInfo d in liste_resultat)
                {
                    //ajouter a liste des devis_years
                    ls_devis_years.Add(d);

                }

            }

        }

        //cette methode que je dois appel vis verssa avec le autre methode
        public static void get_devis_ls_devis()
        {
            //charger les sou directories dans ls_years_devis
            foreach (DirectoryInfo d in ls_devis_years)
            {
                DirectoryInfo[] sou_dr = d.GetDirectories();
                foreach (DirectoryInfo dri in sou_dr)
                {
                    ls_devis.Add(dri);

                }
            }
        }


        public static void get_facture()
        {

            // date time now doit etre respecter
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                //  la forme des dossiers
                string pattern = "FACTURE CLIENT " + i;
                string[] mot_chercher = pattern.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //chercher dans la liste all_dossier
                var liste_resultat = (from drc in ls_All_dossier
                                      where mot_chercher.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                      select drc);


                foreach (DirectoryInfo d in liste_resultat)
                {
                    //ajouter a liste des devis_years
                    ls_facture_years.Add(d);

                }

            }

        }

        public static void get_facture_ls_fact()
        {
            //charger les sou directories dans ls_years_facture
            foreach (DirectoryInfo d in ls_facture_years)
            {
                DirectoryInfo[] sou_dr = d.GetDirectories();
                foreach (DirectoryInfo dri in sou_dr)
                {
                    ls_Facture.Add(dri);

                }
            }
        }




        public static void get_situation_commande()
        {

            // date time now doit etre respecter
           
                //  la forme des dossiers
                string pattern = "SITUATION COMMANDE" ;
                string[] mot_chercher = pattern.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //chercher dans la liste all_dossier
                var liste_resultat = (from drc in ls_All_dossier
                                      where mot_chercher.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                      select drc);

                // afficher dans list box 3

                foreach (DirectoryInfo d in liste_resultat)
                {
                    //ajouter a liste des devis_years
                    ls_situa_commande.Add(d);

                }

            

        }

        public static void get_situation_facture()
        {

            // date time now doit etre respecter
         //   for (int i = 2010; i <= DateTime.Now.Year; i++)
          //  {
                //  la forme des dossiers
                string pattern = "SITUATION FACTURATION";
                string[] mot_chercher = pattern.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //chercher dans la liste all_dossier
                var liste_resultat = (from drc in ls_All_dossier
                                      where mot_chercher.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                      select drc);

                // afficher dans list box 3

                foreach (DirectoryInfo d in liste_resultat)
                {
                    //ajouter a liste des devis_years
                    ls_situa_facturation.Add(d);

                }
           // }
        }

        public static List<DirectoryInfo> get_doc_Comptable()
        {
            // ici j'ai utilisé une autre méthode (sans faire une liste juste chercher dans all_dossier et returner un liste 

            //******************************/
            string pattern = "AKKI";
            string[] mot_chercher = pattern.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //chercher dans la liste all_dossier
            var liste_resultat = (from drc in ls_All_dossier
                                  where mot_chercher.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                  select drc).ToList();

            return liste_resultat;

        }


        // ici j'ai utilisé une autre méthode (sans faire une liste juste chercher dans all_dossier et returner un liste 
        public static List<DirectoryInfo>  get_reference_OCP()
        { 
            string pattern = "REFERENCEMENT";
            string[] mot_chercher = pattern.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //chercher dans la liste all_dossier
            var liste_resultat = (from drc in ls_All_dossier
                                  where mot_chercher.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                  select drc).ToList();

            return liste_resultat;
        }


        public static DirectoryInfo chercher_FACpayéOUnon (string CO)
        {

            DirectoryInfo dir_test = null;
            foreach(DirectoryInfo  dr in ls_Facture)
            {
                
                if (dr.Name.Contains(CO) )
                {
                    dir_test = dr;
                    break;
                }

            }


            return dir_test;
        }
    } 
}
