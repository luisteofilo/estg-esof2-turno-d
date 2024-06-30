using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Invoice;
using Common.Dtos.Position;
using Common.Dtos.Timesheet;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.Services;
using ESOF.WebApp.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceResponseDTO>>> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceResponseDTO>> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<ActionResult> CreateInvoice(Guid timesheetId, [FromBody] InvoiceCreateDTO dto)
        {
            await _invoiceService.CreateInvoice(timesheetId, dto);
            return Ok();
            // return CreatedAtAction(nameof(GetPositionById), new { id = createdPosition.PositionId }, createdPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(Guid id, Guid timesheetId,InvoiceUpdateDTO dto)
        {
            await _invoiceService.UpdateInvoice(id, timesheetId, dto);
            return Ok(); 
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var result = await _invoiceService.DeleteInvoice(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
