using DomainModels.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaDoc1.Controllers
{
    public class HomeController : Controller
    {
        private IFileRepository FileRepository { get; set; }
        public HomeController(IFileRepository FileRepository)
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