namespace AvtoTest.Data.Models
{
    public class TicketEntity
    {
         

        public int Index { get; set; }
        public string Text { get; set; }
        public int QuestionsCount { get; set; }
        public int CorrectAnswersCount { get; set; }
        public List<QuestionEntity> Questions { get; set; }

        public int CurrentQuestion
        {
            get { return QuestionsCount - Questions.Count; }
        }

        public bool IsCompleted
        {
            get
            {
                return QuestionsCount == CorrectAnswersCount;
            }
        }

        public TicketEntity(List<QuestionEntity> questions)
        {
            QuestionsCount = questions.Count;
            CorrectAnswersCount = 0;
            Questions = questions;
        }
        public TicketEntity(int index, List<QuestionEntity> questions)
        {
            QuestionsCount = questions.Count;
            CorrectAnswersCount = 0;
            Questions = questions;
            Index = index;
        }
        public TicketEntity(int index, string text)
        {
            Index = index;
            Text = text;
        }
    }
}
