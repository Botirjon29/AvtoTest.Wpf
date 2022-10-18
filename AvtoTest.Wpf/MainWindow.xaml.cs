using AvtoTest.Wpf.Enum;
using AvtoTest.Wpf.Pages;
using System;
using System.Windows;

namespace AvtoTest.Wpf
{

    public partial class MainWindow : Window
    {
        private static MainWindow _instance;
        public static MainWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainWindow();
                return _instance;
            }

        }

        public MainWindow()
        {
            InitializeComponent();
            _instance = this;

            MainFrame.Navigate(new MenuPage());
        }

        public void DisplayPage(EPages page)
        {
            switch (page)
            {
                case EPages.Menu:
                    MainFrame.Navigate(new MenuPage());
                    break;
                case EPages.Examination:
                    MainFrame.Navigate(new ExaminationPage());
                    break;
                case EPages.Tickets:
                    MainFrame.Navigate(new TicketsPage());
                    break;

            }
        }

        public void DisplayTicketPage(int index)
        {
             MainFrame.Navigate(new ExaminationPage(index));
        }
    }
}
