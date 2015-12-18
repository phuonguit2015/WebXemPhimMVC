using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.DAL;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class FileImageController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        // GET: FileImage
        public ActionResult Index()
        {
            return View();
        }

       
    }
}