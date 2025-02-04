using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class ADMIN_THONGKEController : Controller
    {
        DB_BANHANGEntities db = new DB_BANHANGEntities();
        // GET: ADMIN_THONGKE
        public ActionResult Index()
        {
            List<HOADON> listHD = db.HOADON.ToList();
            List<PHIEUDAT> listPD = db.PHIEUDAT.ToList();
            ViewBag.listPD = listPD;
            ViewBag.TongThuNhap = listHD.ToList().Sum(t => t.TONGTIEN);
            ViewBag.TongChi = listPD.ToList().Sum(t => t.TONGTIEN);

           
            return View(listHD);
        }
    }
}