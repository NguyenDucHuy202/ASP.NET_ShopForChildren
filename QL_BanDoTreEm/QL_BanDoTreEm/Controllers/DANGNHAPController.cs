using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QL_BanDoTreEm.Controllers
{

    public class DANGNHAPController : Controller
    {
        DB_BANHANGEntities db = new DB_BANHANGEntities();

        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View(new TAIKHOAN());
        }
        [HttpPost]
        public ActionResult LoginAdmin(TAIKHOAN tAIKHOAN)
        {
            List<TAIKHOAN> taikhoans = db.TAIKHOAN.Where(e => e.TENDANGNHAP == tAIKHOAN.TENDANGNHAP && e.MATKHAU == tAIKHOAN.MATKHAU).ToList();
            if (taikhoans.Count ==1)
            {
                TAIKHOAN taiKhoan = taikhoans.First();
                if (taiKhoan.MATKHAU == tAIKHOAN.MATKHAU)
                {
                    ViewBag.SucccessMessage = "Logged";
                    Session["TEN"] = tAIKHOAN.TENDANGNHAP;
                    if (tAIKHOAN.TENDANGNHAP.Substring(0,2)=="Ad")
                    {
                        return RedirectToAction("Index", "ADMIN_QLSanPham");
                    }
                    else if (tAIKHOAN.TENDANGNHAP.Substring(0, 2) == "NV")
                    {
                        Session["NV"] = db.NHANVIEN.First(n => n.TENDANGNHAP == tAIKHOAN.TENDANGNHAP);
                        return RedirectToAction("Index", "HOADON");
                    }
                    else
                    {

                        KHACHHANG kh = db.KHACHHANG.First(t=>t.TENDANGNHAP == taiKhoan.TENDANGNHAP);
                        GIOHANG gh = kh.GIOHANG.First();
                        List<CHITIET_GIOHANG> ds_ctgh = gh.CHITIET_GIOHANG.ToList();
                        Session["KH"] = kh;
                        Session["SoSP_GH"] = ds_ctgh.Count();
                        return RedirectToAction("Index", "TRANGCHU");
                    }



                }
                else
                {
                    ViewBag.SucccessMessage = "Fail";
                    return View(new TAIKHOAN());
                }
            }
            else
            {
                ViewBag.SucccessMessage = "Fail";
                return View(new TAIKHOAN());
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "TRANGCHU");
        }
    }
}