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

        /// <summary>
        /// Create a new invoice.
        /// </summary>
        /// <param name="invoiceDto">Object containing invoice data.</param>
        /// <returns>Returns the created Invoice.</returns>
        [HttpPost]
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

        /// <summary>
        /// Returns all invoices.
        /// </summary>
        /// <returns>List of invoices.</returns>
        [HttpGet]        
        public ActionResult<IEnumerable<InvoiceDTO>> GetAllInvoices()
        {
            return Ok(_invoiceService.GetAllInvoices());
        }

        /// <summary>
        /// Search for invoices by customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Returns the found customers.</returns>
        [HttpGet("{customer}")]
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

        /// <summary>
        /// Update a invoice by customer.
        /// </summary>
        /// <param name="customer">PlayID.</param>
        /// <param name="invoiceDto">Invoice data to update.</param>
        [HttpPut("{customer}")]
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

        /// <summary>
        /// Delete a invoice by customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        [HttpDelete("{customer}")]        
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

        /// <summary>
        /// Return the statement in text by customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Returns the statement text.</returns>
        [HttpGet("textStatement/{customer}")]
        public IActionResult GetTextStatementByCustomer(string customer)
        {
            try
            {
                var statement = _invoiceService.GetTextStatementByCustomer(customer);
                return Ok(statement);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Statement for customer {customer} not found.");
            }
        }

        /// <summary>
        /// Return the statement in XML by customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Returns the statement XML.</returns>
        [HttpGet("xmlStatement/{customer}")]
        public IActionResult GetXmlStatementByCustomer(string customer)
        {
            try
            {
                var statement = _invoiceService.GetXmlStatementByCustomer(customer);
                return Ok(statement);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Statement for customer {customer} not found.");
            }
        }
    }
}