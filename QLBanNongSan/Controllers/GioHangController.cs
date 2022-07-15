using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanNongSan.Models;

namespace QLBanNongSan.Controllers
{
    public class GioHangController : BaseCheckController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: GioHang
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = data.San_phams.SingleOrDefault(s => s.ma_san_pham == id);
            if(pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("Index", "GioHang");
        }

        public ActionResult Index()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                ViewBag.check = "Giỏ hàng của bạn rỗng";
                return View(cart);
            }
            CheckCart();
            return View(cart);
        }
        
        public ActionResult UpdateQuantity (FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int idSP = int.Parse(form["maSP"]);
            int qty = int.Parse(form["qty"]);
            cart.Update_Quantity(idSP, qty);
            return RedirectToAction("Index", "GioHang");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("Index", "GioHang");
        }

    }
}