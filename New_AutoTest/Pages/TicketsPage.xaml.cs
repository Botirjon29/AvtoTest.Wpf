using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace New_AutoTest.Pages
{
    public partial class TicketsPage : Page
    {
        public TicketsPage()
        {
            InitializeComponent();
            GenarateTicketsButton();
        }

        private void GenarateTicketsButton()
        {
            var questionRepository = MainWindow.Instance.QuestionRepository;
            var ticketsCount = questionRepository.GetTicketsCount();
            var ticketResult = MainWindow.Instance.QuestionRepository.UserTicketResult;
            for (int i = 0; i < ticketsCount; i++)
            {
                var button = new Button();
                if (ticketResult.Any(t => t.Index == i))
                {
                    var ticket = ticketResult.First(t => t.Index == i);
                    button.Content = ticket.TicketCompleted
                        ? $"Ticket {i + 1} \t✅"
                        : $"Ticket {i + 1} \t{ticket.CorrectAnswerCount}/{ticket.QuestionsCount}";
                }
                else
                {
                button.Content = $"Ticket" + (1 + i);
                }
                button.FontSize = 20;
                button.Width = 300;
                button.Height = 40;
                button.Background = Brushes.Aqua;
                button.Margin = new Thickness(0, 0, 0, 10);
                button.Tag = i;
                button.Click += TicketButton_Click;
                TicketsPanel.Children.Add(button);
            }
        }

        private void TicketButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ticketIndex = (int)(sender as Button)!.Tag;
            MainWindow.Instance.MainFrame.Navigate(new ExaminationPage(ticketIndex));
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.MainFrame.Navigate(new MenuPage());
        }
    }
}
