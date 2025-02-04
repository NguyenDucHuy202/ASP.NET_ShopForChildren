using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_BanDoTreEm.Models;

namespace QL_BanDoTreEm.Controllers
{
    public class ADMIN_QLNguoiDungController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: ADMIN_QLNguoiDung
        public ActionResult Index()
        {
          
            var kHACHHANG = db.KHACHHANG.Include(k => k.TAIKHOAN);
            @ViewBag.SoLuongNguoiDung = kHACHHANG.ToList().Count();
            return View(kHACHHANG.ToList());
        }
        

        // GET: ADMIN_QLNguoiDung/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: ADMIN_QLNguoiDung/Create
        public ActionResult Create()
        {
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU");
            return View();
        }

        // POST: ADMIN_QLNguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_KHACHHANG,TENKHACHHANG,PHAIKHACHHANG,DIACHI,EMAIL,DIENTHOAI,TENDANGNHAP")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANG.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", kHACHHANG.TENDANGNHAP);
            return View(kHACHHANG);
        }

        // GET: ADMIN_QLNguoiDung/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", kHACHHANG.TENDANGNHAP);
            return View(kHACHHANG);
        }

        // POST: ADMIN_QLNguoiDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_KHACHHANG,TENKHACHHANG,PHAIKHACHHANG,DIACHI,EMAIL,DIENTHOAI,TENDANGNHAP")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", kHACHHANG.TENDANGNHAP);
            return View(kHACHHANG);
        }

        // GET: ADMIN_QLNguoiDung/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: ADMIN_QLNguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANG.Find(id);
            db.KHACHHANG.Remove(kHACHHANG);
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
