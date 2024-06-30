namespace Common.Dtos.Users;

public class ClientDto
{
    public Guid ClientId { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string CompanyDescription { get; set; }
    public Guid UserId { get; set; }
}