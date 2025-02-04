using QL_BanDoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanDoTreEm.Controllers
{
    public class AutoCompleteController : Controller
    {
        DB_BANHANGEntities db = new DB_BANHANGEntities();
        // GET: AutoComplete
        public ActionResult Index(string search="")
        {
            var products = db.SANPHAM.Where(row=>row.TENSANPHAM.Contains(search)).ToList();
            

            return View(products);
        }

    }
}