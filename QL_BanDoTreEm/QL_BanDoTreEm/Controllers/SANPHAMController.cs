using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QL_BanDoTreEm.Models;

namespace QL_BanDoTreEm.Controllers
{
    public class SANPHAMController : Controller
    {
        private DB_BANHANGEntities db = new DB_BANHANGEntities();

        // GET: SANPHAM
        public ActionResult Index()
        {
           
            var sANPHAM = db.SANPHAM.Include(s => s.LOAISANPHAM).Include(s => s.THUONGHIEU).Include(s => s.DANHMUCSANPHAM);
            return View(sANPHAM.ToList());
        }

        // GET: SANPHAM/Details/5
        public ActionResult Details(string id)
        {
         
            CHITIET_HOADON cthb = db.CHITIET_HOADON.Where(t => t.ID_SANPHAM == id).FirstOrDefault();
            int tongSoLuongBanRa = (int)((cthb != null) ? cthb.SOLUONGMUA : 0);

            CHITIET_PHIEUDAT ctpd = db.CHITIET_PHIEUDAT.Where(t => t.ID_SANPHAM == id).FirstOrDefault();
            int soLuongDat = (int)((ctpd != null) ? ctpd.SOLUONGDAT : 0);

            ViewBag.TongSoLuongBanRa = tongSoLuongBanRa;
            ViewBag.SoLuongDat = soLuongDat;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: SANPHAM/Create
        public ActionResult Create()
        {
            ViewBag.ID_LOAISP = new SelectList(db.LOAISANPHAM, "ID_LOAISP", "TENLOAISP");
            ViewBag.ID_THUONGHIEU = new SelectList(db.THUONGHIEU, "ID_THUONGHIEU", "TENTHUONGHIEU");
            return View();
        }
        public int getMaxID()
        {
            return (1 + int.Parse(db.SANPHAM.Max(t => t.ID_SANPHAM.Substring(2)).ToString()));
        }

        // POST: SANPHAM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SANPHAM,TENSANPHAM,THANHPHAN,HUONGDANSUDUNG,HUONGDANBAOQUAN,HANSUDUNG,UUTIENNOIBAT,CHATLIEU,SOLUONG,KICHTHUOC,THETICH,TRONGLUONG,DONGIABAN,NGAYSANXUAT,DOTUOIPHUHOP,LUUY,HINHANH,ID_THUONGHIEU,ID_LOAISP,DONGIANHAP")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                string maMoi = getMaxID().ToString();
                int max = 100000;
                while (int.Parse(maMoi) < max)
                {
                    maMoi = '0'+maMoi;
                    max /= 10;
                }
                maMoi = "SP" + maMoi;
                sANPHAM.ID_SANPHAM = maMoi;
                db.SANPHAM.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index","ADMIN_QLSanPham");
            }

            ViewBag.ID_LOAISP = new SelectList(db.LOAISANPHAM, "ID_LOAISP", "TENLOAISP", sANPHAM.ID_LOAISP);
            ViewBag.ID_THUONGHIEU = new SelectList(db.THUONGHIEU, "ID_THUONGHIEU", "TENTHUONGHIEU", sANPHAM.ID_THUONGHIEU);
            return View(sANPHAM);
        }

        // GET: SANPHAM/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LOAISP = new SelectList(db.LOAISANPHAM, "ID_LOAISP", "TENLOAISP", sANPHAM.ID_LOAISP);
            ViewBag.ID_THUONGHIEU = new SelectList(db.THUONGHIEU, "ID_THUONGHIEU", "TENTHUONGHIEU", sANPHAM.ID_THUONGHIEU);
            return View(sANPHAM);
        }

        // POST: SANPHAM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SANPHAM,TENSANPHAM,THANHPHAN,HUONGDANSUDUNG,HUONGDANBAOQUAN,HANSUDUNG,UUTIENNOIBAT,CHATLIEU,SOLUONG,KICHTHUOC,THETICH,TRONGLUONG,DONGIABAN,NGAYSANXUAT,DOTUOIPHUHOP,LUUY,HINHANH,ID_THUONGHIEU,ID_LOAISP,DONGIANHAP")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ADMIN_QLSanPham");
            }
            ViewBag.ID_LOAISP = new SelectList(db.LOAISANPHAM, "ID_LOAISP", "TENLOAISP", sANPHAM.ID_LOAISP);
            ViewBag.ID_THUONGHIEU = new SelectList(db.THUONGHIEU, "ID_THUONGHIEU", "TENTHUONGHIEU", sANPHAM.ID_THUONGHIEU);
            return View(sANPHAM);
        }

        // GET: SANPHAM/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: SANPHAM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SANPHAM sANPHAM = db.SANPHAM.Find(id);
            db.SANPHAM.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index", "ADMIN_QLSanPham");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult cardSANPHAM(string ID_DANHMUCSP, int?Trang)
        {
            

            if (Trang == null)
                Trang = 1;
            int SoSP = 12;
            int SoTrang = (Trang ?? 1); 
            List<SANPHAM> sSANPHAM = db.SANPHAM.Where(s => s.ID_DANHMUCSP == ID_DANHMUCSP).ToList();

            if (string.IsNullOrEmpty(ID_DANHMUCSP))
            {
                sSANPHAM = db.SANPHAM.ToList();
            }

            return View(sSANPHAM.ToPagedList(SoTrang,SoSP));
        }

        public ActionResult SanPhamTheoDanhMuc(string IDDanhMuc)
        {
            string tenDanhMuc = db.DANHMUCSANPHAM.First(t => t.ID_DANHMUCSP == IDDanhMuc).TENDANHMUCSP;
            var list = db.SANPHAM.Where(s => s.ID_DANHMUCSP == IDDanhMuc).OrderBy(s => s.DONGIABAN).ToList();
            if (list.Count != 0)
            {
                ViewBag.SanPham_s = tenDanhMuc;
            }

            else
            {
                ViewBag.SanPham_s = "Không có sản phẩm thuộc " + tenDanhMuc;
            }
            return View(list);
        }
    }
}
