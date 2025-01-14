using blog_rpg.Models.Enums;

namespace blog_rpg.Models
{
    public class User
    {
        public User(int id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public int Id { get; init; }
        public string Name { get; set; }
        public UserRole Role { get; private set; }
    }
}
