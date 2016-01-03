using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using WebXemPhim.DAL;
using WebXemPhim.Models;
using System.Linq;
using System;
using System.Collections.Generic;

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
            return View();
        }

        public PartialViewResult DanhSachLichChieuPartial(int PhimID, DateTime ngayChieu, bool CoNgayChieu)
        {
            List<LichChieu> _danhSachLichChieu = new List<LichChieu>();
            // Lây danh sách phim
            if (PhimID != -1)
            {
                _danhSachLichChieu = db.LichChieux.Include(l => l.Phim).Where(lc => lc.PhimID == PhimID).ToList();
            }
            else
            {
                _danhSachLichChieu = db.LichChieux.Include(l => l.Phim).Where(lc => lc.Phim.TrangThai == TrangThaiPhim.Dang_Chieu).ToList();
            }
            // Nếu có ngày chiếu thì lọc danh sách đó theo ngày chiếu
            if (CoNgayChieu)
            {
                _danhSachLichChieu = _danhSachLichChieu.Where(l => l.NgayChieu == ngayChieu).ToList();

            }
           

            List<LichChieu> _danhSachPhim = new List<LichChieu>();
            List<LichChieu> _danhSachNgayChieu = new List<LichChieu>();

            WebXemPhim.Models.LichChieu _lichChieu = null;
            foreach (var item in _danhSachLichChieu)
            {
                _lichChieu = new Models.LichChieu();
                _lichChieu.LichChieuID = item.LichChieuID;
                _lichChieu.Phim = item.Phim;
                _lichChieu.PhimID = item.PhimID;      
                _lichChieu.NgayChieu = item.NgayChieu;
                if ((_danhSachPhim.FindIndex(p => p.PhimID == item.PhimID)) < 0)
                {
                    _danhSachPhim.Add(_lichChieu); // Dánh sách Phim
                }
                _danhSachNgayChieu.Add(_lichChieu); // Dánh sách ngày chiếu

            }
            ViewBag.DanhSachPhim = _danhSachPhim;
            ViewBag.DanhSachNgayChieu = _danhSachNgayChieu;
            // Lịch Chiêu
            return PartialView();
        }
        public ActionResult TicketPrices()
        {
            ViewBag.Message = "Your TicketPrices page.";

            return View();
        }

        // Danh sách phim đang chiếu
        public JsonResult DanhSachPhimDangChieu()
        {
            var phims = db.Phims.Where(p => p.TrangThai == Models.TrangThaiPhim.Dang_Chieu);
            var list = new List<WebXemPhim.Models.Phim>();
            WebXemPhim.Models.Phim _phim = null;
            foreach (var item in phims)
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
            }, JsonRequestBehavior.AllowGet);
        }

        // Danh sách loại vé
        public JsonResult DanhSachLoaiVe()
        {
            var loaiVe = db.LoaiVes.ToList();
            var list = new List<WebXemPhim.Models.LoaiVe>();
            WebXemPhim.Models.LoaiVe _loaiVe = null;
            foreach (var item in loaiVe)
            {
                _loaiVe = new Models.LoaiVe();
                _loaiVe.LoaiVeID = item.LoaiVeID;
                _loaiVe.TenLoaiVe = item.TenLoaiVe;
                list.Add(_loaiVe);
            }
            return Json(new
            {
                data = list,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        
        // Danh sách ngày chiếu, theo phim, theo loại vé    
        public JsonResult DanhSachNgayChieu(int PhimID, int LoaiVeID)
        {
            var lichChieus = db.LichChieux.Where(l => l.PhimID == PhimID && l.LoaiVeID == LoaiVeID).ToList();
            var list = new List<LichChieu>();
            LichChieu _lichChieu;
            foreach(var item in lichChieus)
            {
                _lichChieu = new LichChieu();
                _lichChieu.LichChieuID = item.LichChieuID;
                _lichChieu.NgayChieu = item.NgayChieu;
                int index = list.FindIndex(i => i.NgayChieu == _lichChieu.NgayChieu);
                if (index < 0)
                {
                    list.Add(_lichChieu);
                }
            }
            return Json(new
            {
                data = list,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        // Danh sách giờ chiếu theo phim, theo loại vé, theo ngày
        public JsonResult DanhSachGioChieu(int LichChieuID)
        {
            var temp = db.LichChieux.Single(l => l.LichChieuID == LichChieuID);
            int PhimID = temp.PhimID;
            int LoaiVeID = temp.LoaiVeID;
            DateTime NgayChieu = temp.NgayChieu;

            var lichChieus = db.LichChieux.Where(l => l.PhimID == PhimID && l.LoaiVeID == LoaiVeID && l.NgayChieu == NgayChieu).ToList();
            var list = new List<LichChieu>();
            LichChieu _lichChieu;
            foreach (var item in lichChieus)
            {
                _lichChieu = new LichChieu();
                _lichChieu.LichChieuID = item.LichChieuID;
                _lichChieu.NgayChieu = item.NgayChieu;
                _lichChieu.GioChieu = item.GioChieu;
                list.Add(_lichChieu);
            }
            return Json(new
            {
                data = list,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        // Thông tin phòng chiếu theo lịch chiếu 
        public JsonResult CapNhatPhongChieu(int LichChieuID)
        {
            var temp = db.LichChieux.Include(l => l.PhongChieu).Single(l => l.LichChieuID == LichChieuID);
            string tenPhongChieu = temp.PhongChieu.TenPhongChieu;
            return Json(tenPhongChieu, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DanhSachGhe(int LichChieuID)
        {
            var temp = db.LichChieux.Single(l => l.LichChieuID == LichChieuID);
            // Danh sách ghế đã đặt của phim A
            var danhsachGheDaDat = db.Ves.Include(l => l.Ghe).Where(v => v.LichChieuID == LichChieuID);
            // Danh sách ghế trong phong chiếu
            var danhSachGhe = db.Ghes.ToList();
            // Loại danh sách các ghế đã đặt khỏi danh sách ghế.
            var danhSach = new List<Ghe>();
            foreach(var i in danhSachGhe)
            {
                foreach(var j in danhsachGheDaDat)
                {
                    if(i != j.Ghe)
                    {
                        danhSach.Add(i);
                        break;
                    }
                }
            }
            var list = new List<Ghe>();
            Ghe ghe;
            foreach(var item in danhSach)
            {
                ghe = new Ghe();
                ghe.GheID = item.GheID;
                ghe.SoGhe = item.SoGhe;
                list.Add(ghe);
            }
            return Json(new
            {
                data = list,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CapNhatGiaVe(int loaiVeID)
        {
            var temp = db.LoaiVes.Single(l => l.LoaiVeID == loaiVeID);
            string giaVe = temp.GiaTriToString;
            return Json(giaVe, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BuyTicket()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyTicket([Bind(Include = "GheID,LoaiVeID,LichChieuID,ThanhTien,SoCMND,SoDienThoai,TenKhachHang")] Ve ve)
        {
            if (ModelState.IsValid)
            {
                ve.NgayDatVe = DateTime.Now;
                ve.NgayThanhToan = DateTime.Now;

                // Tạo mã vé
                string code = "ENJPN" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                ve.Code = code;
                db.Ves.Add(ve);
                db.SaveChanges();
                return RedirectToAction("Index","TrangChu");
            }                    
            return View(ve);
        }
     

    }
}