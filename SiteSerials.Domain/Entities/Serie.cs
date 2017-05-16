using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Entities
{    public class Serie
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public string Title { get; set; }

        public virtual Season Season { get; set; }
    }
}
