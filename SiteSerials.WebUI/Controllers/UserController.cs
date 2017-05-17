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
    }
}