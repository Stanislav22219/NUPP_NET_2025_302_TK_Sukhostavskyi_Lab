using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGames.Common
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }

        public string GetDescription()
        {
            return $"{Title} developed by {Developer}";
        }
    }
}
