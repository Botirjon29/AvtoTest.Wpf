using System.Windows;

namespace AvtoTest.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for RestartWindow.xaml
    /// </summary>
    public partial class RestartWindow : Window
    {
        public RestartWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
            this.Close();
        }
    }
}
