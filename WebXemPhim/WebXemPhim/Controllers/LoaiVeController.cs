using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.DAL;
using PagedList;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class LoaiVeController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: LoaiVe
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

            var loaiVe = from l in db.LoaiVes select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                loaiVe = loaiVe.Where(s => s.TenLoaiVe.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    loaiVe = loaiVe.OrderByDescending(l => l.TenLoaiVe);
                    break;
                default:
                    loaiVe = loaiVe.OrderBy(l => l.TenLoaiVe);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(loaiVe.ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index()
        //{
        //    return View(db.LoaiVes.ToList());
        //}

        // GET: LoaiVe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVe loaiVe = db.LoaiVes.Find(id);
            if (loaiVe == null)
            {
                return HttpNotFound();
            }
            return View(loaiVe);
        }

        // GET: LoaiVe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiVe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoaiVeID,TenLoaiVe,GiaTri")] LoaiVe loaiVe)
        {
            if (ModelState.IsValid)
            {
                db.LoaiVes.Add(loaiVe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiVe);
        }

        // GET: LoaiVe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVe loaiVe = db.LoaiVes.Find(id);
            if (loaiVe == null)
            {
                return HttpNotFound();
            }
            return View(loaiVe);
        }

        // POST: LoaiVe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoaiVeID,TenLoaiVe,GiaTri")] LoaiVe loaiVe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiVe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiVe);
        }

        // GET: LoaiVe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVe loaiVe = db.LoaiVes.Find(id);
            if (loaiVe == null)
            {
                return HttpNotFound();
            }
            return View(loaiVe);
        }

        // POST: LoaiVe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiVe loaiVe = db.LoaiVes.Find(id);
            db.LoaiVes.Remove(loaiVe);
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
