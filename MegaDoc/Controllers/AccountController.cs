using DomainModels.NHibernate;
using DomainModels.Repository;
using MegaDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MegaDoc.Controllers
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
                ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}