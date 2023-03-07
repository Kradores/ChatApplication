namespace Chat.API.Endpoints.Users.GetMany;

public class Response
{
    public List<User> Users { get; set; } = new List<User>();

    public class User
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
