using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Concrete
{
    public class EFSerialRepository : ISerialRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Serial> Serials
        {
            get { return context.Serials; }
        }
    }
}
