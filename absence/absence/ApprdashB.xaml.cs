using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        SqlConnection cn = new SqlConnection("initial catalog= absGestion; data source= DESKTOP-TLLL2MM; integrated security= true;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

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

            cmd.CommandText = " SELECT TOP (3) idAbs as ID, dateAbs as Date, motif as Motif, justif as Justification from Absence WHERE idApprenant = 1 ";
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


            cmd.CommandText = " SELECT dateAbs, motif, justif from Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "' ";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            dataGrid_App_Abs.ItemsSource = dt.DefaultView;

            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_app_info.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Visible;
            cn.Close();
        }

        private void btn_App_filtre_mois_Click(object sender, RoutedEventArgs e)
        {
            cmd.CommandText = " SELECT dateAbs,  motif, justif from Absence WHERE mois = '" + CBx_mois_appr.Text + "' AND idApprenant = '" + TBx_App_id_info.Text + "'  ";
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
            cmd.CommandText = " SELECT dateAbs, motif, justif from Absence WHERE idApprenant = '" + TBx_App_id_info.Text + "'  ";
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
        }

        private void dataGrid_App_Abs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_App_prfl_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();

            cmd = new SqlCommand(" SELECT Apprenant.nom, Apprenant.prenom, Apprenant.email, Apprenant.idApprenant, Apprenant.adresse, Apprenant.phone, Formation.formation , Formateur.nom, Formateur.prenom, Apprenant.pdp FROM Apprenant LEFT JOIN Formation ON Apprenant.idFormation = Formation.idFormation LEFT JOIN Formateur ON Apprenant.idFormateur = Formateur.idFormateur WHERE idApprenant = '" + TBx_App_id_info.Text + "'; ", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            TBx_app_info_nom.Text = dr[0].ToString();
            TBx_app_info_prenom.Text = dr[1].ToString();
            TBx_app_info_format.Text = dr[7].ToString() + " " + dr[8].ToString();
            TBx_app_info_mail.Text = dr[2].ToString();
            TBx_app_info_adress.Text = dr[4].ToString();
            TBx_app_info_phone.Text = dr[5].ToString();
            TBx_app_info_forma.Text = dr[6].ToString();

            //byte[] img = ((byte[])dr[9]);

            //using (MemoryStream ms = new MemoryStream(img))
            //{
            //    img_perfil.Source = BitmapFrame.Create(ms,
            //                          BitmapCreateOptions.None,
            //                          BitmapCacheOption.OnLoad);
            //}
            //MemoryStream ms = new MemoryStream(img);
            //var bitMap = new BitmapImage();
            //bitMap.BeginInit();
            //bitMap.StreamSource = ms;
            //bitMap.EndInit();
            //img_perfil.Source = bitMap;

            //img_perfil.Source = Image.FromStream(ms);

            Grid_app_info.Visibility = Visibility.Visible;
            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
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
        {TBx_Search.Text = "Chercher...";
            
            
        }

    }
}
