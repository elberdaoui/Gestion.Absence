using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace absence
{
    /// <summary>
    /// Interaction logic for Formateur.xaml
    /// </summary>
    public partial class Formateur : Window
    {
        SqlConnection cn = new SqlConnection("Data Source=DESKTOP-0PAGHK2;Initial Catalog=DATABASE;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public string date = DateTime.Now.ToString("dd-MM-yyyy");
        public string month = DateTime.Now.ToString("MMMM");
        public string dateTime = DateTime.Now.ToString("t");


        public Formateur()
        {
            InitializeComponent();
        }

        private void FormDash_Loaded(object sender, RoutedEventArgs e)
        {
            cmd.CommandText = " SELECT idApprenant, nom, prenom, email, phone, adresse from Apprenant WHERE idFormateur = '" + TBx_Form_id_info.Text + "' ";

            cmd.Connection = cn;
            DataTable dt = new DataTable();
            cn.Open();
            dr = cmd.ExecuteReader();
            dt.Load(dr);

            dataGrid_Form_home.ItemsSource = dt.DefaultView;

            cn.Close();
        }

        private void Btn_Frm_dash_Click(object sender, RoutedEventArgs e)
        {
            Grid_dash_App.Visibility = Visibility.Visible;
            //Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
            Grid_frm_info.Visibility = Visibility.Hidden;
        }

        private void Btn_Frm_prfl_Click(object sender, RoutedEventArgs e)
        {
            cn.Open();

            cmd = new SqlCommand(" SELECT f.nom, f.prenom, f.email, f.idFormateur, f.groupe, f.phone, fm.formation , f.pdp FROM Formateur f LEFT JOIN Formation fm ON f.idFormation = fm.idFormation WHERE idFormateur = '" + TBx_Form_id_info.Text + "'; ", cn);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            TBx_frm_info_nom.Text = dr[0].ToString();
            TBx_frm_info_prenom.Text = dr[1].ToString();
            //TBx_app_info_format.Text = dr[7].ToString() + " " + dr[8].ToString();
            TBx_frm_info_mail.Text = dr[2].ToString();
            //TBx_frm_info_groupe.Text = dr[4].ToString();
            TBx_frm_info_phone.Text = dr[5].ToString();
            TBx_frm_info_forma.Text = dr[6].ToString();
            if (TBx_frm_info_forma.Text == "C#" || TBx_frm_info_forma.Text == "JAVA" || TBx_frm_info_forma.Text == "FEBE")
            {
                TBx_frm_info_groupe.Text = "-";
            }
            else
            {
                TBx_frm_info_groupe.Text = dr[4].ToString();
            }
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

            Grid_frm_info.Visibility = Visibility.Visible;
            Grid_dash_App.Visibility = Visibility.Hidden;
            //Grid_App_filtrAbsperMois.Visibility = Visibility.Hidden;
            cn.Close();
        }


        private void abs_chk_Checked(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGrid_Form_home.SelectedItem;
            int id = (int)dataRow.Row.ItemArray[0];
            MessageBox.Show(id.ToString());
            MessageBox.Show(month);
            //cmd.CommandText = "UPDATE Absence SET justif ='" + txbx_just_scrt.Text + "' WHERE idAbs ='" + id +"'";

            cmd.CommandText = "INSERT INTO Absence (idApprenant, dateAbs, mois) VALUES ('" + id + "', '" + date + "', '" + month + "')";

            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            //getdata();

        }

        private void abs_chk_Unchecked(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGrid_Form_home.SelectedItem;
            int id = (int)dataRow.Row.ItemArray[0];
            MessageBox.Show(id.ToString());

            cmd.CommandText = "DELETE FROM Absence WHERE idApprenant = '" + id + "' AND dateAbs = '" + date + "'";

            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            //getdata();

        }

        private void late_chk_Checked(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGrid_Form_home.SelectedItem;
            int id = (int)dataRow.Row.ItemArray[0];

            string startTime = "9:00 AM";
            string endTime = dateTime;
            TimeSpan elapsed = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            MessageBox.Show(endTime);
            MessageBox.Show(elapsed.TotalHours.ToString());
            if (elapsed.TotalHours < 4)
            {
                MessageBox.Show("1ère demi-journée");
                cmd.CommandText = "INSERT INTO Absence (idApprenant, retard, dateRtd) VALUES ('" + id + "', '1ère demi-journée', '" + date + "')";

                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                //getdata();
            }
            else
            {
                MessageBox.Show("2nd half day");
                MessageBox.Show("2ème demi-journée");
                cmd.CommandText = "INSERT INTO Absence (idApprenant, retard, dateRtd) VALUES ('" + id + "', '2ème demi-journée', '" + date + "')";

                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                //getdata();
            }
        }

        private void late_chk_Unchecked(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dataGrid_Form_home.SelectedItem;
            int id = (int)dataRow.Row.ItemArray[0];
            cmd.CommandText = "DELETE FROM Absence WHERE idApprenant = '" + id + "' AND dateRtd = '" + date + "'";
            cmd.Connection = cn;
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            //getdata();
        }

        private void btn_déco_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearTxt(object sender, MouseButtonEventArgs e)
        {
            TBx_Search.Clear();

        }

        private void BackText(object sender, MouseEventArgs e)
        {
            TBx_Search.Text = "Chercher...";


        }

        private void TBx_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //cmd.Connection = cn;
            //DataTable dtb = new DataTable();
            //cn.Open();
            //string query = $"select idApprenant, nom, prenom, email, phone, adresse from Apprenant where nom like '%{TBx_Search.Text}%' or prenom like '%{TBx_Search.Text}%'";
            //SqlCommand com = new SqlCommand(query,cn);

           
            //dr = com.ExecuteReader();
            //dtb.Load(dr);
            //dataGrid_Form_home.ItemsSource = dtb.DefaultView;
        }
    }
}
