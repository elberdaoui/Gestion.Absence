using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NuGet.Protocol.Plugins;
using VisioForge.Shared.MediaFoundation.OPM;

namespace gestionAbs
{
    /// <summary>
    /// Interaction logic for Formateur.xaml
    /// </summary>
    public partial class Formateur : Page
    {
        Queries qr = new Queries();
        public string date = DateTime.Now.ToString("dd-MM-yyyy");
        public string dateTime = DateTime.Now.ToString();
        public Formateur()
        {
            InitializeComponent();
            ShowData();
            
        }

        private void ShowData()
        {
            qr.Connect();

            qr.cmd.CommandText = "select a.idApprenant, a.nom, a.prenom, a.email, a.phone, f.formation from Apprenant a INNER JOIN Formation f ON a.idFormation=f.idFormation";
            qr.cmd.Connection = qr.cnx;
            qr.dtr = qr.cmd.ExecuteReader();
            qr.dt.Load(qr.dtr);
            dg.ItemsSource = qr.dt.DefaultView;
            
            qr.dtr.Close();
            //abs_chk.Visibilty = Visibility.Hidden;
            late_temp.Visibility = Visibility.Hidden;
            abs_temp.Visibility = Visibility.Hidden;
        }

        public void stdList_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void absence_Click(object sender, RoutedEventArgs e)
        {
            late_temp.Visibility = Visibility.Visible;
            abs_temp.Visibility = Visibility.Visible;

            

        }

        private void abs_chk_Checked(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("checked");


            //int posID = Convert.ToInt32(((CheckBox)sender).Name);
            //foreach(DataRow row in qr.dt.Rows)
            // {
            //     int idx = qr.dt.Rows.IndexOf(row);
            // }
            //var idx = qr.dt.Select("idApprenant = 'this'");
            //int posID = idx;
            
            MessageBox.Show(dateTime);
            int posID = dg.Items.IndexOf(dg.CurrentItem) + 8;
            MessageBox.Show(posID.ToString());
            qr.cmd.CommandText = "update Apprenant set absent = '" + date + "', retard = 'Non' where idApprenant = '" + posID + "'";
            qr.cmd.Connection = qr.cnx;
            qr.cmd.ExecuteNonQuery();
            
            
        }

        private void abs_chk_Unchecked(object sender, RoutedEventArgs e)
        {
            int posID = dg.Items.IndexOf(dg.CurrentItem) + 8;
            MessageBox.Show(posID.ToString());
            qr.cmd.CommandText = "update Apprenant set absent = 'Non', retard = 'Non' where idApprenant = '" + posID + "'";
            qr.cmd.Connection = qr.cnx;
            qr.cmd.ExecuteNonQuery();
        }

        private void late_chk_Checked(object sender, RoutedEventArgs e)
        {
            int posID = dg.Items.IndexOf(dg.CurrentItem) + 8;
            MessageBox.Show(posID.ToString());
            qr.cmd.CommandText = "update Apprenant set absent = 'Non', retard = 'Oui' where idApprenant = '" + posID + "'";
            qr.cmd.Connection = qr.cnx;
            qr.cmd.ExecuteNonQuery();
        }

        private void late_chk_Unchecked(object sender, RoutedEventArgs e)
        {
            int posID = dg.Items.IndexOf(dg.CurrentItem) + 8;
            MessageBox.Show(posID.ToString());
            qr.cmd.CommandText = "update Apprenant set absent = 'Non', retard = 'Non' where idApprenant = '" + posID + "'";
            qr.cmd.Connection = qr.cnx;
            qr.cmd.ExecuteNonQuery();
        }

        private void discon_Click(object sender, RoutedEventArgs e)
        {
            qr.Disconnect();
            //this.NavigationService.GoBack();
            MainWindow main = new MainWindow();
            //NavigationService.Navigate(main);
            main.frame.Content = null;
            main.frame.Visibility = Visibility.Hidden;
            main.Close();
            //main.Show();
           
        }
    }
}
