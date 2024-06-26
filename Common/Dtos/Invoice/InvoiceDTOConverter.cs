namespace Common.Dtos.Invoice;

public class InvoiceDTOConverter
{
    public ESOF.WebApp.DBLayer.Entities.Invoice InvoiceCreateDTOToInvoice(InvoiceCreateDTO dto, Guid timesheetId)
    {
        
        return new ESOF.WebApp.DBLayer.Entities.Invoice
        {
            TimesheetId = timesheetId, 
            Date = dto.Date,
            Payment = dto.Payment
        };
    }
}