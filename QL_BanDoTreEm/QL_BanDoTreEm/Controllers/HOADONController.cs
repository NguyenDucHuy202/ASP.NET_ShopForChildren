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
    public class HOADONController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: HOADON
        public ActionResult Index(NHANVIEN nHANVIEN)
        {
            NHANVIEN nv;
            if (Session["NV"] != null)
                nv = Session["NV"] as NHANVIEN;
            else
            {
                return RedirectToAction("Index", "ADMIN_QLSanPham");
            }

            var hOADON = db.HOADON.Include(h => h.GIAMGIA1).Include(h => h.KHACHHANG).Where(s => s.ID_NHANVIEN == nHANVIEN.ID_NHANVIEN);
            return View(hOADON.ToList());
        }
        public ActionResult getHOADONNULL()
        {
            var hOADON = db.HOADON.Include(h => h.GIAMGIA1).Include(h => h.KHACHHANG).Where(s => s.ID_NHANVIEN == null);


            return View(hOADON.ToList());
        }
        public ActionResult getHOADONKHACHHANG(HOADON  hOADON)
        {
            var hoadon = db.HOADON.Include(h => h.GIAMGIA1).Include(h => h.KHACHHANG).Where(s => s.ID_KHACHHANG == hOADON.ID_KHACHHANG);


            return View(hoadon.ToList());
        }

        public ActionResult getALLHOADON()
        {
            var hOADON = db.HOADON.Include(h => h.GIAMGIA1).Include(h => h.KHACHHANG).Where(t => t.ID_NHANVIEN != null);


            return View(hOADON.ToList());
        }
        public ActionResult getKHACHHANG(string id)
        {
            var hOADON = db.HOADON.Include(h => h.GIAMGIA1).Include(h => h.KHACHHANG).Where(s => s.ID_KHACHHANG == id);


            return View(hOADON.ToList());
        }
        public ActionResult getKHACHHANGNV(string id)
        {
            var hOADON = db.HOADON.Include(h => h.GIAMGIA1).Include(h => h.KHACHHANG).Where(s => s.ID_KHACHHANG == id);


            return View(hOADON.ToList());
        }

        // GET: HOADON/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADON.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // GET: HOADON/Create
        public ActionResult Create()
        {
            ViewBag.ID_GIAMGIA = new SelectList(db.GIAMGIA, "ID_GIAMGIA", "ID_GIAMGIA");
            ViewBag.ID_KHACHHANG = new SelectList(db.KHACHHANG, "ID_KHACHHANG", "TENKHACHHANG");
            return View();
        }

        // POST: HOADON/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_HOADON,NGAYLAP,GIAMGIA,PHIVANCHUYEN,TONGTIEN,TRANGTHAI,ID_GIAMGIA,ID_NHANVIEN,ID_KHACHHANG")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.HOADON.Add(hOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_GIAMGIA = new SelectList(db.GIAMGIA, "ID_GIAMGIA", "ID_GIAMGIA", hOADON.ID_GIAMGIA);
            ViewBag.ID_KHACHHANG = new SelectList(db.KHACHHANG, "ID_KHACHHANG", "TENKHACHHANG", hOADON.ID_KHACHHANG);
            return View(hOADON);
        }

        // GET: HOADON/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADON.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIEN, "ID_NHANVIEN", "ID_NHANVIEN", hOADON.ID_NHANVIEN);
            ViewBag.ID_GIAMGIA = new SelectList(db.GIAMGIA, "ID_GIAMGIA", "ID_GIAMGIA", hOADON.ID_GIAMGIA);
            ViewBag.ID_KHACHHANG = new SelectList(db.KHACHHANG, "ID_KHACHHANG", "TENKHACHHANG", hOADON.ID_KHACHHANG);
            return View(hOADON);
        }

        // POST: HOADON/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HOADON, NGAYLAP,GIAMGIA,PHIVANCHUYEN,TONGTIEN,TRANGTHAI,ID_GIAMGIA,ID_NHANVIEN,ID_KHACHHANG")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("getHOADONNULL");
            }
            return View(hOADON);

        }
        public ActionResult EditHoaDonNHANVIEN(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADON.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // POST: HOADON/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHoaDonNHANVIEN([Bind(Include = "ID_HOADON, NGAYLAP,GIAMGIA,PHIVANCHUYEN,TONGTIEN,TRANGTHAI")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                NHANVIEN nv = Session["NV"] as NHANVIEN;
                hOADON.ID_NHANVIEN =nv.ID_NHANVIEN;
                db.Entry(hOADON).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index", "HOADON");
            }
            return View(hOADON);
        }

        // GET: HOADON/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADON.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // POST: HOADON/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOADON hOADON = db.HOADON.Where(t => t.ID_HOADON == id).FirstOrDefault();
            List<CHITIET_HOADON> cthoadon = db.CHITIET_HOADON.Where(t => t.ID_HOADON == id).ToList();
            foreach (var ct in cthoadon)
            {
                db.CHITIET_HOADON.Remove(ct);
                db.SaveChanges();
            }
            db.HOADON.Remove(hOADON);
            db.SaveChanges();
            return RedirectToAction("getALLHOADON", "HOADON");
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
