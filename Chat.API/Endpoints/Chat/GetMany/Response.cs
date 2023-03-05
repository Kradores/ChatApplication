namespace Chat.API.Endpoints.Chat.GetMany;

public class Response
{
    public List<Room> Rooms { get; set; } = new List<Room>();

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
