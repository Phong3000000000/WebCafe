using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN_CLOUND.Models;

namespace DOAN_CLOUND.Controllers
{
    public class CartController : Controller
    {
        WebCafeDataContext db = new WebCafeDataContext();

        // GET: Cart
        public ActionResult SanPham()
        {
            var listProduct = db.PRODUCTs.ToList();
            return View(listProduct);
        }
    }
}