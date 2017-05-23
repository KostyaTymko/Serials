using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Concrete;
using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteSerials.WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        ISerialRepository repository;

        public AdminController(ISerialRepository repo)
        {
            repository = repo;
        }

        [Authorize(Users = "Admin")]
        public ActionResult Index()
        {
            return View(repository.Serials);
        }
        //---------------------------------------------------------------------------------------
        public ActionResult ViewSerie(int id)
        {
            int i = 0;
            ViewBag.SerieId = id;
            Season season = repository.Seasons.FirstOrDefault(g => g.Id == id);
            i = season.Id;
            ViewData["ID"] = i;
            return View(season);
        }

        public ActionResult EditSerie(int id)
        {
            //Serie serie =  repository.Series.FirstOrDefault(q => q.Id == id);
            return View(repository.Series.FirstOrDefault(q => q.Id == id));
        }
        [HttpPost]
        public ActionResult EditSerie(Serie serie)
        {
            if (ModelState.IsValid)
            {
                repository.SaveSerie(serie);
                TempData["message"] = string.Format("Изменения в сезоне \"{0}\" были сохранены", serie.Title);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(serie);
            }
        }
        //public ViewResult CreateSerie(int id)
        //{
        //    Serie serie = new Serie();
        //    serie.SeasonId= id;
        //    return View(serie);
        //}

        public ActionResult CreateSerie(int? id)
        {
            
            if (id == null)
                return HttpNotFound();
            SelectList series = new SelectList(repository.Seasons.Where(d => d.Id == id), "Id", "Season_title");
            ViewBag.Series = series;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSerie(Serie serie)
        {
            EFDbContext db = new EFDbContext();
            if (User.Identity.IsAuthenticated)
            {
                using (db)
                {
                    repository.CreateSerie(serie);
                    db.SaveChanges();
                }
                TempData["message"] = string.Format("Изменения в сезоне \"{0}\" были сохранены", serie.Title);
                return RedirectToAction("Index");
            }
            TempData["message"] = string.Format("Изменения НЕ были сохранены", serie.Title);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSerie(int id)
        {
            EFDbContext db = new EFDbContext();
            using (db)
            {

                Serie s = db.Series.FirstOrDefault(g => g.Id == id);
                db.Series.Remove(s);
                db.SaveChanges();
                TempData["message"] = string.Format("Сезон \"{0}\" был удален",
                    s.Title);
                return RedirectToAction("Index");
            }
        }
        //---------------------------------------------------------------------------------------
        //[HttpPost]
        public ActionResult DeleteSeason(int id)
        {
            EFDbContext db = new EFDbContext();
            using (db)
            {

                Season s = db.Seasons.FirstOrDefault(g => g.Id == id);
                db.Seasons.Remove(s);
                db.SaveChanges();
                    TempData["message"] = string.Format("Сезон \"{0}\" был удален",
                        s.Season_title);
                return RedirectToAction("Index");
            }
        }
        public ViewResult CreateSeason(int id)
        {
            Season season = new Season();
            season.SerialId = id;
            return View(season);
        }
        [HttpPost]
        public ActionResult CreateSeason(Season season)
        {
                if (User.Identity.IsAuthenticated)
                {
                    repository.CreateSeason(season);
                    TempData["message"] = string.Format("Изменения в сезоне \"{0}\" были сохранены", season.Season_title);
                    return RedirectToAction("Index");
                }
            TempData["message"] = string.Format("Изменения НЕ были сохранены", season.Season_title);
            return RedirectToAction("Index");
        }
        public ActionResult EditSeason(int id)
        {
            Season season=null; 
            IEnumerable<Serial> serials = repository.Serials;
            foreach(var r in serials)
            {
               foreach(var b in r.Seasons)
                {
                    if(b.Id == id)
                    {
                        season = b;
                    }
                }
            }
            Serial serial = serials.FirstOrDefault(q => q.Id == id);
            return View(season);
        }

        [HttpPost]
        public ActionResult EditSeason(Season season)
        {
            if (ModelState.IsValid)
            {
                repository.SaveSeason(season);
                TempData["message"] = string.Format("Изменения в сезоне \"{0}\" были сохранены", season.Season_title);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(season);
            }
        }
        public ActionResult ViewSeason(int id)
        {
            int i = 0;
            ViewBag.SerialId = id;
            Serial serial = repository.Serials
                .FirstOrDefault(g => g.Id == id);
            i = serial.Id;
            ViewData["ID"] = i;
            return View(serial);
        }
        //----------------------------------------------------------------------------------------
        public ViewResult Create()
        {
            return View("EditSerial", new Serial());
        }

        public ActionResult EditSerial(int id)
        {
            Serial serial = repository.Serials
                .FirstOrDefault(g => g.Id == id);
            return View(serial);
        }
        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult EditSerial(Serial serial, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    serial.ImageMimeType = image.ContentType;
                    serial.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(serial.ImageData, 0, image.ContentLength);
                }
                repository.SaveSerial(serial);
                TempData["message"] = string.Format("Изменения в сериале \"{0}\" были сохранены", serial.Serial_title);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(serial);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Serial deletedSerial = repository.DeleteSerial(id);
            if (deletedSerial != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена",
                    deletedSerial.Serial_title);
            }
            return RedirectToAction("Index");
        }
    }
}