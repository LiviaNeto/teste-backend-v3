using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // Métodos anteriores permanecem os mesmos, apenas substituindo o tipo de _invoiceService
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateInvoice([FromBody] Invoice invoice)
        {
            try
            {
                _invoiceService.CreateInvoice(invoice);
                return CreatedAtAction(
                    nameof(GetInvoiceByCustomer), 
                    new { customer = invoice.Customer }, 
                    invoice
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<InvoiceDTO>> GetAllInvoices()
        {
            return Ok(_invoiceService.GetAllInvoices());
        }

        [HttpGet("{customer}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InvoiceDTO> GetInvoiceByCustomer(string customer)
        {
            try
            {
                return Ok(_invoiceService.GetInvoiceByCustomer(customer));
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Invoice for customer {customer} not found.");
            }
        }

        [HttpPut("{customer}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateInvoice(string customer, [FromBody] Invoice invoice)
        {
            if (customer != invoice.Customer)
            {
                return BadRequest("Customer in URL does not match invoice customer.");
            }

            try
            {
                _invoiceService.UpdateInvoice(invoice);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Invoice for customer {customer} not found.");
            }
        }

        [HttpDelete("{customer}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteInvoice(string customer)
        {
            try
            {
                _invoiceService.DeleteInvoice(customer);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Invoice for customer {customer} not found.");
            }
        }
    }
}