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
    public class KHACHHANGController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: KHACHHANG

        public ActionResult getKHACHHANG()
        {
            var kh = db.KHACHHANG.ToList();
            return View(kh);
        }
        public ActionResult getKHACHHANGNV()
        {
            var kh = db.KHACHHANG.ToList();
            return View(kh);
        }
        public ActionResult gethoadonKHACHHANG(string id)
        {
            var kh = db.KHACHHANG.Where(s=>s.ID_KHACHHANG==id).ToList();
            return View(kh);
        }
        public ActionResult Index()
        {
            if (Session["TEN"] == null)
                return RedirectToAction("LoginAdmin", "DANGNHAP");
            string tdn = Session["TEN"].ToString();
        
            var kHACHHANG = db.KHACHHANG.Where(k => k.TENDANGNHAP == tdn).Include(k => k.TAIKHOAN);
            //ViewBag.ttkh =db.KHACHHANG.Where(t=>t.ID_KHACHHANG== kHACHHANG.)
            return View(kHACHHANG.ToList());
        }
        
        // GET: KHACHHANG/Details/5
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

        // GET: KHACHHANG/Create
        public ActionResult Create()
        {
            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU");
            return View();
        }

        // POST: KHACHHANG/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TENKHACHHANG,PHAIKHACHHANG,DIACHI,EMAIL,DIENTHOAI")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                kHACHHANG.ID_KHACHHANG= Helper.getNewKey(db.KHACHHANG.Max(t => t.ID_KHACHHANG));
                kHACHHANG.TENDANGNHAP = Session["TEN"].ToString();
                db.KHACHHANG.Add(kHACHHANG);
                db.SaveChanges();
                GIOHANG model2 = new GIOHANG();
                string ma = db.GIOHANG.Max(s => s.ID_GIOHANG);
                model2.ID_GIOHANG = Helper.getNewKey(ma);
                model2.ID_KHACHHANG = kHACHHANG.ID_KHACHHANG;
                db.GIOHANG.Add(model2);
                db.SaveChanges();
                CHITIET_GIOHANG model3 = new CHITIET_GIOHANG();
                model3.ID_GIOHANG = model2.ID_GIOHANG;
                model3.ID_SANPHAM = "SP001192";
                model3.THANHTIEN = 0;
                model3.SOLUONGMUA = 1;
                db.CHITIET_GIOHANG.Add(model3);
                db.SaveChanges();
                return RedirectToAction("LoginAdmin","DANGNHAP");
            }

            ViewBag.TENDANGNHAP = new SelectList(db.TAIKHOAN, "TENDANGNHAP", "MATKHAU", kHACHHANG.TENDANGNHAP);
            return View(kHACHHANG);
        }

        // GET: KHACHHANG/Edit/5
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

        // POST: KHACHHANG/Edit/5
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

        // GET: KHACHHANG/Delete/5
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

        // POST: KHACHHANG/Delete/5
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
