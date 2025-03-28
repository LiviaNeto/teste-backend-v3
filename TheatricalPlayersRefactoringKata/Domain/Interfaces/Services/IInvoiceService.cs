using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Services
{
    public interface IInvoiceService
    {
        void CreateInvoice(Invoice invoice);    
        IEnumerable<InvoiceDTO> GetAllInvoices();
        InvoiceDTO GetInvoiceByCustomer(string customer);
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(string customer);
        string GetTextStatementByCustomer(string customer);
        string GetXmlStatementByCustomer(string customer);
    }
}