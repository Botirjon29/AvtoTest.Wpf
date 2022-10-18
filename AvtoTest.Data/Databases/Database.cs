
using AvtoTest.Data.Models;
using AvtoTest.Data.Databases;
using Newtonsoft.Json;
 

public class Database
{
    private const string JsonDataPath = "C:\\Users\\Admin\\OneDrive\\Рабочий стол\\Projects\\AvtoTest.Wpf\\AvtoTest.Wpf\\JsonData\\uzlotin.json";

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

    private List<QuestionEntity> ReadQuestionsJson()
    {
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

