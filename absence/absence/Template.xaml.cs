using System.Windows;
using System.Windows.Input;

namespace absence
{
    /// <summary>
    /// Logique d'interaction pour Template.xaml
    /// </summary>
    public partial class Template : Window
    {
        public Template()
        {
            InitializeComponent();
        }

        private void ClearTxt(object sender, MouseButtonEventArgs e)
        {
            TBx_Search.Clear();

        }

        private void BackText(object sender, MouseEventArgs e)
        {
            TBx_Search.Text = "Chercher...";


        }
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
    }
}
