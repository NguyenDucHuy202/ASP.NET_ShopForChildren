using Antlr.Runtime.Misc;
using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class LICHSUMUAHANGController : Controller
    {
        // GET: LICHSUMUAHANG
        public ActionResult Index()
        {
            if (Session["TEN"] == null)
                return RedirectToAction("LoginAdmin", "DANGNHAP");

            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
            string tendangnhap = Session["TEN"].ToString();

            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();
            List<HOADON> hd = dB_BANHANGEntities.HOADON.Where(t => t.ID_KHACHHANG == kh.ID_KHACHHANG).ToList();

            
            return View(hd);
        }

        public ActionResult ChiTietDonHang(string id)
        {

            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();

            if (Session["TEN"] == null)
                return RedirectToAction("LoginAdmin", "DANGNHAP");
            string tendangnhap = Session["TEN"].ToString();
                
            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();

            HOADON hd = dB_BANHANGEntities.HOADON.Where(t => t.ID_HOADON == id).First();
            
           List<CHITIET_HOADON> cthd = dB_BANHANGEntities.CHITIET_HOADON.Where(t=>t.ID_HOADON == hd.ID_HOADON).ToList();



            return View(cthd);
        }

    }
}