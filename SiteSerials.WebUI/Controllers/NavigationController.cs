using SiteSerials.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteSerials.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        private ISerialRepository repository;

        public NavigationController(ISerialRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Serials
                .Select(serials => serials.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}