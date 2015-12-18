using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.DAL;
using WebXemPhim.Models;
using PagedList;
using System.IO;

namespace WebXemPhim.Controllers
{
    public class PhimController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Phim
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var phim = db.Phims.Include(p => p.LoaiPhim);

            if (!String.IsNullOrEmpty(searchString))
            {
                phim = phim.Where(s => s.TenPhim.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    phim = phim.OrderByDescending(p => p.TenPhim);
                    break;
                default:
                    phim = phim.OrderBy(p => p.TenPhim);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(phim.ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index()
        //{
        //    var phims = db.Phims.Include(p => p.LoaiPhim);
        //    return View(phims.ToList());
        //}

        // GET: Phim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // GET: Phim/Create
        public ActionResult Create()
        {
            ViewBag.LoaiPhimID = new SelectList(db.LoaiPhims, "LoaiPhimID", "TenLoaiPhim");
            return View();
        }

        // POST: Phim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhimID,TenPhim,DaoDien,DienVien,NoiDung,Poster,ThoiLuong,TrailerURL,TrangThai,NgayChieu,LoaiPhimID")] Phim phim, HttpPostedFileBase uploadFile)
        {
            if (uploadFile == null)
            {
                ModelState.AddModelError(string.Empty, "An image file must be chosen.");
            }
            else if (ModelState.IsValid)
            {
                using (var reader = new System.IO.BinaryReader(uploadFile.InputStream))
                {
                    phim.Poster = reader.ReadBytes(uploadFile.ContentLength);
                }
                db.Phims.Add(phim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiPhimID = new SelectList(db.LoaiPhims, "LoaiPhimID", "TenLoaiPhim", phim.LoaiPhimID);
            return View(phim);
        }

        // GET: Phim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiPhimID = new SelectList(db.LoaiPhims, "LoaiPhimID", "TenLoaiPhim", phim.LoaiPhimID);
            return View(phim);
        }

        // POST: Phim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhimID,TenPhim,DaoDien,DienVien,NoiDung,Poster,ThoiLuong,TrailerURL,TrangThai,NgayChieu,LoaiPhimID")] Phim phim,
            HttpPostedFileBase uploadFile)
        {
            if (uploadFile == null)
            {
                ModelState.AddModelError(string.Empty, "Hình ảnh Poster chưa được chọn.");
            }
            else if(ModelState.IsValid)
            {
                using (var reader = new System.IO.BinaryReader(uploadFile.InputStream))
                {
                    phim.Poster = reader.ReadBytes(uploadFile.ContentLength);
                }
                db.Entry(phim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiPhimID = new SelectList(db.LoaiPhims, "LoaiPhimID", "TenLoaiPhim", phim.LoaiPhimID);
            return View(phim);
        }

        // GET: Phim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: Phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim phim = db.Phims.Find(id);
            db.Phims.Remove(phim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Trả về Poster của từng image
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewImage(int id)
        {
            var item = db.Phims.FirstOrDefault<Phim>(p => p.PhimID == id);
            if (item == null)
                return null;
            byte[] buffer = item.Poster;
            return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
        }

     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
