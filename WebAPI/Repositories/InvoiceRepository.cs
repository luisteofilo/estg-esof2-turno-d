using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    // Create a new Invoice
    public async Task CreateInvoice(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
    }

    // Get all invoices
    public async Task<IEnumerable<Invoice>> GetAllInvoices()
    {
        return await _context.Invoices.Include(i => i.Timesheet).ToListAsync();
    }

    // Get an invoice by ID
    public async Task<Invoice> GetInvoiceById(Guid id)
    {
        return await _context.Invoices.FindAsync(id);
    }

    // Update an invoice
    public async Task UpdateInvoice(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }

    // Delete an invoice
    public async Task<bool> DeleteInvoice(Invoice invoice)
    {
        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return true;
    }
}