using DomainModels.NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModels.Extensions;

namespace MegaDoc1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDocumentsRepository DocumentsRepository { get; set; }
        private IUserRepository UserRepository { get; set; }
        public HomeController(IDocumentsRepository DocumentsRepository, IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
            this.DocumentsRepository = DocumentsRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            var user = UserRepository.GetbyLogin(User.Identity.Name);
            return View(DocumentsRepository.GetAll(user));
        }
        public ActionResult AddDocument()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDocument(string name, HttpPostedFileBase file)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Message = SR.T("Введите название документа");
                return View();
            }
            if (file == null)
            {
                ViewBag.Message = SR.T("Документ не найден");
                return View();
            }

            var currUser = UserRepository.GetbyLogin(User.Identity.Name);

            var path = Server.MapPath("~/Files/" + currUser.Login );
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += @"/" + file.FileName;
            file.SaveAs(path);
            

            // создать документ
            var doc = DocumentsRepository.Create();
            doc.Name = name + System.IO.Path.GetExtension(file.FileName);
            doc.Author = currUser;
            doc.Path = path;

            DocumentsRepository.Update(doc);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(long Id)
        {
            var doc = DocumentsRepository.Get(Id);
            if (doc == null)
            {
                ViewBag.Message = SR.T("Документ не найден");
            }
            DocumentsRepository.Delete(doc);
            return RedirectToAction("Index");
        }
        public ActionResult Download(long Id)
        {
            var doc = DocumentsRepository.Get(Id);
            if (doc == null)
            {
                ViewBag.Message = SR.T("Документ не найден");
                return RedirectToAction("Index");

            }

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + doc.Name);
            Response.WriteFile(doc.Path);
            Response.End();


            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string searchLine)
        {
            var currUser = UserRepository.GetbyLogin(User.Identity.Name);
            var model = DocumentsRepository.SearchDocument(searchLine, currUser);
            return View("Index", model);
        }
    }
}