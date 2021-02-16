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
    /// Logique d'interaction pour SecretaireDashB.xaml
    /// </summary>
    public partial class SecretaireDashB : Window
    {
        public SecretaireDashB()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("initial catalog= absenseGestion; data source= DESKTOP-0PAGHK2; integrated security= true;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackText(object sender, MouseEventArgs e)
        {
            TBx_Search.Text = "Chercher...";
        }
        private void ClearTxt(object sender, MouseButtonEventArgs e)
        {
            TBx_Search.Clear();
            TBx_Scrt_Abs_FiltreNom.Clear();

        }

        private void SecretaireDashB_Loaded(object sender, RoutedEventArgs e)
        {
            cmd.CommandText = "SELECT TOP (4) Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.justif as Etat from Apprenant LEFT JOIN Absence ON Apprenant.idApprenant = Absence.idApprenant Order By Absence.idAbs DESC";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            dataGrid_Scrt_home.ItemsSource = dt.DefaultView;
            cn.Close();
        }

        private void Btn_Scrt_Abs_gestion_Click(object sender, RoutedEventArgs e)
        {
            Grid_Scrt_DashB.Visibility = Visibility.Hidden;
            Grid_Scrt_abs_gestion.Visibility = Visibility.Visible;
            Grid_Scrt_monProfil.Visibility = Visibility.Hidden;

            cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
            cn.Close();
        }

        private void btn_Scrt_filtre_Click(object sender, RoutedEventArgs e)
        {
            btn_Scrt_filtre_Reinit.Visibility = Visibility.Visible;
            if (CBx_Scrt_etatFiltreAbs.Text != "" && DatePick_Scrt_Abs_filtre.Text == "" && TBx_Scrt_Abs_FiltreNom.Text == "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif,  Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.justif = '" + CBx_Scrt_etatFiltreAbs.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            }
            else if(CBx_Scrt_etatFiltreAbs.Text == "" && DatePick_Scrt_Abs_filtre.Text != "" && TBx_Scrt_Abs_FiltreNom.Text == "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.dateAbs = '" + DatePick_Scrt_Abs_filtre.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            }
            else if (CBx_Scrt_etatFiltreAbs.Text == "" && DatePick_Scrt_Abs_filtre.Text == "" && TBx_Scrt_Abs_FiltreNom.Text != "" && TBx_Scrt_Abs_FiltreNom.Text != "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Apprenant.nom = '" + TBx_Scrt_Abs_FiltreNom.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            
            
            }else if (CBx_Scrt_etatFiltreAbs.Text != "" && DatePick_Scrt_Abs_filtre.Text != "" && TBx_Scrt_Abs_FiltreNom.Text == "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.justif = '" + CBx_Scrt_etatFiltreAbs.Text + "' AND Absence.dateAbs = '" + DatePick_Scrt_Abs_filtre.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            }
            else if (CBx_Scrt_etatFiltreAbs.Text != "" && DatePick_Scrt_Abs_filtre.Text != "" && TBx_Scrt_Abs_FiltreNom.Text != "" && TBx_Scrt_Abs_FiltreNom.Text != "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.justif = '" + CBx_Scrt_etatFiltreAbs.Text + "' AND Absence.dateAbs = '" + DatePick_Scrt_Abs_filtre.Text + "' AND Apprenant.nom = '" + TBx_Scrt_Abs_FiltreNom.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            }
            else if(CBx_Scrt_etatFiltreAbs.Text == "" && DatePick_Scrt_Abs_filtre.Text != "" && TBx_Scrt_Abs_FiltreNom.Text != "" && TBx_Scrt_Abs_FiltreNom.Text != "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.dateAbs = '" + DatePick_Scrt_Abs_filtre.Text + "' AND Apprenant.nom = '" + TBx_Scrt_Abs_FiltreNom.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            }
            else if (CBx_Scrt_etatFiltreAbs.Text != "" && DatePick_Scrt_Abs_filtre.Text == "" && TBx_Scrt_Abs_FiltreNom.Text != "" && TBx_Scrt_Abs_FiltreNom.Text != "Chercher un nom...")
            {
                cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.justif = '" + CBx_Scrt_etatFiltreAbs.Text + "' AND Apprenant.nom = '" + TBx_Scrt_Abs_FiltreNom.Text + "' ";
                cmd.Connection = cn;
                DataTable dt = new DataTable();
                cn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
                cn.Close();
            }
            else
            {
                MessageBox.Show("Veuillez utiliser un filtre !");
            }
        }

        private void btn_Scrt_filtre_Reinit_Click(object sender, RoutedEventArgs e)
        {
            btn_Scrt_filtre_Reinit.Visibility = Visibility.Hidden;
            CBx_Scrt_etatFiltreAbs.SelectedIndex = -1;
            TBx_Scrt_Abs_FiltreNom.Text = "Chercher un nom...";
            DatePick_Scrt_Abs_filtre.SelectedDate = null;

            cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
            cn.Close();
        }

        private void btn_Scrt_Abs_save_Click(object sender, RoutedEventArgs e)
        {
            getDataAbs();
            btn_Scrt_Abs_voirDetailApp.Visibility = Visibility.Hidden;
        }

        private void btn_Scrt_Abs_download_justif_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid_Scrt_Abs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                TBx_Scrt_Abs_AppId.Text = row_selected["ID"].ToString();
                btn_Scrt_Abs_voirDetailApp.Visibility = Visibility.Visible;
            }
        }

        private void btn_Scrt_Abs_voirDetailApp_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" SELECT  Apprenant.nom, Apprenant.prenom , Absence.justif, Absence.doc, Apprenant.idApprenant, Absence.idAbs from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant WHERE Absence.idAbs = '" + TBx_Scrt_Abs_AppId.Text + "' ", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
               
                SecretaireApprenantInfo info = new SecretaireApprenantInfo();
                info.Show();
                info.TBx_app_info_nom.Text = dr[0].ToString();
                info.TBx_app_info_prenom.Text = dr[1].ToString();
                info.CBx_Scrt_etatFiltreAbs.Text = dr[2].ToString();
                info.TBx_app_info_idAbs.Text = dr[5].ToString();
                info.TBx_app_abs_justif.Text= Encoding.ASCII.GetString((byte[])dr[3]);
                //info.TBx_app_info_id.Text = dr[4].ToString();
                //info.TBx_app_abs_justif.Text = dr[3].ToString();
            }
            cn.Close();
        }

        private void getDataAbs()
        {
            SqlDataReader dr;
            

            cmd.CommandText = "SELECT Absence.idAbs as ID, Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.motif as Motif, Absence.justif as Etat, Absence.doc as Justificatif from Absence LEFT JOIN Apprenant ON Absence.idApprenant = Apprenant.idApprenant ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            dataGrid_Scrt_Abs.ItemsSource = dt.DefaultView;
            

            cmd.CommandText = "SELECT TOP (4) Apprenant.nom as Nom, Apprenant.prenom as Prénom, Absence.dateAbs as Date, Absence.justif as Etat from Apprenant LEFT JOIN Absence ON Apprenant.idApprenant = Absence.idApprenant Order By Absence.idAbs DESC";
            cmd.Connection = cn;
            DataTable dt1 = new DataTable();
            
            dr = cmd.ExecuteReader();
            dt1.Load(dr);
            dataGrid_Scrt_home.ItemsSource = dt1.DefaultView;
            cn.Close();
        }

        private void Btn_App_prfl_Click(object sender, RoutedEventArgs e)
        {
            Grid_Scrt_DashB.Visibility = Visibility.Hidden;
            Grid_Scrt_abs_gestion.Visibility = Visibility.Hidden;
            Grid_Scrt_monProfil.Visibility = Visibility.Visible;
        }

        private void Btn_App_dash_Click(object sender, RoutedEventArgs e)
        {
            Grid_Scrt_DashB.Visibility = Visibility.Visible;
            Grid_Scrt_abs_gestion.Visibility = Visibility.Hidden;
            Grid_Scrt_monProfil.Visibility = Visibility.Hidden;
        }
    }
}
