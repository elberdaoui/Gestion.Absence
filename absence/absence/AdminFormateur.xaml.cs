using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Logique d'interaction pour AdminFormateur.xaml
    /// </summary>
    public partial class AdminFormateur : Window
    {
        private SqlConnection connection = new SqlConnection();
        String connect = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;

        public AdminFormateur()
        {
            InitializeComponent();
            loaddata();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                string query = $"INSERT INTO Formateur ( nom, prenom, email, mdp  , phone ,  idFormation  ) values ('{input_Lname.Text}','{input_name.Text}','{input_mail.Text}','{input_mdp.Text}','{input_phone.Text}',{Int64.Parse(idform.Text)})";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("records has been Added");
                connection.Close();
                loaddata();

            }
            catch (Exception adderr)
            {

                MessageBox.Show(adderr.Message);
            }
        }





        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();

                string query = $"DELETE FROM  Formateur WHERE idFormateur={int.Parse(id_delete.Text)}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("records has been deleted");
                loaddata();
            }
            catch (Exception delerr)
            {

                MessageBox.Show(delerr.Message);
            }
        }




        private void loaddata()
        {

            try
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Formateur", connection);
                command.ExecuteNonQuery();
                SqlDataReader dr = command.ExecuteReader();
                DG_Zone.ItemsSource = dr;

            }
            catch (Exception loaderr)
            {

                MessageBox.Show(loaderr.Message);
            }



        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.ConnectionString = connect;
                connection.Open();
                string query = $"UPDATE Formateur SET prenom='{input_name.Text}', nom='{input_Lname.Text}'  , email='{input_mail.Text}' , mdp='{input_mdp.Text}' , phone= '{input_phone.Text}' , idFormation= {int.Parse(idform.Text)}  WHERE idFormateur={int.Parse(id_edit.Text)}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                loaddata();
            }
            catch (Exception ederr)
            {

                MessageBox.Show(ederr.Message);
            }
        }
    }
}
