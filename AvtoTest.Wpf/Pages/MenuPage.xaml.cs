using AvtoTest.Wpf.Enum;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
 

namespace AvtoTest.Wpf.Pages
{
 
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            SetImage();
        }

        private void SetImage()
        {
            MenuImage.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "Images", "AvtoHome.jpg")));
        }

        private void TicketsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DisplayPage(EPages.Tickets);
        }

        private void ExaminationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DisplayPage(EPages.Examination);
        }
    }
}
