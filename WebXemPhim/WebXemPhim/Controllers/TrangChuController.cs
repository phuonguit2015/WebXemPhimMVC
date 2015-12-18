using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.DAL;

namespace WebXemPhim.Controllers
{
    public class TrangChuController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: TrangChu
        public ActionResult Index()
        {
            // Lấy 7 Phim trong CSDL
            var phimMoi = db.Phims.Take(7).ToList();
            return View(phimMoi);
        }

        public PartialViewResult PhimDangChieu()
        {
            var phimDangChieu = db.Phims.Take(7).ToList();
            return PartialView(phimDangChieu);
        }

        public PartialViewResult DatVePartial()
        {
            return PartialView();
        }

        public PartialViewResult PhimSapChieuPartial()
        {
            var phimSapChieu = db.Phims.Take(7).ToList();
            return PartialView(phimSapChieu);
        }
    }
}