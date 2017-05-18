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
        // GET: Favorite

        public ActionResult Index(string returnUrl)
        {
            ViewBag.p = returnUrl;
            using (EFDbContext db = new EFDbContext())
            {
                if (User.Identity.IsAuthenticated)
                {
                    Favorite f = new Favorite();
                    f.FavSerials= db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserSerials;
                    returnUrl = "yes";
                    return View(f);
                }
            }

            return View();
        }

        public RedirectToRouteResult AddToFavorite(int id, string returnUrl)
        {
            using (EFDbContext db = new EFDbContext())
            {
                if (User.Identity.IsAuthenticated)
                {
                    Serial serial = db.Serials.FirstOrDefault(g => g.Id == id);
                    db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserSerials.Add(serial);
                    db.SaveChanges();
                    returnUrl = "yes";
                    //returnUrl = User.Identity.Name;
                    //returnUrl = serial.Serial_title;
                    return RedirectToAction("Index", new { returnUrl });
                }
            }
            returnUrl = id.ToString();
            return RedirectToAction("Index", new { returnUrl });
            //return RedirectToAction("Index");
        }

        //public RedirectToRouteResult RemoveFromCart(int gameId, string returnUrl)
        //{
        //    Game game = repository.Games
        //        .FirstOrDefault(g => g.GameId == gameId);

        //    if (game != null)
        //    {
        //        GetCart().RemoveLine(game);
        //    }
        //    return RedirectToAction("Index", new { returnUrl });
        //}

        //public Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

    }
}