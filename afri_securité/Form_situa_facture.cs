using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Linq;
using System.Diagnostics;



namespace afri_data
{
    public partial class Form_situa_facture : MaterialForm
    {
        public Form_situa_facture()
        {
            InitializeComponent();

            var skin_material = MaterialSkinManager.Instance;
            skin_material.AddFormToManage(this);
            skin_material.Theme = MaterialSkinManager.Themes.LIGHT;

            skin_material.ColorScheme = new ColorScheme(Primary.Red500, Primary.Red900, Primary.Red900, Accent.Red700, TextShade.BLACK);

        }

        public static List<string> ls_Nfacture = new List<string>();
        public static List<string> ls_num_Fact = new List<string>();


        public static List<string> ls_payéOUnon = new List<string>();

        public static FileInfo excel_choisis { get; set; }

        // c'est comment un serveur qui fait appel les excel workbook 
        private static Excel.Application MyApp = null;

        // workbook c'est leficher excel
        private static Excel.Workbook MyBook = null;
        // worksheet sont les les sheet dans fishier pdf
        private static Excel.Worksheet MySheet = null;
        private static Excel.Range xlRange;

        List<DirectoryInfo> ls_Facture_payé = new List<DirectoryInfo>();
        List<DirectoryInfo> ls_Facture_impayé = new List<DirectoryInfo>();
        private void Form_situa_facture_Load(object sender, EventArgs e)
        {

            ls_Facture_impayé.Clear();
            ls_Facture_payé.Clear();
            dataGridView1.Rows.Clear();
            try {
                MyApp = new Excel.Application();
                MyApp.Visible = true;
                MyBook = MyApp.Workbooks.Open(excel_choisis.FullName);
                MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explicit cast is not required here
               xlRange = MySheet.UsedRange;
            }
            catch
            {
                MessageBox.Show("il y a un problème dans le fichier excel, essayer de résoudre les problemes relatif !! ", "Attention");
            }
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //listBox1.Items.Clear();
            // listBox2.Items.Clear();


            ls_payéOUnon.Clear();
            try
            {
                for (int i = 1; i <= rowCount; i++)
                {

                    for (int j = 1; j <= colCount; j++)
                    {

                        try
                        {
                            if (j == 1)
                            {
                                //write the value to the console
                                if (xlRange.Cells[i ,j] != null && xlRange.Cells[i , j].Value2 != null)
                                {


                                    dataGridView1.Rows.Add(xlRange.Cells[i , j].Value2,
                                                         
                                                        xlRange.Cells[i , j + 2].Value2,
                                                          xlRange.Cells[i , j + 3].Value2,
                                                           xlRange.Cells[i , j + 5].Value2,
                                                           xlRange.Cells[i , j + 6].Value2,
                                                           xlRange.Cells[i , j + 7].Value2,
                                                           xlRange.Cells[i , j + 8].Value2
                                                           );


                                }
                            }

                        }
                        catch (FieldAccessException ex)
                        {
                            MessageBox.Show("il ya des problème par-apport votre fichier excel \n ", ex.ToString());
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.ToString(), "attention");
                return;
            }

            try
            {
                MyBook.Close(null, null, null);
                MyApp.Quit();

              

                Marshal.ReleaseComObject(MySheet);
                Marshal.ReleaseComObject(MyBook);
                Marshal.ReleaseComObject(MyApp);
            }
            catch
            {
                return;
            }


            try {
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {

                    string n = (string)r.Cells[0].Value;
                    if (n != null &&    !n.Equals("FACTURE") )
                    {
                        string A = n.TrimStart(new char[] { 'F', 'A', 'C', 'T', 'U', 'R', 'E' });
                        string[] C = A.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries); ;
                         string D = C[0].TrimStart(new char[] { ' ' });
                      string code = D.TrimEnd(new char[] { ' ' });

                        //déviser le mot de la societe dans data_grid view
                        string nom = (string)r.Cells[1].Value;
                        string[] mot = nom.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        DirectoryInfo diri = Dossiers.chercher_FACpayéOUnon(code);

                        if (diri != null)
                        {
                            string P = (string)r.Cells[4].Value;
                            string x = P.TrimStart(new char[] { ' ' });
                            string p = x.TrimEnd(new char[] { ' ' });
                            if (p.ToString().ToUpper().Equals("PAYE".ToUpper()))
                            {
                                ls_Facture_payé.Add(diri);


                            }
                            else
                            {
                                ls_Facture_impayé.Add(diri);
                            }
                        }
                        else
                        {
                            listBox1.Items.Add(A);
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("il y'a un problème dans le fichier excel \n vérifier si tous les cellule est bien organisé","Attention");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listBox4.Items.Clear();

            listBox3.Items.Clear();
            foreach (DirectoryInfo d in ls_Facture_payé)
            {
                listBox3.Items.Add(d);
            }
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
            listBox3.Items.Clear();
            foreach (DirectoryInfo d in ls_Facture_impayé)
            {
                listBox3.Items.Add(d);
            }
        }

        private void materialSingleLineTextField1_TextChanged(object sender, EventArgs e)
        {
            string[] mot_chercher = materialSingleLineTextField1.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            listBox3.Items.Clear();

            if (materialRadioButton1.Checked)
            {
                resultat_recherche(mot_chercher, ls_Facture_payé);

            }
            else if (materialRadioButton2.Checked)
            {
                resultat_recherche(mot_chercher, ls_Facture_impayé);

            }
        }
        public void resultat_recherche(string[] mot, List< DirectoryInfo> director)
        {
            var liste_resulta = (from drc in director
                                  where mot.All (s => drc.Name.ToLower().Contains(s.ToLower()))
                                  select drc);


            foreach (DirectoryInfo d in liste_resulta)
            {

                listBox3.Items.Add(d);

            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // afficher devis selectionner sur listbox
            listBox4.Items.Clear();
            if (listBox3.SelectedItem != null)
            {
                if (listBox3.SelectedItem.GetType() == typeof(DirectoryInfo))
                {
                    DirectoryInfo dr = (DirectoryInfo)listBox3.SelectedItem;
                    FileInfo[] dr_files = dr.GetFiles();
                    DirectoryInfo[] dr_directories = dr.GetDirectories();

                    foreach (FileInfo f in dr_files)
                    {
                        //ignorer les fichier temporaire debut par  ~$
                        if (!f.Name.Contains("~$"))
                        {
                            listBox4.Items.Add(f);
                        }
                    }
                    foreach (DirectoryInfo d in dr_directories)
                    {
                        if (!d.Name.Contains("~$"))
                        {
                            listBox4.Items.Add(d);
                        }
                    }
                }
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
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

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {

            //tester si l'object est  directory 
            if (listBox4.SelectedItem.GetType() == typeof(DirectoryInfo))
            {
                var fileToOpen = (DirectoryInfo)listBox4.SelectedItem;
                Process.Start(fileToOpen.FullName);
            }
            else if (listBox4.SelectedItem.GetType() == typeof(FileInfo))
            {
                var fileToOpen = (FileInfo)listBox4.SelectedItem;
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

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }
        DirectoryInfo test;
        private void materialSingleLineTextField3_TextChanged(object sender, EventArgs e)
        {
            test = (DirectoryInfo)listBox3.SelectedItem;
            if (test != null)
            {
                listBox4.Items.Clear();
                string[] mot_chercher = materialSingleLineTextField3.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                resultat_recherche(mot_chercher, test);
            }
            else
            {
                MessageBox.Show("Séléctionner un dossier avant de chercher", "attention");
            }
        }
        FileInfo[] files;
        DirectoryInfo[] directories;
        public void resultat_recherche(string[] mot, DirectoryInfo directo)
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
                    listBox4.Items.Add(f);
                }
            }
            foreach (DirectoryInfo d in liste_derictory)
            {
                if (!d.Name.Contains("$"))
                {
                    listBox4.Items.Add(d);
                }
            }
        }

        private void materialSingleLineTextField2_TextChanged(object sender, EventArgs e)
        {
           
                listBox3.Items.Clear();
                string[] mot_chercher = materialSingleLineTextField2.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var liste_derictory = (from dr in Dossiers.ls_Facture
                                   where mot_chercher.All(s => dr.Name.ToLower().Contains(s.ToLower()))
                                   select dr).ToList();

            foreach (DirectoryInfo f in liste_derictory)
            {
                if (!f.Name.Contains("$"))
                {
                    listBox3.Items.Add(f);
                }
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}    
      

       
          

    /*   foreach (string n in ls_Nfacture)
       {

           string A = n.TrimStart(new char[] { 'F', 'A', 'C', 'T', 'U', 'R', 'E' });
           string B = A.TrimStart(new char[] { 'D', 'E', 'V', 'I', 'S' });
           string C = B.TrimStart(new char[] { ' ' });
           string D = C.TrimEnd(new char[] { ' ' });

       }


       foreach (DirectoryInfo d in Dossiers.ls_Facture)
       {
           string A = d.Name.TrimStart(new char[] { 'F', 'A', 'C', 'T', 'U', 'R', 'E' });
           string D= A.TrimStart(new char[] { 'D', 'E', 'V', 'I', 'S' });
         //  string C = B.TrimStart(new char[] { ' ' });
          // string D = C.TrimEnd(new char[] { ' ' });
           foreach (string nu in listBox1.Items)
           {
               string[] mot = nu.Split(new[] { '-' });


               if (D.Contains(mot[0]) && D.Contains(mot[1]))
               {
                   listBox2.Items.Add(d);
                   break;
               }
           }

           /*  for (int i = 0; i < ls_num_Fact.Count; i++)
             {
                 for (int j = 0; j < Dossiers.ls_Facture.Count; j++)
                 {
                     if (Dossiers.ls_Facture.ElementAt(j).Name.Contains(ls_num_Fact.ElementAt(i)))
                     {
                         listBox1.Items.Add(Dossiers.ls_Facture.ElementAt(j));
                         break;
                     }
                 }
             }*/
