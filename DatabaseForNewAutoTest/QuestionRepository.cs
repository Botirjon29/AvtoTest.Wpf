using DatabaseForNewAutoTest.Model;
using Newtonsoft.Json;

namespace DatabaseForNewAutoTest;

public class QuestionRepository
{
	public List<QuestionEntity>? Questions = new List<QuestionEntity>();


	public List<Ticket> UserTicketResult = new List<Ticket>();
    public QuestionRepository()
    {
        ReadAllTextFromJsonFile();
        ReadAllTextJsonData();

    }

	private const string Folder = "UserData";

	private const string FileName = "usertickets.json";

    public void WriteToJson()
	{
		List<Ticket> ticketData = UserTicketResult
			.Select(t => new Ticket(t.Index, t.CorrectAnswerCount, t.QuestionsCount))
			.ToList();
		
		var jsonData = JsonConvert.SerializeObject(ticketData);
		if (!Directory.Exists(Folder))
		{
			Directory.CreateDirectory(Folder);
		}

		File.WriteAllText(Path.Combine(Folder,FileName), jsonData);
	}

	public void ReadAllTextJsonData()
	{
		if (!File.Exists(Path.Combine(Folder, FileName))) return;

			var jsonData = File.ReadAllText("UserData/usertickets.json");
			UserTicketResult = JsonConvert.DeserializeObject<List<Ticket>>(jsonData)!;
	}


	

	private void ReadAllTextFromJsonFile()
	{
			var jsonData = File.ReadAllText("JsonData/uzlotin.json");
			Questions = JsonConvert.DeserializeObject<List<QuestionEntity>>(jsonData);
		
	}

	public int TicketQuestionsCount = 5;

	public int GetTicketsCount()
	{
		return Questions!.Count / TicketQuestionsCount;
	}

	public List<QuestionEntity> GetQuestionsRange(int from, int count)
	{
		return Questions!.Skip(from).Take(count).ToList();
	}
}

