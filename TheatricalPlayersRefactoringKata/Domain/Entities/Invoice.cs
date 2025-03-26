using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; 
                             set
                             {
                                  if (string.IsNullOrWhiteSpace(value))
                                   throw new ArgumentException("Customer cannot be null or empty.", nameof(Customer));

                                  _customer = value;
                              } 
                           }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        if (string.IsNullOrWhiteSpace(customer))
                throw new ArgumentException("Customer cannot be null or empty.", nameof(customer));

        this._customer = customer;
        this._performances = performance;
    }

}
