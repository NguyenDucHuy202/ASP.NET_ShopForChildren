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
    public class TAIKHOANController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: TAIKHOAN
        public ActionResult Index()
        {
            return View(db.TAIKHOAN.ToList());
        }

        // GET: TAIKHOAN/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // GET: TAIKHOAN/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TAIKHOAN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TENDANGNHAP,MATKHAU")]  TAIKHOAN tAIKHOAN, string repass)
        {
            if (ModelState.IsValid)
            {
                if (db.TAIKHOAN.Where(m => m.TENDANGNHAP == tAIKHOAN.TENDANGNHAP).FirstOrDefault()==null)
                {
                    if (repass == tAIKHOAN.MATKHAU)
                    {
                        db.TAIKHOAN.Add(tAIKHOAN);
                        db.SaveChanges();
                        Session["TEN"] = tAIKHOAN.TENDANGNHAP;
                        string sessionValue = Session["TEN"] as string;
                        KHACHHANG model = new KHACHHANG();
                        model.TENDANGNHAP = sessionValue;
                        
                       



                        return RedirectToAction("Index","KHACHHANG");
                    }
                    else
                    {
                        ViewBag.Message = "repass and pass not match";
                        return View(tAIKHOAN);
                    }
                }
                else
                {
                    ViewBag.Message = "Already have";
                    return View(tAIKHOAN);
                }
            }


            return View(tAIKHOAN);
        }

        // GET: TAIKHOAN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: TAIKHOAN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TENDANGNHAP,MATKHAU")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tAIKHOAN);
        }

        // GET: TAIKHOAN/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: TAIKHOAN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            db.TAIKHOAN.Remove(tAIKHOAN);
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
