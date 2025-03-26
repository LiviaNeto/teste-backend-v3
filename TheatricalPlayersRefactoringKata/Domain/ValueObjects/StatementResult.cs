using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.ValueObjects
{
    public class StatementResult
    {
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public int VolumeCredits { get; set; }
        public List<PerformanceDetail> Performances { get; set; }

        public class PerformanceDetail
        {
            public string PlayName { get; set; }
            public decimal Amount { get; set; }
            public int Audience { get; set; }
            public decimal Credits { get; set; }
        } 
    }
}