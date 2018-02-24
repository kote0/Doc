using DomainModels.NHibernate;
using DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DomainModels.Extensions;

namespace MegaDoc1.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository UserRepository { get; set; }
        public AccountController(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Login, string Password)
        {
            if (UserRepository.IsValid(Login, Password))
            {
                FormsAuthentication.SetAuthCookie(Login, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", SR.T("Пользователя с таким логином и паролем не существует"));
            }
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangeLocation(int? idLocation)
        {
            if (idLocation.HasValue)
            {
                SR.ChangeLocation(idLocation.Value);
            }
            return RedirectToAction("Login");
        }
    }
}