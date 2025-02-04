using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class ADMIN_QLNhapHangController : Controller
    {
        // GET: ADMIN_QLNhapHang
        public ActionResult Index()
        {
            DB_BANHANGEntities db = new DB_BANHANGEntities();
            List<PHIEUDAT> pd = db.PHIEUDAT.ToList();
            return View(pd);
        }

        

    }
}