
using AvtoTest.Data.Databases;
using AvtoTest.Data.Models;
using Newtonsoft.Json;


public class Database
{


    private static Database _database;
    public static Database DB
    {
        get
        {
            if (_database == null)
            {
                _database = new Database();
            }
            return _database;
        }
    }

    public Database()
    {
        QuestionsDb = new QuestionsDatabase(ReadQuestionsJson());
        TicketDb = new TicketDatabase();
         
    }
    public QuestionsDatabase QuestionsDb;
    public TicketDatabase TicketDb;
    public UsersDatabase UsersDb;

    private List<QuestionEntity> ReadQuestionsJson()
    {
        var JsonDataPath = Path.Combine(Environment.CurrentDirectory, "JsonData", "uzlotin.json");
        if (!File.Exists(JsonDataPath)) return new List<QuestionEntity>();
        var json = File.ReadAllText(JsonDataPath);

        try
        {
            return JsonConvert.DeserializeObject<List<QuestionEntity>>(json)!;
        }
        catch
        {
            Console.WriteLine("Cannot Deserialize json");
            return new List<QuestionEntity>();

        }
    }
}

