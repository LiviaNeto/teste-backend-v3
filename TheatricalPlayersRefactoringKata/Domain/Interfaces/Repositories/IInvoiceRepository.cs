using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        IEnumerable<InvoiceDTO> GetAll();
        InvoiceDTO GetByCustomer(string customer);
        Invoice GetInvoiceByCustomer(string customer);
        void Delete(string customer);
        void Update(Invoice invoice);     
    }
}