namespace Common.Dtos.Users;

public class TalentDto
{
    public Guid TalentId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string AreaOfInterest { get; set; }
    public Guid UserId { get; set; }
    
}