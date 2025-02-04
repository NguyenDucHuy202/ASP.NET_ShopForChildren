using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class ADMIN_QLKhoHangController : Controller
    {
        // GET: ADMIN_QLKhoHang
        DB_BANHANGEntities db = new DB_BANHANGEntities();
        public ActionResult Index()
        {
            List<SANPHAM> listSP = db.SANPHAM.ToList();

            ViewBag.SoLuongSPCanNhap = listSP.Where(t => int.Parse(t.SOLUONG) < 100).Count();
            return View(listSP);
        }
        public ActionResult QLKho_NhanVien()
        {
            List<SANPHAM> listSP = db.SANPHAM.ToList();

            return View(listSP);
        }


        public ActionResult SanPhamCanNhap()
        {
            List<SANPHAM> listSP = db.SANPHAM.ToList();
            List<SANPHAM> slcannhap = listSP.Where(t => int.Parse(t.SOLUONG) < 100).ToList();
            return View(slcannhap);
        }

        public ActionResult TaoPhieuNhap(string[] sanPhamMua)
        {
            if (sanPhamMua==null)
            {
                return RedirectToAction("SanPhamCanNhap");
            }
            PHIEUDAT phieuDat = new PHIEUDAT();
            phieuDat.ID_PHIEUDAT = Helper.getNewKey(db.PHIEUDAT.Max(t => t.ID_PHIEUDAT));
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime nowVST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
            phieuDat.NGAYDATHANG = nowVST;
            phieuDat.TRANGTHAI = "Đang tạo";
            int tongTien = 0;
            foreach (string sp in sanPhamMua)
            {
                SANPHAM sanPham = db.SANPHAM.First(t => t.ID_SANPHAM == sp);

                CHITIET_PHIEUDAT ctpd = new CHITIET_PHIEUDAT();
                ctpd.ID_PHIEUDAT = phieuDat.ID_PHIEUDAT;
                ctpd.ID_SANPHAM = sp;
                ctpd.SOLUONGDAT = 1;
                ctpd.THANHTIEN = sanPham.DONGIANHAP ?? 0;

                phieuDat.CHITIET_PHIEUDAT.Add(ctpd);
                tongTien += sanPham.DONGIANHAP ?? 0;
               
            }
            phieuDat.TONGTIEN = tongTien;

            db.PHIEUDAT.Add(phieuDat);
            db.SaveChanges();
            return View(phieuDat);
        }

        public ActionResult ChiTietPhieuNhap(string maPD, string maHH, int soLuong)
        {

            CHITIET_PHIEUDAT ctpd = db.CHITIET_PHIEUDAT.First(t => t.ID_PHIEUDAT == maPD && t.ID_SANPHAM == maHH);
            ctpd.SOLUONGDAT = soLuong;
            ctpd.THANHTIEN = soLuong * ctpd.SANPHAM.DONGIANHAP;

            db.SaveChanges();

            PHIEUDAT pd = db.PHIEUDAT.First(t => t.ID_PHIEUDAT == maPD);
            pd.TONGTIEN = db.CHITIET_PHIEUDAT.Where(t => t.ID_PHIEUDAT == maPD).Sum(t => t.THANHTIEN);
            db.SaveChanges();
            

            return PartialView(db.PHIEUDAT.First(t => t.ID_PHIEUDAT == maPD));
        }

        public ActionResult XacNhan(string maPD)
        {
            PHIEUDAT pd = db.PHIEUDAT.First(t => t.ID_PHIEUDAT == (maPD));
            foreach(CHITIET_PHIEUDAT ct in pd.CHITIET_PHIEUDAT)
            {
                int sl = int.Parse(db.SANPHAM.First(y => y.ID_SANPHAM == ct.ID_SANPHAM).SOLUONG ??"0") + ct.SOLUONGDAT??0;
                db.SANPHAM.First(y => y.ID_SANPHAM == ct.ID_SANPHAM).SOLUONG = sl.ToString();
            }    
            
            pd.TRANGTHAI = "Hoàn thành";

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Huy(string maPD)
        {
            db.CHITIET_PHIEUDAT.RemoveRange(db.CHITIET_PHIEUDAT.Where(t=>t.ID_PHIEUDAT==maPD));

            PHIEUDAT pd = db.PHIEUDAT.First(t => t.ID_PHIEUDAT == maPD);
            db.PHIEUDAT.Remove(pd);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }


}