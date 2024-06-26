namespace Common.Dtos.Message;

public class MessageDto
{
    public string message { get; set; }
    public string sender { get; set; }
    public string data { get; set; }

    public MessageDto(string sender, string message)
    {
        this.sender = sender;
        this.message = message;
        this.data = DateTime.Now.ToString("dd-MM-yyyy");
    }
}