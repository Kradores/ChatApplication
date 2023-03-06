namespace BlazorChat.Client.Models.Chat;

public class ChatCreateInput
{
    public string Name { get; set; } = null!;
    public List<UserCheckbox> UserCheckboxes { get; set; } = new List<UserCheckbox>();

    public class UserCheckbox
    {
        public string Name { get; set; } = null!;
        public string Id { get; set; } = null!;
        public bool IsChecked { get; set; }
    }
}
