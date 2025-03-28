using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    [JsonPropertyName("customer")]
    public string Customer 
    { 
        get => _customer;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Customer cannot be null or empty.", nameof(Customer));
            _customer = value;
        }
    }

    [JsonPropertyName("performances")]
    public List<Performance> Performances 
    { 
        get => _performances; 
        set => _performances = value ?? new List<Performance>(); 
    }

    public Invoice(string customer, List<Performance> performances)
    {
        if (string.IsNullOrWhiteSpace(customer))
            throw new ArgumentException("Customer cannot be null or empty.", nameof(customer));
        
        _customer = customer;
        _performances = performances ?? new List<Performance>();
    }

    public Invoice()
    {
        _customer = string.Empty;
        _performances = new List<Performance>();
    }
}
