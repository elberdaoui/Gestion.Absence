using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Logique d'interaction pour AdminSecrétaire.xaml
    /// </summary>
    public partial class AdminSecrétaire : Window
    {
        public SqlConnection connection = new SqlConnection();
        String connect = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        int Position;

        public AdminSecrétaire()
        {
            InitializeComponent();
            getdata();
        }

        public void getdata()
        {
            try
            {


                connection.ConnectionString = connect;
                connection.Open();
                string query = "SELECT * from Secretaire";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader rd = command.ExecuteReader();
                if (rd.HasRows)
                {



                    DG_Zone.ItemsSource = rd;


                }
            }
            catch (Exception a)
            {

                MessageBox.Show(a.Message);

            }
        }

        //private void btn_edit_Click(object sender, RoutedEventArgs e)
        //{
        //    string query = "UPDATE Secretaire SET idSecretary = @name, C_fname= @fname, mail = @mail, ville = @ville WHERE id = @ID";
        //    using (SqlConnection conn = new SqlConnection(connect))
        //    {

        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@name", this.input_name_c.Text.Trim());
        //        cmd.Parameters.AddWithValue("@fname", this.input_lName_c.Text.Trim());
        //        cmd.Parameters.AddWithValue("@mail", this.input_mail_c.Text.Trim());
        //        cmd.Parameters.AddWithValue("@ville", this.input_ville_c.Text.Trim());
        //        cmd.Parameters.AddWithValue("@ID", this.input_id_C.Text.Trim());
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();

        //        MessageBox.Show("row id edited");
        //    }
        //}

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                string query = $"INSERT INTO Secretaire (  nom ,prenom , email,mdp,pdp,phone ) VALUES ('{input_Lname.Text}', '{input_name.Text}','{input_mail.Text}','{input_mdp.Text}','{input_pdp.Text}','{input_phone.Text}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("recored added successfully");
                getdata();


            }
            catch (Exception ax)
            {

                MessageBox.Show(ax.Message);
            }

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                string query = $"DELETE FROM  Secretaire WHERE idSecretary = '{int.Parse(id_delete.Text)}' ";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("recored deleted successfully");
                getdata();
            }
            catch (Exception errdel)
            {
                MessageBox.Show(errdel.Message);

            }

        }



        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            connection.ConnectionString = connect;
            connection.Open();
            string query = $"UPDATE Secretaire SET nom='{input_Lname.Text}', prenom='{input_name.Text}'  , email='{input_mail.Text}' , mdp='{input_mdp.Text}' , pdp='{input_mdp.Text}' , phone='{input_phone.Text}'   WHERE idSecretary=15";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
