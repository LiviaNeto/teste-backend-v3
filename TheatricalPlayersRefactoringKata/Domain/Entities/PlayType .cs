using System;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public abstract class PlayType 
    {
        public string Name { get; }

        protected PlayType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected static int NormalizeLines(int lines)
        {
            return Math.Clamp(lines, 1000, 4000);
        }

        public virtual int CalculateCredits(int audience)
        {
            return audience > 30 ? audience - 30 : 0;
        }

        public abstract decimal CalculateCharge(int lines, int audience);
    }
}