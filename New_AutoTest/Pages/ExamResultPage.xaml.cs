using DatabaseForNewAutoTest.Model;
using System.Windows;
using System.Windows.Controls;

namespace New_AutoTest.Pages
{
    public partial class ExamResultPage : Page
    {
        public ExamResultPage(Ticket ticket)
        {
            InitializeComponent();
            CorrectAnswerBlock.Text = ticket.CorrectAnswerCount.ToString();
            QuestionBlock.Text = ticket.QuestionsCount.ToString();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.MainFrame.Navigate(new MenuPage());
        }
    }
}
