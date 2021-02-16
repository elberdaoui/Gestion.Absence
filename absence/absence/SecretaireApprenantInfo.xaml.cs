using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Ubiety.Dns.Core;

namespace absence
{
    /// <summary>
    /// Logique d'interaction pour SecretaireApprenantInfo.xaml
    /// </summary>
    public partial class SecretaireApprenantInfo : Window
    {
        public SecretaireApprenantInfo()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("initial catalog= absenseGestion; data source= DESKTOP-0PAGHK2; integrated security= true;");
        SqlCommand cmd = new SqlCommand();
        

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Scrt_Abs_download_justif_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Justificatif absence_" + TBx_app_info_nom.Text + " " + TBx_app_info_prenom.Text+".pdf";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            SecretaireDashB secret = new SecretaireDashB();

            cn.Open();
            string sql = " SELECT doc FROM Absence WHERE idAbs = @ID";
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                //Pass byte array into database
                cmd.Parameters.AddWithValue("ID", secret.TBx_Scrt_Abs_AppId.Text);
                byte[] fichier = (byte[])cmd.ExecuteScalar();
                using (FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create))
                {
                    BinaryWriter write = new BinaryWriter(fileStream);
                    write.Write(fichier);
                    write.Close();
                }


            }

        }

        private void btn_Scrt_Abs_save_Click(object sender, RoutedEventArgs e)
        {
            SecretaireDashB secretDash = new SecretaireDashB();

            string query = $"UPDATE Absence SET  justif=  '" + CBx_Scrt_etatFiltreAbs.Text +"' WHERE idAbs = '" + TBx_app_info_idAbs.Text + "' ";
            //SqlConnection connection = new SqlConnection();
            //cn.ConnectionString = connect;
            cn.Open();
            SqlCommand command = new SqlCommand(query, cn);          
            command.ExecuteNonQuery();
            MessageBox.Show("Modification enregistrée !");
            
            cn.Close();

        }

        
    }
}
