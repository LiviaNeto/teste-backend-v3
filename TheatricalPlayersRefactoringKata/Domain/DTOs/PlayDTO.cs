using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.DTOs
{
    public class PlayDTO
    {
        public string Name { get; set; }
        public int Lines { get; set; }
        public string PlayTypeName { get; set; }    
    }
}