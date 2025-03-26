using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class TragedyPlay : PlayType
    {
        public TragedyPlay() : base("tragedy") { }

        public override decimal CalculateCharge(int lines, int audience)
        {
            decimal baseValue = NormalizeLines(lines) / 10m;
            
            if (audience <= 30)
            {
                return baseValue;
            }
            
            return baseValue + (10m * (audience - 30));

        }
    }
}