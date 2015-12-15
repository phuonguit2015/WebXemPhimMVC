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

namespace WebXemPhim.Controllers
{
    public class DatVeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: DatVe
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

            var ves = db.Ves.Include(v => v.Ghe).Include(v => v.LichChieu).Include(v => v.LoaiVe);
            if (!String.IsNullOrEmpty(searchString))
            {
                ves = ves.Where(s => s.LichChieu.Phim.TenPhim.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ves = ves.OrderByDescending(p => p.NgayDatVe);
                    break;
                default:
                    ves = ves.OrderBy(p => p.NgayDatVe);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(ves.ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index()
        //{
        //    var ves = db.Ves.Include(v => v.Ghe).Include(v => v.LichChieu).Include(v => v.LoaiVe);
        //    return View(ves.ToList());
        //}

        // GET: DatVe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ve ve = db.Ves.Find(id);
            if (ve == null)
            {
                return HttpNotFound();
            }
            return View(ve);
        }

        // GET: DatVe/Create
        public ActionResult Create()
        {
            ViewBag.GheID = new SelectList(db.Ghes, "GheID", "SoGhe");
            ViewBag.LichChieuID = new SelectList(db.LichChieux, "LichChieuID", "LichChieuID");
            ViewBag.LoaiVeID = new SelectList(db.LoaiVes, "LoaiVeID", "TenLoaiVe");
            return View();
        }

        // POST: DatVe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VeID,Code,GheID,LoaiVeID,LichChieuID,NgayDatVe,NgayThanhToan,SoCMND,SoDienThoai,TenKhachHang")] Ve ve)
        {
            if (ModelState.IsValid)
            {
                db.Ves.Add(ve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GheID = new SelectList(db.Ghes, "GheID", "SoGhe", ve.GheID);
            ViewBag.LichChieuID = new SelectList(db.LichChieux, "LichChieuID", "LichChieuID", ve.LichChieuID);
            ViewBag.LoaiVeID = new SelectList(db.LoaiVes, "LoaiVeID", "TenLoaiVe", ve.LoaiVeID);
            return View(ve);
        }

        // GET: DatVe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ve ve = db.Ves.Find(id);
            if (ve == null)
            {
                return HttpNotFound();
            }
            ViewBag.GheID = new SelectList(db.Ghes, "GheID", "SoGhe", ve.GheID);
            ViewBag.LichChieuID = new SelectList(db.LichChieux, "LichChieuID", "LichChieuID", ve.LichChieuID);
            ViewBag.LoaiVeID = new SelectList(db.LoaiVes, "LoaiVeID", "TenLoaiVe", ve.LoaiVeID);
            return View(ve);
        }

        // POST: DatVe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VeID,Code,GheID,LoaiVeID,LichChieuID,NgayDatVe,NgayThanhToan,SoCMND,SoDienThoai,TenKhachHang")] Ve ve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GheID = new SelectList(db.Ghes, "GheID", "SoGhe", ve.GheID);
            ViewBag.LichChieuID = new SelectList(db.LichChieux, "LichChieuID", "LichChieuID", ve.LichChieuID);
            ViewBag.LoaiVeID = new SelectList(db.LoaiVes, "LoaiVeID", "TenLoaiVe", ve.LoaiVeID);
            return View(ve);
        }

        // GET: DatVe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ve ve = db.Ves.Find(id);
            if (ve == null)
            {
                return HttpNotFound();
            }
            return View(ve);
        }

        // POST: DatVe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ve ve = db.Ves.Find(id);
            db.Ves.Remove(ve);
            db.SaveChanges();
            return RedirectToAction("Index");
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
