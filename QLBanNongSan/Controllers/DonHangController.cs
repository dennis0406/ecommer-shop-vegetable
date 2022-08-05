using System;
using QLBanNongSan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanNongSan.Controllers
{
    public class DonHangController : BaseCheckController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: DonHang
        public ActionResult Index()
        {
            //Check if cart is empty
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                return RedirectToAction("Index", "SanPham");
            }

            //Check if client didn't loging in 
            var emailKH = Session["ClientId"];
            if (emailKH == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                var kh = from s in data.Khach_hangs where s.email == emailKH select s;
                ViewBag.kh = kh.Single();
            }

            CheckCart();
            return View(cart);
        }

        [HttpPost]
        public ActionResult Index(FormCollection f, Cart cart)
        {
            return View();
        }

        [HttpPost]
        public ActionResult XacNhan(FormCollection f, Hoa_don hd)
        {
            var email = Session["ClientId"];
            Cart cart = Session["Cart"] as Cart;
            var kh = data.Khach_hangs.First(m => m.email == email);

            try
            {
                //Update client info
                kh.ten_khach_hang = f["tenKH"];
                kh.dia_chi = f["diaChi"];
                kh.so_dien_thoai = f["sdt"];
                UpdateModel(kh);
                data.SubmitChanges();

                //add new order
                var dateCf = DateTime.Now;
                hd.ngay_dat_hang = dateCf;
                hd.ma_khach_hang = kh.ma_khach_hang;
                decimal total = (decimal)cart.Total();
                hd.tong = total + phiVanChuyen;
                hd.trang_thai = "processing";
                hd.ghi_chu = f["ghiChu"];
                hd.phi_van_chuyen = phiVanChuyen;
                data.Hoa_dons.InsertOnSubmit(hd);
                data.SubmitChanges();

                //Add new order detail

                foreach (var item in cart.Items)
                {
                    CT_hoa_don cthd = new CT_hoa_don();
                    var hdf = data.Hoa_dons.First(m => m.ngay_dat_hang == dateCf);
                    cthd.ma_hoa_don = hdf.ma_hoa_don;
                    cthd.ma_san_pham = item.shoppingProduct.ma_san_pham;
                    cthd.so_luong = item.shopping_quantity;
                    cthd.thanh_tien = item.shoppingProduct.gia * item.shopping_quantity;
                    data.CT_hoa_dons.InsertOnSubmit(cthd);

                    //Change the quantity of the product
                    var spf = data.San_phams.First(m => m.ma_san_pham == item.shoppingProduct.ma_san_pham);
                    spf.so_luong -= item.shopping_quantity;
                    UpdateModel(spf);

                }
                data.SubmitChanges();
                Session["Cart"] = null;
                Session["dateCf"] = dateCf;
                return RedirectToAction("HoanTat");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult HoanTat()
        {
            var emailKH = Session["ClientId"];
            var dateCf = Session["dateCf"];
            if (emailKH == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            if(dateCf == null)
            {
                return RedirectToAction("Index", "SanPham");
            }

            DateTime ? datef = (DateTime?)dateCf;

            ViewBag.shippingFee = phiVanChuyen;
            Hoa_don hd = data.Hoa_dons.SingleOrDefault(s => s.ngay_dat_hang == datef);
            CheckCart();
            return View(hd);
        }
    }
}