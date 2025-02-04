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
    public class PHIEUDATController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: PHIEUDAT
        public ActionResult Index()
        {
            var pHIEUDAT = db.PHIEUDAT.Include(p => p.NHACUNGCAP).Include(p => p.NHANVIEN).Include(p=>p.CHITIET_PHIEUDAT);
            return View(pHIEUDAT.ToList());
        }

        // GET: PHIEUDAT/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDAT pHIEUDAT = db.PHIEUDAT.Find(id);
            if (pHIEUDAT == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUDAT);
        }

        // GET: PHIEUDAT/Create
        public ActionResult Create()
        {
            ViewBag.ID_NHACUNGCAP = new SelectList(db.NHACUNGCAP, "ID_NHACUNGCAP", "TENNHACUNGCAP");
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIEN, "ID_NHANVIEN", "TENNHANVIEN");
            return View();
        }

        // POST: PHIEUDAT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NGAYDATHANG,TONGTIEN,ID_NHACUNGCAP,ID_NHANVIEN")] PHIEUDAT pHIEUDAT)
        {
            if (ModelState.IsValid)
            {
                
                pHIEUDAT.ID_PHIEUDAT = Helper.getNewKey(db.PHIEUDAT.Max(t=>t.ID_PHIEUDAT));
                db.PHIEUDAT.Add(pHIEUDAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_NHACUNGCAP = new SelectList(db.NHACUNGCAP, "ID_NHACUNGCAP", "TENNHACUNGCAP", pHIEUDAT.ID_NHACUNGCAP);
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIEN, "ID_NHANVIEN", "TENNHANVIEN", pHIEUDAT.ID_NHANVIEN);
            return View(pHIEUDAT);
        }

        // GET: PHIEUDAT/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDAT pHIEUDAT = db.PHIEUDAT.Find(id);
            if (pHIEUDAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_NHACUNGCAP = new SelectList(db.NHACUNGCAP, "ID_NHACUNGCAP", "TENNHACUNGCAP", pHIEUDAT.ID_NHACUNGCAP);
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIEN, "ID_NHANVIEN", "TENNHANVIEN", pHIEUDAT.ID_NHANVIEN);
            return View(pHIEUDAT);
        }

        // POST: PHIEUDAT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PHIEUDAT,NGAYDATHANG,TONGTIEN,ID_NHACUNGCAP,ID_NHANVIEN")] PHIEUDAT pHIEUDAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUDAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_NHACUNGCAP = new SelectList(db.NHACUNGCAP, "ID_NHACUNGCAP", "TENNHACUNGCAP", pHIEUDAT.ID_NHACUNGCAP);
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIEN, "ID_NHANVIEN", "TENNHANVIEN", pHIEUDAT.ID_NHANVIEN);
            return View(pHIEUDAT);
        }

        // GET: PHIEUDAT/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDAT pHIEUDAT = db.PHIEUDAT.Find(id);
            if (pHIEUDAT == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUDAT);
        }

        // POST: PHIEUDAT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHIEUDAT pHIEUDAT = db.PHIEUDAT.Find(id);
            db.PHIEUDAT.Remove(pHIEUDAT);
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
