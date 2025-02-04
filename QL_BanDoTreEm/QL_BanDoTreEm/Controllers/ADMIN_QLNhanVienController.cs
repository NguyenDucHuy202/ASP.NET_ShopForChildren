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
    public class ADMIN_QLNhanVienController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: ADMIN_QLNhanVien
        public ActionResult Index()
        {
            var nHANVIEN = db.NHANVIEN.Include(n => n.TAIKHOAN);
            return View(nHANVIEN.ToList());
        }

        // GET: ADMIN_QLNhanVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIEN.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // GET: ADMIN_QLNhanVien/Create
        public ActionResult Create()
        {
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU");
            return View();
        }

        // POST: ADMIN_QLNhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_NHANVIEN,TENNHANVIEN,PHAI,NGAYSINH,DIACHI,EMAIL,DIENTHOAI,CHUCVU,TENDANGNHAP")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.NHANVIEN.Add(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", nHANVIEN.TENDANGNHAP);
            return View(nHANVIEN);
        }

        // GET: ADMIN_QLNhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIEN.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", nHANVIEN.TENDANGNHAP);
            return View(nHANVIEN);
        }

        // POST: ADMIN_QLNhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_NHANVIEN,TENNHANVIEN,PHAI,NGAYSINH,DIACHI,EMAIL,DIENTHOAI,CHUCVU,TENDANGNHAP")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", nHANVIEN.TENDANGNHAP);
            return View(nHANVIEN);
        }

        // GET: ADMIN_QLNhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIEN.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: ADMIN_QLNhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIEN nHANVIEN = db.NHANVIEN.Where(t=>t.ID_NHANVIEN==id).FirstOrDefault();
            db.NHANVIEN.Remove(nHANVIEN);
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
