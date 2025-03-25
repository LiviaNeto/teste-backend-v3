using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class HistoricalPlay : PlayType
    {
        public HistoricalPlay() : base("historical") { }

        public override decimal CalculateCharge(int lines, int audience)
        {
            var tragedyCharge = new TragedyPlay().CalculateCharge(lines, audience);
            var comedyCharge = new ComedyPlay().CalculateCharge(lines, audience);
            return tragedyCharge + comedyCharge;
        }    
    }
}