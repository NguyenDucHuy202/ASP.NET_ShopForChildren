using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class NHANVIEN_QLDONHANGController : Controller
    {
        // GET: NHANVIEN_QLDONHANG
        public ActionResult Index(string search = "", string SortColumn = "MASP", string IconClass = "bxs-up-arrow", int page = 1)
        {
            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();

            ViewBag.SoLuongSPDangBan = (int)dB_BANHANGEntities.SANPHAM.Count();
            ViewBag.SoLuongThuongHieu = (int)dB_BANHANGEntities.THUONGHIEU.Count();
            return View(dB_BANHANGEntities.SANPHAM.ToList());
        }
    }
}