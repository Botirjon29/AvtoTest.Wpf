using DatabaseForNewAutoTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace New_AutoTest.Pages
{
    public partial class ExaminationPage : Page
    {
        private int currentQuestionIndex = 0;
        Ticket currentTicket;
        public ExaminationPage(int currentTicketIndex = -1)
        {
            InitializeComponent();
            if (currentTicketIndex <= -1)
            {
                var random = new Random();
                currentTicketIndex = random.Next(0, MainWindow.Instance.QuestionRepository.GetTicketsCount());
            }

            TitleLabel.Content = $"Ticket {currentTicketIndex + 1}";
            CreateTicket(currentTicketIndex);
            GenarateQuestionIndexButtons();
            ShowQuestion();
        }

        public void CreateTicket(int ticketIndex)
        {
            var questionsCount = MainWindow.Instance.QuestionRepository.TicketQuestionsCount;
            var from = ticketIndex * questionsCount;
            var ticketQuestions = MainWindow.Instance.QuestionRepository.GetQuestionsRange(from, questionsCount);
            currentTicket = new Ticket(ticketIndex, ticketQuestions);
        }

        private void GenarateQuestionIndexButtons()
        {
            for (int i = 0; i < currentTicket.QuestionsCount; i++)
            {
                var button = new Button();
                if (i == 0)
                {
                    button.Style = FindResource("QuestionIndexButtonStyle") as Style;
                }
                else
                {
                    button.Style = FindResource("DefaultQuestionIndexButtonStyle") as Style;
                }

                button.Content = i + 1;
                button.Tag = i;
                button.Click += QuestionIndexButton_Click;
                QuestionsIndexPanel.Children.Add(button);
            }

        }

        private void QuestionIndexButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Style = FindResource("QuestionIndexButtonStyle") as Style;
            var defaultButton = QuestionsIndexPanel.Children[currentQuestionIndex] as Button;
            defaultButton!.Style = FindResource("DefaultQuestionIndexButtonStyle") as Style;
            currentQuestionIndex = (int)button.Tag;

            ShowQuestion();
        }

        private void ShowQuestion()
        {
            var question = currentTicket.Questions[currentQuestionIndex];/*new QuestionRepository();*/
            QuestionText.Text = $"{currentQuestionIndex + 1}. {question.Question}";

            GenarateChoicesButton(question.Choices!);
            SetQuestionImage(question.Media);
        }

        private void SetQuestionImage(Media? media)
        {
            if (media!.Exist)
            {
                QuestionImage.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "Images", $"{media?.Name}.png")));
            }
            else
            {
                QuestionImage.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "Images", $"NoImage.png")));
            }
        }

        private void GenarateChoicesButton(List<Choice> choices)
        {
            ChoicesPanel.Children.Clear();
            for (int i = 0; i < choices.Count; i++)
            {
                var choice = choices[i];

                var button = new Button();
                if (currentTicket.IsChoiceCompleted(currentQuestionIndex,i))
                {
                    if (choice.Answer)
                    {
                        button.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                    else
                    {
                        button.Background = new SolidColorBrush(Colors.Red);
                    }
                }
                else
                {
                    button.Background = Brushes.Orange;
                }
                button.MinHeight = 30;
                button.Width = 300;
                button.Margin = new Thickness(0, 0, 0, 10);
                button.Tag = choice;
                choice.Index = i;
                button.Click += ChoicesButton_Click;


                var textBlock = new TextBlock();
                textBlock.Text = choice.Text;
                textBlock.FontSize = 14;
                textBlock.FontWeight = FontWeights.DemiBold;
                textBlock.TextWrapping = TextWrapping.Wrap;
                button.Content = textBlock;
                ChoicesPanel.Children.Add(button);
            }
        }

        private void ChoicesButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTicket.IsQuestionCompleted(currentQuestionIndex)) return;
            var button = sender as Button;
            var choice = (Choice)button!.Tag;

            if (choice.Answer)
            {
                button.Background = new SolidColorBrush(Colors.LightGreen);
                (QuestionsIndexPanel.Children[currentQuestionIndex] as Button)!.Background = new SolidColorBrush(Colors.LightGreen); ;
                currentTicket.CorrectAnswerCount++;
            }
            else
            {
                button.Background = new SolidColorBrush(Colors.Red);
                (QuestionsIndexPanel.Children[currentQuestionIndex] as Button)!.Background = new SolidColorBrush(Colors.Red); ;
            }

            currentTicket.SelectedQuestionIndexes.Add(new TicketData(currentQuestionIndex,choice.Index));

            if (currentTicket.QuestionsCount == currentTicket.SelectedQuestionIndexes.Count)
            {
                var ticketResult = MainWindow.Instance.QuestionRepository.UserTicketResult;
                var isCompleted = ticketResult.Any(ut => ut.Index == currentTicket.Index);
                if (isCompleted)
                {
                    var oldTicket = ticketResult.First(ut => ut.Index == currentTicket.Index);
                    ticketResult.Remove(oldTicket);
                }
                ticketResult.Add(currentTicket);
                MainWindow.Instance.MainFrame.Navigate(new ExamResultPage(currentTicket));
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.MainFrame.Navigate(new MenuPage());
        }
    }
}
