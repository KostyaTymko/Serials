using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public int SerialId { get; set; }
        public string Season_title { get; set; }
        public virtual Serial Serial { get; set; }
        public virtual ICollection<Serie> Series { get; set; }

        public Season()
        {
            this.Series = new HashSet<Serie>();
        }
    }
}
