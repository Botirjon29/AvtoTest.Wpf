 
 
 
namespace AvtoTest.Data.Models
{
    public class UserEntity
    {
        public string Name { get; set; }

        public UserEntity(long chatId, string name)
        {
            Name = name;
        }
         
    }                                                                                          
}
