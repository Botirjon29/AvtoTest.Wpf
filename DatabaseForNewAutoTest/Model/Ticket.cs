namespace DatabaseForNewAutoTest.Model;

public class Ticket
{
    public int Index { get; set; }
    public List<QuestionEntity> Questions;
    public int QuestionsCount;
    public int CorrectAnswerCount;

    public bool TicketCompleted
    {
        get
        {
            return QuestionsCount == CorrectAnswerCount;
        }
    }

 
    public List<TicketData> SelectedQuestionIndexes;

    public bool IsQuestionCompleted(int questionIndex)
    {
        return SelectedQuestionIndexes.Any(td => td.QuestionIndex == questionIndex);
    }

    public bool IsChoiceCompleted(int questionIndex, int selectedChoiceIndex)
    {

        return SelectedQuestionIndexes.Any(td => td.QuestionIndex == questionIndex && td.SelectedChoiceIndex == selectedChoiceIndex);
    }

    public Ticket()
    {

    }
    public Ticket(int index, List<QuestionEntity> questions)
    {
        Index = index;
        Questions = questions;
        SelectedQuestionIndexes = new List<TicketData>();
        QuestionsCount = questions.Count;
        
    }

    public Ticket(int index, int correctAnswerCount, int questionsCount)
    {
        Index = index;
        CorrectAnswerCount = correctAnswerCount;
        QuestionsCount = questionsCount;
    }

   

}


/// <summary>
///  TicketData Class
/// </summary>
public class TicketData
{
    public int QuestionIndex { get; set; }
    public int SelectedChoiceIndex { get; set; }

    public TicketData(int questionIndex, int selectedChoiceIndex)
    {
        QuestionIndex = questionIndex;
        SelectedChoiceIndex = selectedChoiceIndex;
    }
}

