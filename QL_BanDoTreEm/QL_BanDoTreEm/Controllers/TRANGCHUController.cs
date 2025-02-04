using PagedList;
using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class TRANGCHUController : Controller
    {
        // GET: TRANGCHU
        DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
        public ActionResult Index(int? Trang)
        {
            if (Trang == null)
                Trang = 1;
            int SoSP = 8;
            int SoTrang = (Trang ?? 1);

            return View(dB_BANHANGEntities.SANPHAM.ToList().ToPagedList(SoTrang, SoSP));

        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = dB_BANHANGEntities.SANPHAM.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        public ActionResult LienHe()
        {
            return View();

        }
    }
}