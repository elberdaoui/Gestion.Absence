using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace absence
{
    /// <summary>
    /// Logique d'interaction pour ApprdashB.xaml
    /// </summary>
    public partial class ApprdashB : Window
    {
        public ApprdashB()
        {
            InitializeComponent();

        }

        SqlConnection cn = new SqlConnection("initial catalog= absenseGestion; data source= DESKTOP-0PAGHK2; integrated security= true;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        private DataView dv;


        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_déco_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void ApprdashB1_Loaded(object sender, RoutedEventArgs e)
        {

            cmd.CommandText = " SELECT TOP (3) idAbs as ID, dateAbs as Date, motif as Motif, justif as Etat from Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "' Order By idAbs DESC;";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            dataGrid_App_home.ItemsSource = dt.DefaultView;

            cn.Close();
        }

        private void Btn_Abs_App_Click(object sender, RoutedEventArgs e)
        {


            cmd.CommandText = " SELECT dateAbs as Date, motif as Motif, justif as Etat, doc as Justificatif, mois as mois from Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "' ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            dataGrid_App_Abs.ItemsSource = dt.DefaultView;

            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_app_info.Visibility = Visibility.Hidden;
            Grid_App_askForAbs.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Visible;
            cn.Close();
        }

        private void btn_App_filtre_mois_Click(object sender, RoutedEventArgs e)
        {
            cmd.CommandText = " SELECT dateAbs as Date,  motif as Motif, justif as Etat, doc as Justificatif from Absence WHERE mois = '" + CBx_mois_appr.Text + "' AND idApprenant = '" + TBx_App_id_info.Text + "'  ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            cn.Close();
            dataGrid_App_Abs.ItemsSource = dt.DefaultView;

            //btn_App_filtre_mois.Visibility = Visibility.Hidden;
            btn_App_filtre_mois_reinis.Visibility = Visibility.Visible;
        }

        private void btn_App_filtre_mois_reinis_Click(object sender, RoutedEventArgs e)
        {
            cmd.CommandText = " SELECT dateAbs as Date, motif as Motif, justif as Etat, doc as Justificatif from Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "'  ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            cn.Close();
            dataGrid_App_Abs.ItemsSource = dt.DefaultView;

            btn_App_filtre_mois.Visibility = Visibility.Visible;
            btn_App_filtre_mois_reinis.Visibility = Visibility.Hidden;
            CBx_mois_appr.SelectedIndex = -1;
        }

        private void Btn_App_dash_Click(object sender, RoutedEventArgs e)
        {
            Grid_dash_App.Visibility = Visibility.Visible;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
            Grid_app_info.Visibility = Visibility.Hidden;
            Grid_App_askForAbs.Visibility = Visibility.Hidden;

        }

        private void dataGrid_App_Abs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_App_prfl_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();

            cmd = new SqlCommand(" SELECT Apprenant.nom, Apprenant.prenom, Apprenant.email, Apprenant.idApprenant, Apprenant.adresse, Apprenant.phone, Formation.formation , Formateur.nom, Formateur.prenom, Apprenant.picture FROM Apprenant LEFT JOIN Formation ON Apprenant.idFormation = Formation.idFormation LEFT JOIN Formateur ON Apprenant.idFormateur = Formateur.idFormateur WHERE idApprenant = '" + TBx_App_id_info.Text + "'; ", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            TBx_app_info_nom.Text = dr[0].ToString();
            TBx_app_info_prenom.Text = dr[1].ToString();
            TBx_app_info_format.Text = dr[7].ToString() + " " + dr[8].ToString();
            TBx_app_info_mail.Text = dr[2].ToString();
            TBx_app_info_adress.Text = dr[4].ToString();
            TBx_app_info_phone.Text = dr[5].ToString();
            TBx_app_info_forma.Text = dr[6].ToString();

            //img_perfil.Source = Image.FromStream(ms);

            Grid_app_info.Visibility = Visibility.Visible;
            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
            Grid_App_askForAbs.Visibility = Visibility.Hidden;

            cn.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void ClearTxt(object sender, MouseButtonEventArgs e)
        {
            TBx_Search.Clear();

        }

        private void BackText(object sender, MouseEventArgs e)
        {
            TBx_Search.Text = "Chercher...";


        }

        private void Btn_App_addJustif_Click(object sender, RoutedEventArgs e)
        {
            
            FileDialog fldlg = new OpenFileDialog();
            //fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            fldlg.Filter = " File (*.jpg;*.pdf;*.png)|*.jpg;*.pdf;*.png";
            fldlg.ShowDialog();
            {
                string strName = fldlg.SafeFileName;
                TBx_App_fileJustif.Text = fldlg.FileName;
            }
            fldlg = null;
        }

        private void btn_App_save_abs_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();
            string sql = "insert into Absence(idApprenant, dateAbs, mois, justif, doc) values( '" + TBx_App_id_info.Text + "', '" + TBx_App_date_abs.Text + "','" + CBx_mois_appr2.Text + "','" + TBx_App_EnAtt_Abs.Text + "',@doc ) ";
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                //Pass byte array into database
                cmd.Parameters.AddWithValue("doc", Encoding.UTF8.GetBytes(TBx_App_fileJustif.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Votre demande à bien été envoyé !");

                TBx_App_AskAbs_motif.Clear();
                TBx_App_AskAbs_nbrDays.Clear();
                TBx_App_fileJustif.Clear();
                CBx_mois_appr2.SelectedIndex = -1;
            }
            cn.Close();
        }

        private void btn_App_backHome_Click(object sender, RoutedEventArgs e)
        {
            Grid_App_askForAbs.Visibility = Visibility.Hidden;
            Grid_dash_App.Visibility = Visibility.Visible;

        }

        private void btn_App_askForABs_Click(object sender, RoutedEventArgs e)
        {
            Grid_App_askForAbs.Visibility = Visibility.Visible;
            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
            Grid_app_info.Visibility = Visibility.Hidden;

        }

        private void CBx_mois_appr_SelectonChanged(object sender, SelectionChangedEventArgs e)
        {
            //cmd.CommandText = " SELECT dateAbs as Date,  motif as Motif, justif as Etat, doc as Justificatif from Absence WHERE mois = '" + CBx_mois_appr.Text + "' AND idApprenant = '" + TBx_App_id_info.Text + "'  ";
            //cmd.Connection = cn;
            //DataTable dt = new DataTable();
            //cn.Open();
            //dr = cmd.ExecuteReader();
            //dt.Load(dr);
            //cn.Close();
            //dataGrid_App_Abs.ItemsSource = dt.DefaultView;

            //String SelectedItem = CBx_mois_appr.SelectedItem.ToString();
            //if(SelectedItem != "None")
            //{
            //   dv.RowFilter = string.Format("mois Like '%{0}%'", SelectedItem);
            //    dataGrid_App_Abs.ItemsSource = dv;
            //}
        }














        //private void getDataAbs()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader dr;

        //    cmd.CommandText = " SELECT idAbs, dateAbs, motif, justif, doc from Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "' ";
        //    cmd.Connection = cn;
        //    DataTable dt = new DataTable();
        //    cn.Open();
        //    dr = cmd.ExecuteReader();
        //    dt.Load(dr);

        //    Datagrid_App_AddAbs.ItemsSource = dt.DefaultView;
        //    cn.Close();
        //}

        //private void Btn_App_JustifTelech_Click(object sender, RoutedEventArgs e)
        //{
        //    //SaveFileDialog FileName = "Justificatif absence " + TBx_App_nom_home.Text + " "+ TBx_App_date_abs.Text;

        //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        //    dlg.FileName = "Justificatif absence " + TBx_App_nom_home.Text + " " + TBx_App_date_abs.Text; // Default file name
        //    //dlg.DefaultExt = ".text"; // Default file extension
        //    //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();



        //    cn.Open();
        //    string sql = " SELECT doc FROM Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "' AND idAbs = @idAbs";
        //    using (SqlCommand cmd = new SqlCommand(sql, cn))
        //    {
        //        //Pass byte array into database
        //        cmd.Parameters.AddWithValue("idAbs", TxtBox_idAbs.Text);
        //        byte[] fichier = (byte[])cmd.ExecuteScalar();
        //        using (FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create))
        //        {
        //            BinaryWriter write = new BinaryWriter(fileStream);
        //            write.Write(fichier);
        //            write.Close();
        //        }
        //    }


        //    //// Process save file dialog box results
        //    //if (result == true)
        //    //{
        //    //    // Save document
        //    //    TBx_App_fileJustif.Text = dlg.FileName;
        //    //}
        //}

        //private void Click(object sender, SelectionChangedEventArgs e)
        //{
        //    DataRowView row_selected = Datagrid_App_AddAbs.SelectedItem as DataRowView;

        //    if (row_selected != null)
        //    {
        //        TxtBox_idAbs.Text = row_selected["idAbs"].ToString();
        //    }
        //}
    }
}
