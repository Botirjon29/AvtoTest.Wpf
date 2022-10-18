using AvtoTest.Data.Models;
using AvtoTest.Data.Options;
using AvtoTest.Wpf.Enum;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AvtoTest.Wpf.Pages
{
 
    public partial class TicketsPage : Page
    {
        public TicketsPage()
        {
            InitializeComponent();
            ShowTickets();
        }

        private void ShowTickets()
        {
            TicketButtonsPanel.Children.Clear();
             
            for (int i = 0; i < Database.DB.TicketDb.GetTicketsCount(); i++)
            {
                var button = new Button();
                button.Style = FindResource("TicketButtonsStyle") as Style;
                button.Width = 300;
                button.Height = 30;
                button.FontSize = 16;
                button.Tag = i;
                button.Click += StartTicket_Click;

                if (Database.DB.TicketDb.UserTickets.Any(t => t.Index == i))
                {
                    var ticket = Database.DB.TicketDb.UserTickets.First(t => t.Index == i);

                    ticket.Text = ticket.IsCompleted
                        ? $"Ticket {i + 1} ✅"
                        : $"Ticket {i + 1} {ticket.CorrectAnswersCount}/{ticket.QuestionsCount}";

                    button.DataContext = ticket;
                }
                else
                {
                    button.DataContext = new TicketEntity(i, $"Ticket {i + 1}");
                }
                TicketButtonsPanel.Children.Add(button);
            }
        }

        private void StartTicket_Click(object sender, RoutedEventArgs e)
        {
            var ticket = (TicketEntity)(sender as Button)!.DataContext;
            if (Database.DB.TicketDb.UserTickets.Any(t => t.Index == ticket.Index))
            {
                var restartWindow = new RestartWindow();
                var result = restartWindow.ShowDialog();
                if (result == true)
                {
                    MainWindow.Instance.DisplayTicketPage(ticket.Index);
                }
                return;
            }
            MainWindow.Instance.DisplayTicketPage(ticket.Index);
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DisplayPage(EPages.Menu);
            
        }
    }
}
