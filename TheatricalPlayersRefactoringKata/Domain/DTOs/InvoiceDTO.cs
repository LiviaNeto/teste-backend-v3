using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.DTOs
{
    public class InvoiceDTO
    {
        public string Customer { get; set; }
        public List<PerformanceDTO> Performances { get; set; }    
    }
}