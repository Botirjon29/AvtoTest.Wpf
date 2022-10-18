


 
namespace AvtoTest.Data.Models;

public class QuestionEntity
{
    public long Id { get; set; }
    public string Question { get; set; }
    public List<Choice> Choices { get; set; }
    public Media Media { get; set; }

    public bool IsCompleted { get; set; }
}

