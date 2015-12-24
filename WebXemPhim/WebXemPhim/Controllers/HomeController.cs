using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using WebXemPhim.DAL;
using WebXemPhim.Models;
using System.Linq;

namespace WebXemPhim.Controllers
{
    public class HomeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Movies(string tieude, TrangThaiPhim trangthai)
        {
            ViewBag.Message = tieude;
            var phims = db.Phims.Include(p => p.LoaiPhim);
            // Bad idea
            if (trangthai != TrangThaiPhim.Da_Chieu)
            {
                phims = phims.Where(s => s.TrangThai == trangthai);
            }
            return View(phims.ToList());
        }


        // Hien thi modal popup show video
        public PartialViewResult ModalPartial()
        {
            return PartialView();
        } 


        public ActionResult Sale()
        {
            ViewBag.Message = "Your sale page.";

            return View();
        }
        public ActionResult Showtimes()
        {
            ViewBag.Message = "Your Showtimes page.";

            return View();
        }
        public ActionResult TicketPrices()
        {
            ViewBag.Message = "Your TicketPrices page.";

            return View();
        }
    }
}