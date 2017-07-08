using DomainModels.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaDoc.Controllers
{
    public class HomeController : Controller
    {
        private IDocumentsRepository FileRepository { get; set; }
        public HomeController(IDocumentsRepository FileRepository)
        {
            this.FileRepository = FileRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View(FileRepository.GetAll());
        }
    }
}