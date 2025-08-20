namespace SimpleAPIApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Roles { get; set; }

        public User(int id, string name, string email, string roles)
        {
            Id = id;
            Name = name;
            Email = email;
            Roles = roles;
        }
    }
}
