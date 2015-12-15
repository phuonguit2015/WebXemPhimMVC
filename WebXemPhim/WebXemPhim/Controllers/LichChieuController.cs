using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using PagedList;
using System.Web.Mvc;
using WebXemPhim.DAL;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class LichChieuController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: LichChieu
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

            var lichChieux = db.LichChieux.Include(l => l.Phim).Include(l => l.PhongChieu);
            if (!String.IsNullOrEmpty(searchString))
            {
                lichChieux = lichChieux.Where(s => s.Phim.TenPhim.Contains(searchString));
                lichChieux = lichChieux.Where(s => s.PhongChieu.TenPhongChieu.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    lichChieux = lichChieux.OrderByDescending(p => p.NgayChieu);
                    break;
                default:
                    lichChieux = lichChieux.OrderBy(p => p.NgayChieu);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(lichChieux.ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index()
        //{
        //    var lichChieux = db.LichChieux.Include(l => l.Phim).Include(l => l.PhongChieu);
        //    return View(lichChieux.ToList());
        //}

        // GET: LichChieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichChieu lichChieu = db.LichChieux.Find(id);
            if (lichChieu == null)
            {
                return HttpNotFound();
            }
            return View(lichChieu);
        }

        // GET: LichChieu/Create
        public ActionResult Create()
        {
            ViewBag.PhimID = new SelectList(db.Phims, "PhimID", "TenPhim");
            ViewBag.PhongChieuID = new SelectList(db.PhongChieux, "PhongChieuID", "TenPhongChieu");
            return View();
        }

        // POST: LichChieu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LichChieuID,PhimID,PhongChieuID,NgayChieu,GioChieu")] LichChieu lichChieu)
        {
            if (ModelState.IsValid)
            {
                db.LichChieux.Add(lichChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhimID = new SelectList(db.Phims, "PhimID", "TenPhim", lichChieu.PhimID);
            ViewBag.PhongChieuID = new SelectList(db.PhongChieux, "PhongChieuID", "TenPhongChieu", lichChieu.PhongChieuID);
            return View(lichChieu);
        }

        // GET: LichChieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichChieu lichChieu = db.LichChieux.Find(id);
            if (lichChieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhimID = new SelectList(db.Phims, "PhimID", "TenPhim", lichChieu.PhimID);
            ViewBag.PhongChieuID = new SelectList(db.PhongChieux, "PhongChieuID", "TenPhongChieu", lichChieu.PhongChieuID);
            return View(lichChieu);
        }

        // POST: LichChieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LichChieuID,PhimID,PhongChieuID,NgayChieu,GioChieu")] LichChieu lichChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhimID = new SelectList(db.Phims, "PhimID", "TenPhim", lichChieu.PhimID);
            ViewBag.PhongChieuID = new SelectList(db.PhongChieux, "PhongChieuID", "TenPhongChieu", lichChieu.PhongChieuID);
            return View(lichChieu);
        }

        // GET: LichChieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichChieu lichChieu = db.LichChieux.Find(id);
            if (lichChieu == null)
            {
                return HttpNotFound();
            }
            return View(lichChieu);
        }

        // POST: LichChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichChieu lichChieu = db.LichChieux.Find(id);
            db.LichChieux.Remove(lichChieu);
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
