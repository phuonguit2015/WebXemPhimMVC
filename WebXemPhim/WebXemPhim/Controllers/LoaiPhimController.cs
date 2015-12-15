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
    public class LoaiPhimController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: LoaiPhim
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

            var loaiPhim = from l in db.LoaiPhims select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                loaiPhim = loaiPhim.Where(s => s.TenLoaiPhim.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    loaiPhim = loaiPhim.OrderByDescending(l => l.TenLoaiPhim);
                    break;
                default:
                    loaiPhim = loaiPhim.OrderBy(l => l.TenLoaiPhim);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(loaiPhim.ToPagedList(pageNumber, pageSize));
        }

        // GET: LoaiPhim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhim loaiPhim = db.LoaiPhims.Find(id);
            if (loaiPhim == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhim);
        }

        // GET: LoaiPhim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiPhim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoaiPhimID,TenLoaiPhim")] LoaiPhim loaiPhim)
        {
            if (ModelState.IsValid)
            {
                db.LoaiPhims.Add(loaiPhim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiPhim);
        }

        // GET: LoaiPhim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhim loaiPhim = db.LoaiPhims.Find(id);
            if (loaiPhim == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhim);
        }

        // POST: LoaiPhim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoaiPhimID,TenLoaiPhim")] LoaiPhim loaiPhim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiPhim).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(loaiPhim);
        }

        // GET: LoaiPhim/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            LoaiPhim loaiPhim = db.LoaiPhims.Find(id);
            if (loaiPhim == null)
            {
                return HttpNotFound();
            }
            return View(loaiPhim);
        }

        // POST: LoaiPhim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LoaiPhim loaiPhim = db.LoaiPhims.Find(id);
                db.LoaiPhims.Remove(loaiPhim);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
