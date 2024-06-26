using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos.Invoice;
using Common.Dtos.Position;
using Common.Dtos.Timesheet;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.Services
{
    public class InvoiceService
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly InvoiceDTOConverter _dtoConverter;

        public InvoiceService(ITimesheetRepository timesheetRepository, IInvoiceRepository invoiceRepository, InvoiceDTOConverter dtoConverter)
        {
            _timesheetRepository = timesheetRepository;
            _invoiceRepository = invoiceRepository;
            _dtoConverter = dtoConverter;
        }

        public async Task CreateInvoice(Guid timesheetId, InvoiceCreateDTO dto)
        {
            var timesheet = await _timesheetRepository.GetTimesheetById(timesheetId);
            if (timesheet == null)
            {
                throw new Exception("There is no timesheet with this id.");
            }

            var invoice = _dtoConverter.InvoiceCreateDTOToInvoice(dto, timesheetId);
            invoice.Description = "Task Description: " + timesheet.TaskDescription + " / Hours Worked: " +
                                  timesheet.HoursWorked;
            await _invoiceRepository.CreateInvoice(invoice);
        }

        
        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            return await _invoiceRepository.GetAllInvoices();
        }

        public async Task<Invoice> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceRepository.GetInvoiceById(id);
            if (invoice == null)
            {
                throw new Exception("There is no invoice with this Id.");
            }

            return invoice;
        }

        public async Task UpdateInvoice(Guid id, Guid timesheetId, InvoiceUpdateDTO dto)
        {
            var timesheet = await _timesheetRepository.GetTimesheetById(timesheetId);
            if (timesheet == null)
            {
                throw new Exception("There is no timesheet with this id.");
            }
            
            var invoice = await _invoiceRepository.GetInvoiceById(id);
            if (invoice == null)
            {
                throw new Exception("There is no invoice with this Id.");
            }

            invoice.Date = dto.Date;
            invoice.Payment = dto.Payment;
            invoice.Description = "Task Description: " + timesheet.TaskDescription + " / Hours Worked: " +
                                  timesheet.HoursWorked;
            await _invoiceRepository.UpdateInvoice(invoice);
        }

        public async Task<bool> DeleteInvoice(Guid id)
        {
            var invoice = await _invoiceRepository.GetInvoiceById(id);
            if (invoice == null)
            {
                throw new Exception("There is no invoice with this Id.");
            }

            await _invoiceRepository.DeleteInvoice(invoice);
            return true;
        }
    }
}