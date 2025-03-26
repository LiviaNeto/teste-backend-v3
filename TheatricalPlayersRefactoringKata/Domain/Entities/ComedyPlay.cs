using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class ComedyPlay : PlayType
    {
        public ComedyPlay() : base("comedy") { }

        public override decimal CalculateCharge(int lines, int audience)
        {
            decimal baseValue = NormalizeLines(lines) / 10m;
                        
            decimal charge = baseValue + (3m * audience);            

            if (audience > 20)
            {
                charge += 100m + (5m * (audience - 20));
            }

            return charge;
        }

        public override int CalculateCredits(int audience)
        {
            int baseCredits = base.CalculateCredits(audience);
            int bonusCredits = (int)Math.Floor(audience / 5.0);
            return baseCredits + bonusCredits;
        }       
    }
}