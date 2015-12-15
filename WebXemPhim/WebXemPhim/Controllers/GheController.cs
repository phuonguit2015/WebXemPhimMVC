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
    public class GheController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Ghe
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

            var ghe = from l in db.Ghes select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                ghe = ghe.Where(s => s.SoGhe.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ghe = ghe.OrderByDescending(l => l.SoGhe);
                    break;
                default:
                    ghe = ghe.OrderBy(l => l.SoGhe);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(ghe.ToPagedList(pageNumber, pageSize));
        }
        //public ActionResult Index()
        //{
        //    return View(db.Ghes.ToList());
        //}

        // GET: Ghe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // GET: Ghe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ghe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GheID,SoGhe")] Ghe ghe)
        {
            if (ModelState.IsValid)
            {
                db.Ghes.Add(ghe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ghe);
        }

        // GET: Ghe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // POST: Ghe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GheID,SoGhe")] Ghe ghe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ghe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ghe);
        }

        // GET: Ghe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // POST: Ghe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ghe ghe = db.Ghes.Find(id);
            db.Ghes.Remove(ghe);
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
