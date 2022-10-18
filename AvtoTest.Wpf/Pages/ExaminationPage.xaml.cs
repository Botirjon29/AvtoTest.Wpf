using AvtoTest.Data.Models;
using AvtoTest.Data.Options;
using AvtoTest.Wpf.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace AvtoTest.Wpf.Pages
{

    public partial class ExaminationPage : Page
    {
        private TicketEntity CurrentTicket;
        private int currentQuestionIndex = 0;
        public ExaminationPage(int? ticketIndex = null)
        {
            InitializeComponent();
            CurrentTicket = ticketIndex == null
                ? Database.DB.TicketDb.CreateTicket()
                : CreateTicketByIndex(ticketIndex.Value);

            TicketOrExamLabel.Content = ticketIndex != null ? $"Ticket {ticketIndex + 1}" : "Examination";

            GenarateQuestionIndexButton();
            ShowQuestion();
        }

        private void GenarateQuestionIndexButton()
        {
            for (int i = 0; i < CurrentTicket.Questions.Count; i++)
            {
                var button = new Button();
                button.Style = FindResource("QuestionIndexButtonStyle") as Style;
                button.Content = i + 1;
                button.Tag = i;  // bu raw orqali qaysi indekdegi button bosilganini bilish uchun. u o'zida savolning yoki buttonning indeksini saqlab ketadi
                if (i == 0)
                {
                    button.Background = new SolidColorBrush(Colors.Aqua);
                }
                button.Click += QuestionIndexButton_Click;
                QuestionsIndexPanel.Children.Add(button);
            }
        }

        private void QuestionIndexButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (!CurrentTicket.Questions[currentQuestionIndex].IsCompleted)
            {
                (QuestionsIndexPanel.Children[currentQuestionIndex] as Button)!.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            }

            currentQuestionIndex = (int)button!.Tag;
            ShowQuestion();

            if (!CurrentTicket.Questions[currentQuestionIndex].IsCompleted)
            {
                button.Background = new SolidColorBrush(Colors.Aqua);
            }
        }

        private TicketEntity CreateTicketByIndex(int indexOfTicket)
        {
            var from = indexOfTicket * TicketsSettings.TicketQuestionsCount;
            var questions = Database.DB.QuestionsDb.CreateQTicket(from, TicketsSettings.TicketQuestionsCount);
            var ticket = new TicketEntity(indexOfTicket, new List<QuestionEntity>(questions));
            return ticket;
        }

        private void ShowQuestion()
        {
            var question = CurrentTicket.Questions[currentQuestionIndex];
            ShowImage(question.Media.Exist ? question.Media.Name : "NoImage");
            QuestionText.Text = $"{currentQuestionIndex + 1}. {question.Question}";
            ShowChoicesButton(question);
        }

        private void ShowImage(string imageName)
        {
            QuestionsImage.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "Images", $"{imageName}.png")));
        }

        private void ShowChoicesButton(QuestionEntity question)
        {
            ChoicesPanel.Children.Clear();
            for (int i = 0; i < question.Choices.Count; i++)
            {
                var button = new Button();
                button.Style = FindResource("ChoicesButtonStyle") as Style;

                if (question.IsCompleted)
                {
                    if (question.Choices[i].IsSelected)
                    {
                        if (question.Choices[i].Answer)
                        {
                            button.Background = new SolidColorBrush(Colors.LightGreen);
                        }
                        else
                        {
                            button.Background = new SolidColorBrush(Colors.Red);
                        }
                    }
                }

                button.Click += ChoicesButton_Click;
                button.DataContext = question.Choices[i];
                ChoicesPanel.Children.Add(button);
            }
        }

        private void ChoicesButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTicket.Questions[currentQuestionIndex].IsCompleted) return;

            var button = (Button)sender;
            var choice = (Choice)button.DataContext;
            if (choice.Answer)
            {
                button.Background = new SolidColorBrush(Colors.LightGreen);
                (QuestionsIndexPanel.Children[currentQuestionIndex] as Button)!.Background = new SolidColorBrush(Colors.LightGreen);
                CurrentTicket.CorrectAnswersCount++;

            }
            else
            {
                button.Background = new SolidColorBrush(Colors.Red);
                (QuestionsIndexPanel.Children[currentQuestionIndex] as Button)!.Background = new SolidColorBrush(Colors.Red);
            }

            choice.IsSelected = true;

            CurrentTicket.Questions[currentQuestionIndex].IsCompleted = true;
        }

        private void SetVisibility(StackPanel panel, bool isVisible)
        {
            panel.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
            panel.IsEnabled = isVisible;
        }


        private void ExamClose_click(object sender, RoutedEventArgs e)
        {
            SetVisibility(ExaminationPanel, false);
            SetVisibility(ResultPanel, true);
            ResultPanel.DataContext = CurrentTicket;
            if (TicketOrExamLabel.Content != "Examination")
            {
               Database.DB.TicketDb.UserTickets.Add(CurrentTicket);
            }
           
        }

        private void ResultClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DisplayPage(EPages.Menu);
        }
    }
}
