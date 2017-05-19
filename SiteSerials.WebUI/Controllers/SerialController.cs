using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Entities;
using SiteSerials.WebUI.Models;
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
        public int pageSize = 4;

        public SerialController(ISerialRepository repo)
        {
            repository = repo;
        }


        public ViewResult List(string category, int page = 1)
        {
            SerialsListViewModel model = new SerialsListViewModel
            {
                Serials = repository.Serials
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(serial => serial.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                repository.Serials.Count() :
                repository.Serials.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Здравствуйте, " + User.Identity.Name;
            }
            ViewBag.Authenticated = result;
            return View(model);
        }

        public ViewResult SeasonList(int id)
        {
            return View(repository.Serials.Where(r=>r.Id==id));
        }

        public ViewResult Search(string Search)
        {
            SerialsListViewModel model = new SerialsListViewModel
            {
                Serials = repository.Serials.Where(p => p.Serial_title.Contains(Search)),
                PagingInfo = null,
                CurrentCategory = null
            };

            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            Serial serial = repository.Serials
                .FirstOrDefault(g => g.Id == id);

            if (serial != null)
            {
                return File(serial.ImageData, serial.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}