using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Genre_title { get; set; }

        public virtual ICollection<Serial> Serials { get; set; }

        public Genre()
        {
            this.Serials = new HashSet<Serial>();
        }
    }
}
