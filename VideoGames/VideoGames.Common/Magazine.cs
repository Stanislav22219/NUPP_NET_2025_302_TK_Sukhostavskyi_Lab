using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Common
{
    public class Magazine : Item
    {
        public string Publisher { get; set; }
        public int IssueNumber { get; set; }
        public double CriticsRating { get; set; }
    }
}
