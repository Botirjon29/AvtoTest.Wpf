using DatabaseForNewAutoTest;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace New_AutoTest.Pages;

public partial class MenuPage : Page
{
    public MenuPage()
    {
        InitializeComponent();
        SetImage();
        ShowQuestionsAndTicketsResult();
    }

    private void SetImage()
    {
        ImageMenu.Source = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "Images", $"AvtoHome.jpg")));
    }

 
    private void ShowQuestionsAndTicketsResult()
    {
        var totalQuestionCount = MainWindow.Instance.QuestionRepository.Questions!.Count;
        var correctAnswerCount = MainWindow.Instance.QuestionRepository.UserTicketResult.Sum(t => t.CorrectAnswerCount);
        QuestionsBlock.Text = $"{correctAnswerCount}/{totalQuestionCount}";

        var totalTicketsResultCount = MainWindow.Instance.QuestionRepository.GetTicketsCount();
        var CompletedTicketsCount = MainWindow.Instance.QuestionRepository.UserTicketResult.Count(t => t.TicketCompleted);
        TicketsBlock.Text = $"{CompletedTicketsCount}/{totalTicketsResultCount}";
    }

    private void TicketsButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.MainFrame.Navigate(new TicketsPage());
    }

    private void ExaminationButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.MainFrame.Navigate(new ExaminationPage());
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.Close();
    }
}

