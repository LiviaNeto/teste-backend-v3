using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Domain.DTOs;
using TheatricalPlayersRefactoringKata.Presentation.Controllers;
using TheatricalPlayersRefactoringKata.Presentation.Formatters;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IStatementGeneratorService, StatementService>()
                .AddSingleton<TextStatementFormatter>() 
                .AddSingleton<XmlStatementFormatter>()   
                .AddSingleton<StatementController>()     
                .BuildServiceProvider();
        }

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public void CreateInvoice(Invoice invoice)
        {
            if (invoice.Performances == null || invoice.Performances.Count == 0)
            {
                throw new ArgumentException("Invoice must have at least one performance.");
            }

            _invoiceRepository.Add(invoice);
        }

        public IEnumerable<InvoiceDTO> GetAllInvoices()
        {
            return _invoiceRepository.GetAll();
        }

        public InvoiceDTO GetInvoiceByCustomer(string customer)
        {
            var invoice = _invoiceRepository.GetByCustomer(customer);
            
            if (invoice == null)
            {
                throw new KeyNotFoundException($"No invoice found for customer: {customer}");
            }

            return invoice;
        }

        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice.Performances == null || invoice.Performances.Count == 0)
            {
                throw new ArgumentException("Invoice must have at least one performance.");
            }

            _invoiceRepository.Update(invoice);
        }

        public void DeleteInvoice(string customer)
        {
            _invoiceRepository.Delete(customer);
        }   

        public string GetTextStatementByCustomer(string customer)
        {     
            var textStatement = _invoiceRepository.GetTextStatementByCustomer(customer);            
            
            return textStatement;
        }     

        public string GetXmlStatementByCustomer(string customer)
        {     
            var xmlStatement = _invoiceRepository.GetXmlStatementByCustomer(customer);            
            
            return xmlStatement;
        } 
    }
}