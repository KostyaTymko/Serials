using SiteSerials.Domain.Abstract;
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

        public ViewResult Create()
        {
            return View("EditSerial", new Serial());
        }

        public ActionResult EditSerial(int id)
        {
            ViewBag.SerialId = id;
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
                //if (string.IsNullOrEmpty(serial.Serial_title))
                //{
                //    ModelState.AddModelError("Name", "Некорректное название книги");
                //}
                if (image != null)
                {
                    serial.ImageMimeType = image.ContentType;
                    serial.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(serial.ImageData, 0, image.ContentLength);
                }
                //return RedirectToAction("Index");
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