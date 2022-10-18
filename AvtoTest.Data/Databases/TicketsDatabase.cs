 
using AvtoTest.Data.Models;
using AvtoTest.Data.Options;
 
namespace AvtoTest.Data.Databases;

public class TicketDatabase
{
    public List<TicketEntity> UserTickets { get; set; }

    public TicketDatabase()
    {
        UserTickets = new List<TicketEntity>();
    }

    public TicketEntity CreateTicket()
    {
        return new TicketEntity(CreateExamTicket());
    }

    public List<QuestionEntity> CreateExamTicket()
    {
        int randomNumber = new Random().Next(0, GetTicketsCount());
        var questions = Database.DB.QuestionsDb.CreateTicket(randomNumber * TicketsSettings.TicketQuestionsCount, TicketsSettings.TicketQuestionsCount);
        return new List<QuestionEntity>(questions);
    }

    public int GetTicketsCount()
    {
        return Database.DB.QuestionsDb.Questions.Count / TicketsSettings.TicketQuestionsCount;
    }
}