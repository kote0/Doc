using DomainModels.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                ViewBag.Message = "Введите название документа";
                return View();
            }
            if (file == null)
            {
                ViewBag.Message = "Документ не найден";
                return View();
            }

            byte[] binaryFile = new byte[file.ContentLength];
            file.InputStream.Read(binaryFile, 0, file.ContentLength);

            // создать док
            var doc = DocumentsRepository.Create();
            doc.Name = name + System.IO.Path.GetExtension(file.FileName);
            doc.Author = UserRepository.GetbyLogin(User.Identity.Name);
            doc.BinaryFile = binaryFile;

            DocumentsRepository.Update(doc);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(long Id)
        {
            var doc = DocumentsRepository.Get(Id);
            if (doc == null)
            {
                ViewBag.Message = "Документ не найден";
            }
            DocumentsRepository.Delete(doc);
            return RedirectToAction("Index");
        }
        public ActionResult Download(long Id)
        {
            var doc = DocumentsRepository.Get(Id);
            if (doc == null)
            {
                ViewBag.Message = "Документ не найден";
                return RedirectToAction("Index");

            }

            Response.BinaryWrite(doc.BinaryFile);
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + doc.Name);
            Response.End();


            return RedirectToAction("Index");
        }

    }
}