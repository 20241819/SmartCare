public class MessageThread
{
    public int Id { get; set; }
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual List<Message> Messages { get; set; }
}
