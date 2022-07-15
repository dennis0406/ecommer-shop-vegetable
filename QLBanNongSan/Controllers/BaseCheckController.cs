using System;
using System.Collections.Generic;
using QLBanNongSan.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanNongSan.Controllers
{
    public class BaseCheckController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: BaseCheck
        public decimal phiVanChuyen = 20000;

        public ActionResult CheckCart()
        {
            List<Loai_nong_san> loaiNS = data.Loai_nong_sans.ToList();
            ViewBag.loaiNS = loaiNS;
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                ViewBag.cart = 0;
            }
            else
            {
                ViewBag.cart = cart.Return_Quantity();
            }
            return View();
        }

    }
}