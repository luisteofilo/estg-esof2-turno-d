using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IInvoiceRepository
{
    Task CreateInvoice(Invoice invoice);

    // Get all Invoices
    Task<IEnumerable<Invoice>> GetAllInvoices();

    // Get an Invoice by ID
    Task<Invoice> GetInvoiceById(Guid id);
    
    // Update an Invoice
    Task UpdateInvoice(Invoice invoice);
    
    // Delete an Invoice
    Task<bool> DeleteInvoice(Invoice invoice);

}