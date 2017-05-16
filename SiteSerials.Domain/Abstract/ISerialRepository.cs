using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Abstract
{
    public interface ISerialRepository
    {
        IEnumerable<Serial> Serials { get; }
    }
}
