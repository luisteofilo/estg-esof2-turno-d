namespace Common.Dtos.Invoice;

public class InvoiceResponseDTO
{
    public DateTime Date { get; set; }
    public int Payment { get; set; }
    public string Description { get; set; }
}