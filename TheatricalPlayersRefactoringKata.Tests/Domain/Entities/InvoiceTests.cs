using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Tests.Domain.Entities
{
    public class InvoiceTests
    {
        [Fact]
        public void Constructor_ShouldInitializeInvoiceCorrectly()
        {
            // Arrange
            string expectedCustomer = "BigCo";
            var expectedPerformances = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            };

            // Act
            var invoice = new Invoice(expectedCustomer, expectedPerformances);

            // Assert
            Assert.Equal(expectedCustomer, invoice.Customer);
            Assert.Equal(expectedPerformances.Count, invoice.Performances.Count);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenCustomerIsNullOrEmpty()
        {
            // Arrange
            var performanceList = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35)
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Invoice("", performanceList));
            Assert.Equal("Customer cannot be null or empty. (Parameter 'customer')", exception.Message);
            
            exception = Assert.Throws<ArgumentException>(() => new Invoice(null, performanceList));
            Assert.Equal("Customer cannot be null or empty. (Parameter 'customer')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldAllowValidCustomerAndPerformances()
        {
            // Arrange
            string customer = "BigCo";
            var performances = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35)
            };

            // Act
            var invoice = new Invoice(customer, performances);

            // Assert
            Assert.Equal(customer, invoice.Customer);
            Assert.Equal(performances.Count, invoice.Performances.Count);
        }

        [Fact]
        public void SetCustomer_ShouldThrowArgumentException_WhenCustomerIsNullOrEmpty()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>());

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => invoice.Customer = "");
            Assert.Equal("Customer cannot be null or empty. (Parameter 'Customer')", exception.Message);

            exception = Assert.Throws<ArgumentException>(() => invoice.Customer = null);
            Assert.Equal("Customer cannot be null or empty. (Parameter 'Customer')", exception.Message);
        }

        [Fact]
        public void SetCustomer_ShouldAllowValidCustomer()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>());

            // Act
            invoice.Customer = "NewCustomer";

            // Assert
            Assert.Equal("NewCustomer", invoice.Customer);
        }

        [Fact]
        public void Performances_ShouldAllowSetAndGet()
        {
            // Arrange
            var initialPerformances = new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35)
            };
            var invoice = new Invoice("BigCo", initialPerformances);

            // Act
            var newPerformances = new List<Performance>
            {
                new Performance("othello", 40),
                new Performance("henry-v", 20)
            };
            invoice.Performances = newPerformances;

            // Assert
            Assert.Equal(newPerformances.Count, invoice.Performances.Count);
            Assert.Contains(invoice.Performances, p => p.PlayId == "othello");
        }
    }
}