using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.DTOs;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class InMemoryInvoiceRepository : IInvoiceRepository
    {
        // Dicionário para armazenar as invoices em memória
        private readonly Dictionary<string, Invoice> _invoices = new Dictionary<string, Invoice>();

        // Adicionar uma nova invoice
        public void Add(Invoice invoice)
        {
            if (_invoices.ContainsKey(invoice.Customer))
            {
                throw new Exception($"An invoice for customer '{invoice.Customer}' already exists.");
            }
            _invoices[invoice.Customer] = invoice;
            Console.WriteLine($"Invoice added for customer: {_invoices}");
        }

        // Obter todas as invoices
        public IEnumerable<InvoiceDTO> GetAll()
        {
            return _invoices.Values.Select(invoice => new InvoiceDTO
            {
                Customer = invoice.Customer,
                Performances = invoice.Performances.Select(p => new PerformanceDTO
                {
                    PlayId = p.PlayId,
                    Audience = p.Audience
                }).ToList()
            });
        }

        // Obter invoice por nome do cliente
        public InvoiceDTO GetByCustomer(string customer)
        {
            var invoice = _invoices.Values.FirstOrDefault(
                i => i.Customer.Equals(customer, StringComparison.OrdinalIgnoreCase)
            );

            if (invoice == null)
            {
                return null;
            }

            return new InvoiceDTO
            {
                Customer = invoice.Customer,
                Performances = invoice.Performances.Select(p => new PerformanceDTO
                {
                    PlayId = p.PlayId,
                    Audience = p.Audience
                }).ToList()
            };
        }

        // Obter invoice completa por nome do cliente
        public Invoice GetInvoiceByCustomer(string customer)
        {
            return _invoices.Values.FirstOrDefault(
                i => i.Customer.Equals(customer, StringComparison.OrdinalIgnoreCase)
            );
        }

        // Deletar uma invoice
        public void Delete(string customer)
        {
            if (!_invoices.Remove(customer))
            {
                throw new KeyNotFoundException($"Invoice for customer '{customer}' not found.");
            }
        }

        // Atualizar uma invoice
        public void Update(Invoice invoice)
        {
            var oldEntry = _invoices.FirstOrDefault(
                x => x.Value.Customer.Equals(invoice.Customer, StringComparison.OrdinalIgnoreCase)
            );

            if (oldEntry.Key != null)
            {
                _invoices.Remove(oldEntry.Key);
            }
            
            _invoices[invoice.Customer] = invoice;
        }
    }     
}