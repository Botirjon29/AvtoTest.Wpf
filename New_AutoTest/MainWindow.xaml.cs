using DatabaseForNewAutoTest;
using New_AutoTest.Pages;
using System.Windows;
namespace New_AutoTest;

public partial class MainWindow : Window
{
    public QuestionRepository QuestionRepository;

    private static MainWindow? _instance;

    public static MainWindow Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MainWindow();
            }
            return _instance;
        }
    }
    public MainWindow()
    {
        _instance = this;
        InitializeComponent();
        QuestionRepository = new QuestionRepository();
        MainFrame.Navigate(new MenuPage());
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        QuestionRepository.WriteToJson();
    }
}

