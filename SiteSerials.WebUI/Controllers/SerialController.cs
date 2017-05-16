using SiteSerials.Domain.Abstract;
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

        //public ViewResult List(int page = 1)
        //{
        //    SerialsListViewModel model = new SerialsListViewModel
        //    {
        //        Serials = repository.Serials
        //            .OrderBy(serial => serial.Id)
        //            .Skip((page - 1) * pageSize)
        //            .Take(pageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = pageSize,
        //            TotalItems = repository.Serials.Count()
        //        }
        //    };
        //    return View(model);
        //}

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
            return View(model);
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