using System;
using QLBanNongSan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanNongSan.Controllers
{
    public class TaiKhoanController : BaseCheckController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return RedirectToAction("DangNhap");
        }

        //Dang Nhap
        public ActionResult DangNhap()
        {
            if(Session["ClientId"] != null)
            {
                return RedirectToAction("Index", "SanPham"); ;
            }
            CheckCart();
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {

            var email = f["email"];
            var password = f["password"];

            Khach_hang kh = data.Khach_hangs.SingleOrDefault(u => u.email == email && u.mat_khau == password );
            if (kh != null)
            {
                ViewBag.message = "Đăng nhập thành công!";
                Session["ClientId"] = kh.email;
                return RedirectToAction("Index", "SanPham");
            }
            else
            {
                ViewBag.message = "Email hoặc mật khẩu bạn nhập không đúng!";
                return View();
            }
        }

        //Dang Ky
        public ActionResult DangKy()
        {
            CheckCart();
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection f, Khach_hang kh)
        {
            var email = f["email"];
            var password = f["password"];

            var check1 = data.Khach_hangs.FirstOrDefault(s => s.email == email);

            if (check1 != null)
            {
                ViewBag.message = "Email đã tồn tại! ";
                return View();
            }
            else
            {
                kh.email = email;
                kh.mat_khau = password;

                data.Khach_hangs.InsertOnSubmit(kh);
                data.SubmitChanges();

                ViewBag.message = "Đăng ký thành công";
                return RedirectToAction("DangNhap");
            }
        }

        //Dang xuat
        public ActionResult DangXuat()
        {
            Session["ClientId"] = null;
            Session["Cart"] = null;
            return RedirectToAction("DangNhap");
        }
    }
}