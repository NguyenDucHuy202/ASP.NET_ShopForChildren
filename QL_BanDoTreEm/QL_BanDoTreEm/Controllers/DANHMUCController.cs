using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class DANHMUCController : Controller
    {
        DB_BANHANGEntities db  = new DB_BANHANGEntities();
        // GET: DANHMUC
        public ActionResult PartialDanhMuc()
        {
            var list = db.DANHMUCSANPHAM.OrderBy(n=> n.TENDANHMUCSP).ToList();
            return View(list);
        }
    }
}