using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteSerials.WebUI.Models
{
    public class SerialsListViewModel
    {
        public IEnumerable<Serial> Serials { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}