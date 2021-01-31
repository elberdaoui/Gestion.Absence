using System.Data.SqlClient;
using System.Windows;

namespace gestionAbs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Connection string
        SqlConnection cn = new SqlConnection("initial catalog= absGestion; data source= DESKTOP-TLLL2MM; integrated security= true;");

        public MainWindow()
        {
            InitializeComponent();

        ////Show etat connection
        //MessageBox.Show(cn.State.ToString());
    }

        private void btn_cn_Click(object sender, RoutedEventArgs e)
        {            
        //Open connection
        cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" SELECT status FROM Login WHERE email= '" + inp_mail.Text + "' AND mdp= '"
                + passBox_login.Password + "' ", cn);

            SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.Read() == true) MessageBox.Show("Connection réussie !");// .Read() == .HasRows
            if (dr.HasRows)
            {   
                dr.Read();
                if(dr[0].ToString() == "Admin")
                {
                    MessageBox.Show("Bienvenue Admin !");
                }
                else if (dr[0].ToString() == "Secrétaire") 
                {
                    MessageBox.Show("Bienvenue Secrétaire !");
                }
                else if (dr[0].ToString() == "Formateur")
                {
                    MessageBox.Show("Bienvenue Formateur !");
                }
                else if (dr[0].ToString() == "Apprenant")
                {
                    MessageBox.Show("Bienvenue Apprenant !");
                }

            }
            else MessageBox.Show("Email ou mot de passe invalide ! Veuillez réessayer.");

        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
