using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Concrete;
using SiteSerials.Domain.Entities;
using SiteSerials.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserName == model.Name && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("List", "Serial");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserName == model.Name);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (EFDbContext db = new EFDbContext())
                    {
                        db.Users.Add(new User { UserName = model.Name, Password = model.Password });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.UserName == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("List", "Serial");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("List", "Serial");
        }
    }
}