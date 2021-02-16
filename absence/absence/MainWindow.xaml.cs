using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace absence
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection cn = new SqlConnection("initial catalog= absenseGestion; data source= DESKTOP-0PAGHK2; integrated security= true;");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_cn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_cn_Click_1(object sender, RoutedEventArgs e)
        {
            //Open connection
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" SELECT Login.status, Login.nom, Login.prenom, Login.idApprenant, Login.idFormateur, Login.idSecretaire, Formation.formation, Login.email FROM Login LEFT JOIN Formation ON Login.idFormation = Formation.idFormation WHERE email= '" + TBx_email.Text + "' AND mdp= '"
                + PassBx_mdp.Password + "' ", cn);

            SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.Read() == true) MessageBox.Show("Connection réussie !");// .Read() == .HasRows
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "Admin")
                {
                    AdminDashB Admin = new AdminDashB();
                    Admin.Show();
                    this.Hide();
                }
                else if (dr[0].ToString() == "Secrétaire")
                {
                    SecretaireDashB Secrétaire = new SecretaireDashB();
                    Secrétaire.Show();
                    this.Hide();
                    Secrétaire.TBx_App_nom_home.Text = dr[1].ToString() + " " + dr[2].ToString();
                    Secrétaire.TBx_Secrt_id_info.Text = dr[5].ToString();
                    Secrétaire.Tbx_Secrt_forma_info.Text = dr[7].ToString();
                }
                else if (dr[0].ToString() == "Formateur")
                {
                    FormatDashB formateur = new FormatDashB();
                    formateur.Show();
                    this.Hide();
                    formateur.TBx_App_nom_home.Text = dr[1].ToString() + " " + dr[2].ToString();
                    formateur.Tbx_App_forma_info.Text = dr[6].ToString();
                    formateur.TBx_Form_id_info.Text = dr[4].ToString();
                }
                else if (dr[0].ToString() == "Apprenant")
                {
                    ApprdashB aprn = new ApprdashB();
                    aprn.Show();
                    this.Hide();
                    aprn.TBx_App_nom_home.Text = dr[1].ToString() + " " + dr[2].ToString();
                    aprn.Tbx_App_forma_info.Text = dr[6].ToString();
                    aprn.TBx_App_id_info.Text = dr[3].ToString();
                }

            }
            else MessageBox.Show("Email ou mot de passe invalide ! Veuillez réessayer.");

            cn.Close();

        }

        private void ClearTxt(object sender, MouseButtonEventArgs e)
        {
            TBx_email.Clear();
        }
    }
}
