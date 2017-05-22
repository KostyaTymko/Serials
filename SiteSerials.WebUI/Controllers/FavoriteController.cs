using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Concrete;
using SiteSerials.Domain.Entities;
using SiteSerials.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteSerials.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        EFDbContext db = new EFDbContext();
        // GET: Favorite
        public PartialViewResult Summary()
        {
            Favorite f = new Favorite();
            using (db)
            {
                if (User.Identity.IsAuthenticated)
                {
                    f.FavSerials = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserSerials;
                    return PartialView(f);
                }
            }
            f.FavSerials = null;
            return PartialView(f);
        }
        public ActionResult Index(string returnUrl)
        {
            ViewBag.p = returnUrl;
            Favorite f = new Favorite();
            using (db)
            {
                if (User.Identity.IsAuthenticated)
                {
                    f.FavSerials= db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserSerials;
                    returnUrl = "yes";
                    return View(f);
                }
            }
            f.FavSerials = null;
            return View(f);
        }

        public RedirectToRouteResult AddToFavorite(int id)
        {
            string returnUrl="no";
            using (db)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Serial serial = db.Serials.FirstOrDefault(g => g.Id == id);
                    db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserSerials.Add(serial);
                    db.SaveChanges();
                    returnUrl = "yes";
                    return RedirectToAction("Index", new { returnUrl });
                }
            }
            returnUrl = id.ToString();
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromFavorite(int id)
        {
            using (db)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Serial serial = db.Serials.FirstOrDefault(g => g.Id == id);
                    db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserSerials.Remove(serial);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}