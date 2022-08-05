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
        public ActionResult LichSu()
        {
            if (Session["ClientId"] == null)
            {
                return RedirectToAction("DangNhap"); ;
            }
            CheckCart();

            var email = Session["ClientId"].ToString();
            Khach_hang kh = data.Khach_hangs.SingleOrDefault(u => u.email == email);
            List<Hoa_don> donHang = 
                (from s in data.Hoa_dons where s.ma_khach_hang == kh.ma_khach_hang orderby s.ma_hoa_don descending select s).ToList();
            List<CT_hoa_don> cthd = 
                (from s in data.CT_hoa_dons select s).ToList();
            List<San_pham> sp =
                (from s in data.San_phams select s).ToList();
            ViewBag.cthd = cthd;
            ViewBag.khachHang = kh;
            ViewBag.sp = sp;
            return View(donHang);
        }
        //Cancel order
        public ActionResult HuyDonHang(int idOd, String feedback)
        {
            var hoaDon = data.Hoa_dons.First(m => m.ma_hoa_don == idOd);
            hoaDon.trang_thai = "canceled";
            hoaDon.nhan_xet = feedback;
            UpdateModel(hoaDon);
            data.SubmitChanges();
            return RedirectToAction("LichSu");
        }
    }
}