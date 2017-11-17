
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace afri_data
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            Thread t = new Thread(new ThreadStart(splash_screen));
            t.Start();
            Thread.Sleep(1000);

            InitializeComponent();
            t.Abort();

            var skin_material = MaterialSkinManager.Instance;
            skin_material.AddFormToManage(this);
            skin_material.Theme = MaterialSkinManager.Themes.LIGHT;

            skin_material.ColorScheme = new ColorScheme(Primary.Red500, Primary.Red900, Primary.Red900, Accent.Red700, TextShade.BLACK);
        }

        public void splash_screen()
        {
            Application.Run(new Form_splash());
        }

        FolderBrowserDialog FBD_facture = new FolderBrowserDialog();


        List<FileInfo> ls_commande_client_fournisseur = new List<FileInfo>();

        List<FileInfo> ls_liquidation = new List<FileInfo>();

        // la methode qui chager du shemain sauvgarder
        public static void Charge_du_chemin()
        {
            // charger tout les  dossier  du  chemain  serialiser 
            string grand_path = sauvgarde_loud.Read("le shemain des dossiers.xml");
            if (Directory.Exists(grand_path))
            {
                if (grand_path != null)
                {
                    try
                    {
                        //2  importer  le grand dosser  sauvgarder dans laplication
                        DirectoryInfo diri_info = new DirectoryInfo(grand_path);

                        //3 importer tout les sou_dossier dans  la liste all_dossier
                        Dossiers.ls_All_dossier.AddRange(diri_info.GetDirectories("*", SearchOption.AllDirectories));


                    }
                    catch (UnauthorizedAccessException u)
                    {
                        MessageBox.Show("le problem c'est a cause ces problemes: " + u.Message + "\n" + u.Source);

                    }

                }
                else
                {
                    MessageBox.Show("au début vous devez séléctionner un chemain qui contient tous les dossiers !!", "Attention");
                }

            }
            else {
                MessageBox.Show("le chemin séléctionner la dernière fois n'existe plus maintenant !!", "Attention");

            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {


            materialSingleLineTextField1.Focus();

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy";
            dateTimePicker2.Show();

            Charge_du_chemin();

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

            materialRadioButton2.Checked = true;

        }

        private void dociToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //petite forme de chargement de path
            export_form f = new export_form();

            f.ShowDialog();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {

        }


        private void dossierFacturesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }





        public void Afficher_dossier_devis()
        {
            // 5  pour eviter la repitition des dossier
            listBox1.Items.Clear();

            if (materialRadioButton1.Checked)
            {
                // 4  afficher tout les devis dan la list  sur la listview

                foreach (DirectoryInfo dr in Dossiers.ls_devis_years)
                {
                    // respecter l'anneé de datetimepicker
                    if (dr.ToString().ToLower().Contains(dateTimePicker2.Value.Year.ToString().ToLower()))
                    {
                        DirectoryInfo[] dr_temp = dr.GetDirectories();
                        foreach (DirectoryInfo d in dr_temp)
                        {
                            listBox1.Items.Add(d);
                        }

                    }
                }
            }
            else
            {
                // if checkedbox  des devis  pas selection
                listBox1.Items.Clear();
            }
        }

        public void Afficher_dossier_facture()
        {
            // 5  pour eviter la repitition des dossier
            listBox1.Items.Clear();

            if (materialRadioButton2.Checked)
            {
                // 4  afficher tout les facture dans la list  sur la listview

                foreach (DirectoryInfo dr in Dossiers.ls_facture_years)
                {
                    // respecter l'anneé de datetimepicker
                    if (dr.ToString().ToLower().Contains(dateTimePicker2.Value.Year.ToString().ToLower()))
                    {
                        DirectoryInfo[] dr_temp = dr.GetDirectories();
                        foreach (DirectoryInfo d in dr_temp)
                        {
                            listBox1.Items.Add(d);
                        }

                    }
                }
            }
            else
            {
                // if checkedbox  des devis  pas selection
                listBox1.Items.Clear();
            }
        }

        public void Afficher_doc_situa_commande()
        {
            // 5  pour eviter la repitition des dossier
            listBox2.Items.Clear();

            if (materialRadioButton3.Checked)
            {
                // 4  afficher tout les situation commande dans la list  sur la listview

                foreach (DirectoryInfo dr in Dossiers.ls_situa_commande)
                {
                    // respecter l'anneé de datetimepicker
                    //  if (dr.ToString().ToLower().Contains(dateTimePicker2.Value.Year.ToString().ToLower()))
                    //{

                    listBox2.Items.Add(dr);
                    //  }
                }
            }
            else
            {
                // if checkedbox  des devis  pas selection
                listBox2.Items.Clear();
            }
        }
        public void Afficher_doc_situa_facture()
        {
            // 5  pour eviter la repitition des dossier
            listBox2.Items.Clear();

            if (materialRadioButton4.Checked)
            {
                // 4  afficher tout les situation facture dans la list  sur la listbox

                foreach (DirectoryInfo dr in Dossiers.ls_situa_facturation)
                {
                    // respecter l'anneé de datetimepicker
                    //  if (dr.ToString().ToLower().Contains(dateTimePicker2.Value.Year.ToString().ToLower()))
                    //{

                    listBox2.Items.Add(dr);
                    //   }
                }
            }
            else
            {
                // if checkedbox  des devis  pas selection
                listBox2.Items.Clear();
            }
        }

        public void Afficher_doc_COMTABLE()
        {
            // 5  pour eviter la repitition des dossier
            listBox2.Items.Clear();

            List<DirectoryInfo> ls_deri_returned = new List<DirectoryInfo>();
            ls_deri_returned = Dossiers.get_doc_Comptable();
            foreach (DirectoryInfo dr in ls_deri_returned)
            {

                listBox2.Items.Add(dr);

            }
        }


        public void Afficher_REFERENCE_OCP()
        {
            // 5  pour eviter la repitition des dossier
            listBox2.Items.Clear();

            List<DirectoryInfo> ls_ref_ocp_returned = new List<DirectoryInfo>();
            ls_ref_ocp_returned = Dossiers.get_reference_OCP();
            foreach (DirectoryInfo dr in ls_ref_ocp_returned)
            {

                listBox2.Items.Add(dr);
                //   }
            }


        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }




        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            if (!materialCheckBox1.Checked && !materialCheckBox2.Checked)

            {
                // afficher devis selectionner sur listbox
                if (listBox2.SelectedItem != null)
                {
                    if (listBox2.SelectedItem.GetType() == typeof(DirectoryInfo))
                    {
                        DirectoryInfo dr = (DirectoryInfo)listBox2.SelectedItem;
                        FileInfo[] dr_files = dr.GetFiles();
                        DirectoryInfo[] dr_directories = dr.GetDirectories();

                        foreach (FileInfo f in dr_files)
                        {
                            //ignorer les fichier temporaire debut par  ~$
                            if (!f.Name.Contains("~$"))
                            {
                                listBox3.Items.Add(f);
                            }
                        }
                        foreach (DirectoryInfo d in dr_directories)
                        {
                            if (!d.Name.Contains("~$"))
                            {
                                listBox3.Items.Add(d);
                            }
                        }
                    }
                }
            }
            if (materialCheckBox1.Checked)
            {

                // chercher les commande de fournisseur rt client
                chercher_PDF_commande((DirectoryInfo)listBox2.SelectedItem);
                afficher_commande();
            }
            if (materialCheckBox2.Checked)
            {

                // chercher les liquidation
                chercher_liquidation((DirectoryInfo)listBox2.SelectedItem);
                afficher_luiqidation();
            }
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {
              materialCheckBox2.Enabled = true;
            }
            else
            {
                materialCheckBox2.Checked = false;
                materialCheckBox2.Enabled = false;
            }

            // effacer la place des ficher (pdf,word,exel)
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            Afficher_dossier_devis();
        }
        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton2.Checked)
            {
                materialCheckBox1.Enabled = true;
                materialCheckBox2.Enabled = true;

            }
            else
            {
                materialCheckBox1.Checked = false;
                materialCheckBox1.Enabled = false;
                materialCheckBox2.Checked = false;
                materialCheckBox2.Enabled = false;
            }
            // effacer la place des ficher (pdf,word,exel)
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            Afficher_dossier_facture();
        }

        private void materialRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // effacer la place des ficher (pdf,word,exel)
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            Afficher_doc_situa_commande();
        }
        private void materialRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton4.Checked)
            {
                materialFlatButton1.Enabled = true;
            }
            else
            {
                materialFlatButton1.Enabled = false;

            }
            // effacer la place des ficher (pdf,word,exel)
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            Afficher_doc_situa_facture();
        }

        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {
                Afficher_dossier_devis();
            }
            else if (materialRadioButton2.Checked)
            {
                Afficher_dossier_facture();
            }
            else if (materialRadioButton3.Checked)
            {
                Afficher_doc_situa_commande();
            }
            else if (materialRadioButton4.Checked)
            {
                Afficher_doc_situa_facture();
            }
        }





        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }



        private void materialSingleLineTextField1_Click_1(object sender, EventArgs e)
        {
            // voir l'evenement de changement de text 

        }


        private void materialSingleLineTextField1_TextChanged(object sender, EventArgs e)
        {

            string[] mot_chercher = materialSingleLineTextField1.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            listBox2.Items.Clear();

            if (materialRadioButton1.Checked)
            {
                resultat_rechercher(mot_chercher, Dossiers.ls_devis);

            }
            else if (materialRadioButton2.Checked)
            {
                resultat_rechercher(mot_chercher, Dossiers.ls_Facture);

            }
            else if (materialRadioButton3.Checked)
            {
                resultat_rechercher(mot_chercher, Dossiers.ls_situa_commande);

            }
            else if (materialRadioButton4.Checked)
            {
                resultat_rechercher(mot_chercher, Dossiers.ls_situa_facturation);

            }
        }
        public void resultat_rechercher(string[] mot, List<DirectoryInfo> directory)
        {

            var liste_resultat = (from drc in directory
                                  where mot.All(s => drc.Name.ToLower().Contains(s.ToLower()))
                                  select drc).ToList();


            foreach (DirectoryInfo d in liste_resultat)
            {

                listBox2.Items.Add(d);

            }
        }
        FileInfo[] files;
        DirectoryInfo[] directories;
        // pour mini rechercher ,pour chercher files (fichier de liquidation)
        public void resultat_rechercher(string[] mot, DirectoryInfo directo)
        {


            if (directo != null)
            {
                files = directo.GetFiles();
                directories = directo.GetDirectories();
            }
            else
            {
                MessageBox.Show("doosier est vide ou n'est pas selectionner ");
            }
            var liste_files = (from file in files
                               where mot.All(s => file.Name.ToLower().Contains(s.ToLower()))
                               select file).ToList();
            var liste_derictory = (from dr in directories
                                   where mot.All(s => dr.Name.ToLower().Contains(s.ToLower()))
                                   select dr).ToList();

            foreach (FileInfo f in liste_files)
            {
                if (!f.Name.Contains("$"))
                {
                    listBox3.Items.Add(f);
                }
            }
            foreach (DirectoryInfo d in liste_derictory)
            {
                if (!d.Name.Contains("$"))
                {
                    listBox3.Items.Add(d);
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Le nom nom du dossier
          
                materialLabel3.Text = listBox1.SelectedItem.ToString();
            
            listBox3.Items.Clear();

            if (!materialCheckBox1.Checked && !materialCheckBox2.Checked)
            {
                // afficher devis selectionner sur listbox
                if (listBox1.SelectedItem != null)
                {
                    if (listBox1.SelectedItem.GetType() == typeof(DirectoryInfo))
                    {
                        DirectoryInfo dr = (DirectoryInfo)listBox1.SelectedItem;
                        FileInfo[] dr_files = dr.GetFiles();
                        DirectoryInfo[] dr_directories = dr.GetDirectories();

                        foreach (FileInfo f in dr_files)
                        {
                            //ignorer les fichier temporaire debut par  ~$
                            if (!f.Name.Contains("~$"))
                            {
                                listBox3.Items.Add(f);
                            }

                        }
                        foreach (DirectoryInfo d in dr_directories)
                        {
                            if (!d.Name.Contains("~$"))
                            {
                                listBox3.Items.Add(d);
                            }
                        }
                    }
                }
            }

            if (materialCheckBox1.Checked)
            {

                // chercher les commande de fournisseur rt client
                chercher_PDF_commande((DirectoryInfo)listBox1.SelectedItem);
                afficher_commande();
            }
            if (materialCheckBox2.Checked)
            {
                // chercher les liquidation
                chercher_liquidation((DirectoryInfo)listBox1.SelectedItem);
                afficher_luiqidation();
            }
        } 

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                //tester si l'object est  directory 
                if (listBox1.SelectedItem.GetType() == typeof(DirectoryInfo))
                {
                    var fileToOpen = (DirectoryInfo)listBox1.SelectedItem;
                    Process.Start(fileToOpen.FullName);
                }
                else if (listBox1.SelectedItem.GetType() == typeof(FileInfo))
                {
                    var fileToOpen = (FileInfo)listBox1.SelectedItem;

                    Process.Start(fileToOpen.FullName);
                }
                else
                {
                    MessageBox.Show("le dossier n'existe pas ou il y a un probleme relatif au dossier , essayer de cliquer deux fois sur le dossier", "attention");
                }
            }
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                //tester si l'object est  directory 
                if (listBox2.SelectedItem.GetType() == typeof(DirectoryInfo))
                {
                    var fileToOpen = (DirectoryInfo)listBox2.SelectedItem;
                    Process.Start(fileToOpen.FullName);
                }
                else if (listBox2.SelectedItem.GetType() == typeof(FileInfo))
                {
                    var fileToOpen = (FileInfo)listBox2.SelectedItem;
                    Process.Start(fileToOpen.FullName);
                }
                else
                {
                    MessageBox.Show("le dossier n'existe pas", "attention");
                }
            }
        }
        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                //tester si l'object est  directory 
                if (listBox3.SelectedItem.GetType() == typeof(DirectoryInfo))
                {
                    var fileToOpen = (DirectoryInfo)listBox3.SelectedItem;
                    Process.Start(fileToOpen.FullName);
                }
                else if (listBox3.SelectedItem.GetType() == typeof(FileInfo))
                {
                    var fileToOpen = (FileInfo)listBox3.SelectedItem;
                    if (fileToOpen.Exists)
                    {
                        Process.Start(fileToOpen.FullName);
                    }
                    else
                    {
                        MessageBox.Show("le fichier n'xiste pas ou il est modifier !!", "attention");

                    }
                }

            }
        }
        private void materialSingleLineTextField2_Click(object sender, EventArgs e)
        {

        }
        DirectoryInfo test;

        private void materialSingleLineTextField2_TextChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                test = (DirectoryInfo)listBox2.SelectedItem;
            }
            else
            {
                test = (DirectoryInfo)listBox1.SelectedItem;

            }
            if (test != null)
            {
                listBox3.Items.Clear();
                string[] mot_chercher = materialSingleLineTextField2.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                resultat_rechercher(mot_chercher, test);
            }
            else
            {
                MessageBox.Show("Séléctionner un dossier avant de chercher", "attention");
            }
        }
        // on doit ici faire le travaill de la gestion de situation de facturetion payé ou impayé
        // il ya methode juste si_dessous applé dans l"otre form (form situa facture)

        private void listBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                if (listBox3.SelectedItem.GetType() == typeof(FileInfo))
                {
                    FileInfo fileEXL = (FileInfo)listBox3.SelectedItem;
                    if (fileEXL.Extension == ".xlsx" || fileEXL.Extension == ".xlsm" || fileEXL.Extension == ".xlsb" || fileEXL.Extension == ".xltx" || fileEXL.Extension == ".xltm")
                    {
                        Form_situa_facture.excel_choisis = (FileInfo)listBox3.SelectedItem;
                        Form_situa_facture f = new Form_situa_facture();
                        f.Show();
                    }
                    else {
                        MessageBox.Show("vous devez séléctionner un fichier Excel !! ", "Attention");
                    }
                }
            }
            else
            {
                MessageBox.Show("vous devez séléctionner un fichier Excel  !! ", "Attention");

            }
        }

        private void materialRadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            // effacer la place des ficher (pdf,word,exel)
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            Afficher_doc_COMTABLE();
        }

        private void materialRadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            // effacer la place des ficher (pdf,word,exel)
            listBox3.Items.Clear();
            listBox2.Items.Clear();
            Afficher_REFERENCE_OCP();
        }


        private void materialRadioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void chercher_PDF_commande(DirectoryInfo dr)
        {
            //  tous ca ses de  les commande fournisseur et commande client (radiobutton)
            ls_commande_client_fournisseur.Clear();
            if (dr != null)
            {

                FileInfo[] files = dr.GetFiles("*", SearchOption.AllDirectories);
                foreach (FileInfo f in files)
                {
                    ls_commande_client_fournisseur.Add(f);
                }
            }
        
        }
        public void chercher_liquidation(DirectoryInfo dr)
        {
            //  tous ca ses de  les commande fournisseur et commande client (radiobutton)
            ls_liquidation.Clear();
            if (dr != null)
            {

                FileInfo[] files = dr.GetFiles("*", SearchOption.AllDirectories);
                foreach (FileInfo f in files)
                {
                    ls_liquidation.Add(f);
                }
            }
            

        }
        private void materialRadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            foreach (FileInfo ff in ls_commande_client_fournisseur)
            {
                if (ff.Name.ToLower().Contains("client".ToLower()))
                {
                    listBox3.Items.Add(ff);
                }
            }
        }
        DirectoryInfo diri_test;

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem !=null)
            {
                diri_test = (DirectoryInfo)listBox1.SelectedItem;
            }else if (listBox1.SelectedItem != null)
            {
                diri_test = (DirectoryInfo)listBox2.SelectedItem;

            }
            listBox3.Items.Clear();
            // chercher les commande de fournisseur rt client
            chercher_PDF_commande(diri_test);
            afficher_commande();
        }

        public void afficher_commande()
        {
            foreach (FileInfo ff in ls_commande_client_fournisseur)
            {
                if (ff.Name.ToLower().Contains("fournisseur".ToLower()))
                {
                    if (!ff.Name.Contains("$"))
                    {
                        listBox3.Items.Add(ff);
                    }
                }
                if (ff.Name.ToLower().Contains("client".ToLower()) && (ff.Extension == ".pdf" || ff.Extension == ".xlsx" || ff.Extension == ".xlsm" || ff.Extension == ".xlsb" || ff.Extension == ".xltx" || ff.Extension == ".xltm" || ff.Extension == ".docx" ||   ff.Extension == ".doc" || ff.Extension == ".odt" || ff.Extension == ".xlsx" || ff.Extension == ".pptx"))
                {
                    if (!ff.Name.Contains("$"))
                    {
                        listBox3.Items.Add(ff);
                    }
                }
            }
        }
        public void afficher_luiqidation()
        {
            foreach (FileInfo ff in ls_liquidation)
            {
                if (ff.Name.ToLower().Contains("LIQUIDATION".ToLower()))
                {
                    if (!ff.Name.Contains("$"))
                    {
                        listBox3.Items.Add(ff);
                    }
                }

            }
        }
       
        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void materialRadioButton7_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void materialCheckBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                diri_test = (DirectoryInfo)listBox1.SelectedItem;
            }
            else if (listBox1.SelectedItem != null)
            {
                diri_test = (DirectoryInfo)listBox2.SelectedItem;
            }
            listBox3.Items.Clear();
            // chercher les commande de fournisseur rt client
            chercher_liquidation(diri_test);
            afficher_luiqidation();
        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void listBox1_Click_1(object sender, EventArgs e)
        {
            materialLabel3.Text = listBox1.SelectedItem.ToString();
        }
    }
}