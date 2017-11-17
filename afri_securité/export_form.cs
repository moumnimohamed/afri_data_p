using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Text.RegularExpressions;

namespace afri_data
{
    public partial class export_form : MaterialForm
    {
        public export_form()
        {
            InitializeComponent();
            var skin_material = MaterialSkinManager.Instance;
            skin_material.AddFormToManage(this);
            skin_material.Theme = MaterialSkinManager.Themes.LIGHT;
            skin_material.ColorScheme = new ColorScheme(Primary.Red500, Primary.Red900, Primary.Red900, Accent.Red700, TextShade.BLACK);
        }


        FolderBrowserDialog FBD_devis = new FolderBrowserDialog();
        private void button5_Click(object sender, EventArgs e)
        {



        }


        //cette fonction permer de selection le grand cheman


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }




        public void enregistrer_Grand_chemain()
        {
            // doit un chemain sélectionner
            if (!FBD_devis.SelectedPath.Equals(string.Empty))
            {
                try
                {
                    Dossiers.ls_All_dossier.Clear();
                    Dossiers.ls_devis_years.Clear();
                    Dossiers.ls_devis.Clear();
                    Dossiers.ls_facture_years.Clear();
                    Dossiers.ls_Facture.Clear();
                    Dossiers.ls_situa_commande.Clear();
                    Dossiers.ls_situa_facturation.Clear();


                    DirectoryInfo diri_info = new DirectoryInfo(FBD_devis.SelectedPath);
                    // ajouter path selectionner a la proprier bog_path de la class Dossiers
                    Dossiers.Big_path = FBD_devis.SelectedPath;
                    sauvgarde_loud.Write(Dossiers.Big_path, "le shemain des dossiers.xml");
                    //3 importer tout les sou_dossier dans le la liste all_dossier
          
                   Dossiers.ls_All_dossier.AddRange(diri_info.GetDirectories("*", SearchOption.AllDirectories));
                 

                    //  MessageBox.Show("le chemain est bien enregister , tous led dossier de ce chemain son enregister ");

                    this.Close();
                }
                catch (UnauthorizedAccessException u)
                {
                   MessageBox.Show("le problem c'est a cause ces problemes: " + u.Message + "\n" + u.Source);
                   
                }
            }
            else
            {

                MessageBox.Show("vous devez sélectionner un chemain");
            }
        }

        private void export_form_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            enregistrer_Grand_chemain();

            // importer les devis de la liste all_dossier a la liste ls_years_devis
            Dossiers.get_devis();
            // importer les devis de la liste ls_years_devis a la liste ls_devis
            Dossiers.get_devis_ls_devis();

            // importer les factrure de la liste all_dossier a la liste ls_years_facture
            Dossiers.get_facture();
            // importer les facture de la liste ls_years_facture a la liste ls_facture
            Dossiers.get_facture_ls_fact();

            // importer les situation de commande  de la liste all_dossier a la liste ls_situation_commande
            Dossiers.get_situation_commande();
            // importer les situation de facture de la liste all_dossier la liste ls_situation_facture
            Dossiers.get_situation_facture();




        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            //afficher dans list box juste pour voir
            if (FBD_devis.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = FBD_devis.SelectedPath;
                    listBox2.Items.Clear();

                    DirectoryInfo diri_info = new DirectoryInfo(FBD_devis.SelectedPath);
                    //3 importer tout les sou_dossier dans le la liste all_dossier
                    listBox2.Items.AddRange(diri_info.GetDirectories("*", SearchOption.AllDirectories));
                }
                catch (UnauthorizedAccessException u)
                {
                    MessageBox.Show("le problème c'est a cause ce Dossiers: " + u.Message + "\n" + u.Source);

                }
            }
        } 

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
