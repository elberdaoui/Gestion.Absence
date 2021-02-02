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
        SqlCommand cmd = new SqlCommand();
        Formateur frm = new Formateur();
        ApprdashB aprn = new ApprdashB();

        //public void mynbrAbsJust()
        //{
        //    //cn.Open();

        //    cmd = new SqlCommand("select count(justif) from Absence where idApprenant = '" + aprn.TBx_App_id_info.Text + "' and justif = 'oui' ", cn);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    dr.Read();
        //    aprn.NabsJus.Text = dr[0].ToString();
        //    dr.Close();
        //    //cn.Close();
        //}

        //public void mynbrAbsNonJust()
        //{
        //    //cn.Open();
        //    cmd = new SqlCommand("select count(justif) from Absence where idApprenant = '" + aprn.TBx_App_id_info.Text + "' and justif = 'non' ", cn);
        //    SqlDataReader dtr = cmd.ExecuteReader();
        //    dtr.Read();
        //    aprn.NabsNJus.Text = dtr[0].ToString();

        //    //cn.Close();
        //}


        //SqlConnection cn = new SqlConnection("initial catalog= absGestion; data source= DESKTOP-TLLL2MM; integrated security= true;");
        SqlConnection cn = new SqlConnection("Data Source=DESKTOP-0PAGHK2;Initial Catalog=DATABASE;Integrated Security=True");

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
            cmd = new SqlCommand(" SELECT Login.status, Login.nom, Login.prenom, Login.idApprenant, Login.idFormateur, Login.idSecretaire, Formation.formation FROM Login LEFT JOIN Formation ON Login.idFormation = Formation.idFormation WHERE email= '" + TBx_email.Text + "' AND mdp= '"
                + PassBx_mdp.Password + "' ", cn);

            SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.Read() == true) MessageBox.Show("Connection réussie !");// .Read() == .HasRows
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "Admin")
                {
                    MessageBox.Show("Bienvenue Admin !");
                }
                //else if (dr[0].ToString() == "Secrétaire")
                //{
                //    DashSecrétaire Secrétaire = new DashSecrétaire();
                //    Secrétaire.Show();
                //    this.Hide();
                //    Secrétaire.txtBl_Secr_Nom.Text = dr[1].ToString() + " " + dr[2].ToString();
                //    Secrétaire.txtBl_Secr_id.Text = dr[5].ToString();
                //}
                else if (dr[0].ToString() == "Formateur")
                {
                    
                    frm.Show();
                    this.Hide();
                    frm.TBx_App_nom_home.Text = dr[1].ToString() + " " + dr[2].ToString();
                    frm.Tbx_App_forma_info.Text = dr[6].ToString();
                    frm.TBx_Form_id_info.Text = dr[4].ToString();
                }
                else if (dr[0].ToString() == "Apprenant")
                {
                    
                    aprn.Show();
                    this.Hide();
                    aprn.TBx_App_nom_home.Text = dr[1].ToString() + " " + dr[2].ToString();
                    aprn.Tbx_App_forma_info.Text = dr[6].ToString();
                    aprn.TBx_App_id_info.Text = dr[3].ToString();
                    dr.Close();
                    //aprn.mynbrAbsJust();
                    //aprn.mynbrAbsNonJust();
                    aprn.Absence();
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
