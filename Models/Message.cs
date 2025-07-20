public class Message
{
    public int Id { get; set; }
    public int ThreadId { get; set; }
    public string SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }

    public virtual MessageThread Thread { get; set; }
}
