namespace MediaApp.Common.Models;

public class MessageBusPostEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}