using SiteSerials.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteSerials.WebUI.Controllers
{ 
    public class SerialController : Controller
    {
        private ISerialRepository repository;
        public SerialController(ISerialRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Serials);
        }

        public ViewResult SeasonList(int id)
        {
            return View(repository.Serials.Where(r=>r.Id==id));
        }

        public ViewResult SerieList(int id)
        {
            return View();
        }
    }
}