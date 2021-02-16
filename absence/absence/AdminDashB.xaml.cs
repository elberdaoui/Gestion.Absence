using System;
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
    /// Logique d'interaction pour AdminDashB.xaml
    /// </summary>
    public partial class AdminDashB : Window
    {
        public AdminDashB()
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
            getcounterappr();
            getcounterforma();
            getcountersecret();





        }


        private void getcounterappr()
        {

            cn.Open();
            string query = $"SELECT COUNT(*) FROM Apprenant";
            SqlCommand command = new SqlCommand(query, cn);
            command.ExecuteNonQuery();

            // Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
            int numRecords = Convert.ToInt32(command.ExecuteScalar());

            txtcounterappre.Text = numRecords.ToString();
            cn.Close();

        }

        private void getcounterforma()
        {

            cn.Open();
            string query = $"SELECT COUNT(*) FROM Formateur";
            SqlCommand command = new SqlCommand(query, cn);
            command.ExecuteNonQuery();

            // Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
            int numRecords = Convert.ToInt32(command.ExecuteScalar());

            txtform.Text = numRecords.ToString();
            cn.Close();

        }






        private void getcountersecret()
        {

            cn.Open();
            string query = $"SELECT COUNT(*) FROM Secretaire";
            SqlCommand command = new SqlCommand(query, cn);
            //  command.ExecuteNonQuery();

            // Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
            int numRecords = Convert.ToInt32(command.ExecuteScalar());

            txtsecr.Text = numRecords.ToString();
            cn.Close();

        }








        private void Btn_Abs_App_Click(object sender, RoutedEventArgs e)
        {


            cmd.CommandText = " SELECT * FROM Apprenant";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            dataGrid_App_Abs.ItemsSource = dt.DefaultView;


            addFormAppre.Visibility = Visibility.Visible;
            txtname.Visibility = Visibility.Visible;
            Grid_sidebarInfo_home.Visibility = Visibility.Hidden;
            txtstat.Visibility = Visibility.Hidden;
            imgforma.Visibility = Visibility.Hidden;
            txtform.Visibility = Visibility.Hidden;
            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_app_info.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Visible;
            cn.Close();
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


            btn_App_filtre_mois_reinis.Visibility = Visibility.Hidden;

        }

        private void Btn_App_dash_Click(object sender, RoutedEventArgs e)
        {
            Grid_dash_App.Visibility = Visibility.Visible;

            Grid_app_info.Visibility = Visibility.Hidden;
        }



        private void Btn_App_prfl_Click(object sender, RoutedEventArgs e)
        {


            Grid_app_info.Visibility = Visibility.Visible;
            Grid_dash_App.Visibility = Visibility.Hidden;
            Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
            cn.Close();
        }


        private void ClearTxt(object sender, MouseButtonEventArgs e)
        {
            TBx_Search.Clear();

        }

        private void BackText(object sender, MouseEventArgs e)
        {
            TBx_Search.Text = "Chercher...";


        }

        private void btnaddppr_Click(object sender, RoutedEventArgs e)

        {
            cn.Open();
            string query = $"INSERT INTO Apprenant ( nom, prenom, email, mdp , phone ,  idFormation  ) values ('{input_Lname.Text}','{input_name.Text}','{input_mail.Text}','{input_mdp.Text}','{input_phone.Text}',{int.Parse(idform.Text)})";
            SqlCommand command = new SqlCommand(query, cn);
            command.ExecuteNonQuery();
            MessageBox.Show("records has been Added");

            cn.Close();
            loaddata();

        }


        private void loaddata()
        {
            cmd.CommandText = " SELECT * FROM Apprenant";
            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            dataGrid_App_Abs.ItemsSource = dt.DefaultView;
            cn.Close();

        }

        private void btndeleteappre_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();

            string query = $"DELETE FROM  Apprenant WHERE idApprenant = '{int.Parse(id_delete.Text)}'";
            SqlCommand command = new SqlCommand(query, cn);
            command.ExecuteNonQuery();
            MessageBox.Show("records has been deleted");
            cn.Close();
            loaddata();

        }



        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();
            string query = $"UPDATE Apprenant SET prenom='{input_name.Text}' , nom='{input_Lname.Text}'  , email='{input_mail.Text}' , mdp='{input_mdp.Text}' , idFormation={int.Parse(idform.Text)}   WHERE idApprenant={Int32.Parse(id_edit.Text)}";
            SqlCommand command = new SqlCommand(query, cn);
            command.ExecuteNonQuery();
            cn.Close();
            loaddata();

        }

        private void btn_formt_Click(object sender, RoutedEventArgs e)
        {
            AdminFormateur forma = new AdminFormateur();
            forma.Show();

        }

        private void btn_scrt_Click(object sender, RoutedEventArgs e)
        {
            AdminSecrétaire secre = new AdminSecrétaire();
            secre.Show();
        }

        private void btn_appr_Click(object sender, RoutedEventArgs e)
        {
            AdminApp appre = new AdminApp();
            appre.Show();
        }
    }
}
