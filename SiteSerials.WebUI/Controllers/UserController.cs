using SiteSerials.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteSerials.WebUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private IUserRepository repository;
        public UserController(IUserRepository UserRepo)
        {
            repository = UserRepo;
        }
        public ActionResult Index()
        {
            return View(repository.Users);
        }

        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // поиск пользователя в бд
        //        User user = null;
        //        using (UserContext db = new UserContext())
        //        {
        //            user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);

        //        }
        //        if (user != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.Name, true);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
        //        }
        //    }

        //    return View(model);
        //}

        //public ActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = null;
        //        using (UserContext db = new UserContext())
        //        {
        //            user = db.Users.FirstOrDefault(u => u.Email == model.Name);
        //        }
        //        if (user == null)
        //        {
        //            // создаем нового пользователя
        //            using (UserContext db = new UserContext())
        //            {
        //                db.Users.Add(new User { Email = model.Name, Password = model.Password, Age = model.Age });
        //                db.SaveChanges();

        //                user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
        //            }
        //            // если пользователь удачно добавлен в бд
        //            if (user != null)
        //            {
        //                FormsAuthentication.SetAuthCookie(model.Name, true);
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Пользователь с таким логином уже существует");
        //        }
        //    }

        //    return View(model);
        //}
        //public ActionResult Logoff()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}