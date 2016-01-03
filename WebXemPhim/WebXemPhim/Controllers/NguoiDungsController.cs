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
    public class NguoiDungsController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: NguoiDungs
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

            var nguoiDung = from l in db.NguoiDungs select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                nguoiDung = nguoiDung.Where(s => s.TenNguoiDung.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    nguoiDung = nguoiDung.OrderByDescending(l => l.TenNguoiDung);
                    break;
                default:
                    nguoiDung = nguoiDung.OrderBy(l => l.TenNguoiDung);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(nguoiDung.ToPagedList(pageNumber, pageSize));
        }

        // GET: NguoiDungs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NguoiDungID,TenNguoiDung,TaiKhoan,MatKhau")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nguoiDung);
        }

        // GET: NguoiDungs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NguoiDungID,TenNguoiDung,TaiKhoan,MatKhau")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Kiểm tra đăng nhập]
        public ActionResult KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            // Nếu tài khoản thât bại thông báo cho người dùng, nếu tài khoản thành công  thì return
            var nguoiDung = db.NguoiDungs.ToList().Where(n => n.TaiKhoan == taiKhoan && n.MatKhau == matKhau);

            if (nguoiDung.Count() == 0)
            {
                string str = "Tài khoản hoặc mật khẩu không đúng!";
                return RedirectToAction("Index", "TrangChu", new { message = str });
            }
            return RedirectToAction("Index", "Phim");
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
