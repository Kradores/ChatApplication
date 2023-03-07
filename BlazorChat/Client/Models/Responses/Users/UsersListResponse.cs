namespace BlazorChat.Client.Models.Responses.Users;

public class UsersListResponse
{
    public List<User> Users { get; set; } = new();

    public class User
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
