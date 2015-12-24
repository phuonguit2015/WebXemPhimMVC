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
        //    // Lấy danh sách phim đang bán vé - phim đang chiếu
        //    var phim = db.Phims.Where(p => p.TrangThai == Models.TrangThaiPhim.Dang_Chieu);
        //    List<SelectListItem> selectListItemPhims = new List<SelectListItem>();
        //    foreach (var item in phim)
        //    {
        //        SelectListItem selectListItemPhim = new SelectListItem
        //        {
        //            Text = item.TenPhim,
        //            Value = item.PhimID.ToString(),                   
        //        };
        //        selectListItemPhims.Add(selectListItemPhim);
        //    }
        //    ViewBag.Phim = selectListItemPhims;

            return PartialView();
        }

        // Lấy danh sách  phim
        public JsonResult FillMovies()
        {
            var phims = db.Phims.Where(p=>p.TrangThai == Models.TrangThaiPhim.Dang_Chieu);
            var list = new List<WebXemPhim.Models.Phim>();
            WebXemPhim.Models.Phim _phim = null;
            foreach(var item in phims)
            {
                _phim = new Models.Phim();
                _phim.PhimID = item.PhimID;
                _phim.TenPhim = item.TenPhim;
                list.Add(_phim);
            }
            return Json(new
            {
                data = list,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        // Lấy danh sách lịch chiếu theo phim
        public JsonResult FillShowTime(int Phim)
        {
            var lichChieus = db.LichChieux.Where(c => c.PhimID == Phim);
            var list = new List<WebXemPhim.Models.ShortLichChieu>();
            WebXemPhim.Models.ShortLichChieu _lichChieu = null;
            foreach (var item in lichChieus)
            {
                _lichChieu = new Models.ShortLichChieu();
                _lichChieu.LichChieuID = item.LichChieuID;
                _lichChieu.GioChieu = item.GioChieu.ToShortTimeString();
                _lichChieu.NgayChieu = item.NgayChieu.ToShortDateString();
                list.Add(_lichChieu);
            }
            return Json(new {
                data = list,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult PhimSapChieuPartial()
        {
            var phimSapChieu = db.Phims.Take(7).ToList();
            return PartialView(phimSapChieu);
        }        
    }
}