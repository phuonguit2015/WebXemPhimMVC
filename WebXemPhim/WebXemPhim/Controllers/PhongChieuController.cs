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
    public class PhongChieuController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: PhongChieu
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

            var phongChieu = from l in db.PhongChieux select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                phongChieu = phongChieu.Where(s => s.TenPhongChieu.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    phongChieu = phongChieu.OrderByDescending(l => l.TenPhongChieu);
                    break;
                default:
                    phongChieu = phongChieu.OrderBy(l => l.TenPhongChieu);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(phongChieu.ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index()
        //{
        //    return View(db.PhongChieux.ToList());
        //}

        // GET: PhongChieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // GET: PhongChieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongChieu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhongChieuID,TenPhongChieu,SoLuongphongChieu,ThongTin")] PhongChieu phongChieu)
        {
            if (ModelState.IsValid)
            {
                db.PhongChieux.Add(phongChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phongChieu);
        }

        // GET: PhongChieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // POST: PhongChieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhongChieuID,TenPhongChieu,SoLuongphongChieu,ThongTin")] PhongChieu phongChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phongChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phongChieu);
        }

        // GET: PhongChieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // POST: PhongChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            db.PhongChieux.Remove(phongChieu);
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
