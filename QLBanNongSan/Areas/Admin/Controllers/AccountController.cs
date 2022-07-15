using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanNongSan.Models;

namespace QLBanNongSan.Areas.Admin.Controllers
{
    public class AccountController : BaseAdminController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        //Dang nhap
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string tendangnhap = f["txtName"];
            string matkhau = f["txtPassword"];

            _Admin ad = data._Admins.SingleOrDefault(u => u.email == tendangnhap && u.mat_khau == matkhau || u.ten_dang_nhap == tendangnhap && u.mat_khau == matkhau);
            if (ad != null)
            {
                ViewBag.Error = "Đăng nhập thành công!";
                Session["UserAdmin"] = ad.ten_dang_nhap;
                return RedirectToAction("Index", "BaseAdmin");
            }
            else
            {
                ViewBag.Error = "Đăng nhập không thành công!";
                return View();
            }
        }

        //Dangxuat
        public ActionResult Dangxuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "BaseAdmin");
        }

        public ActionResult CaiDatTaiKhoan()
        {
            
            var ad = data._Admins.ToList();
            return View(ad);
        }

        [HttpPost]
        public ActionResult CaiDatTaiKhoan(FormCollection collection)
        {

            string pwd = collection["cfPassword"];
            _Admin ad = data._Admins.SingleOrDefault(s => s.mat_khau == pwd);
            if(ad == null)
            {
                ViewBag.Error = "Mật khẩu không chính xác!";
            }
            else
            {
                return RedirectToAction("ChinhSua");
            }
            
            return View();
        }

        // Chinh sua
        public ActionResult ChinhSua()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult ChinhSua(FormCollection f)
        {
            var ten = f["txtTen"];
            var email = f["txtEmail"];
            var matKhau = f["txtMatKhau"];
            var matKhauCF = f["txtPasswordCF"];

            var ad = data._Admins.First(m => m.ten_dang_nhap == Session["UserAdmin"].ToString());
            if (matKhau.Equals(matKhauCF))
            {
                ad.ten_dang_nhap = ten;
                ad.email = email;
                ad.mat_khau = matKhau;
                 UpdateModel(ad);

                data.SubmitChanges();

                return RedirectToAction("DangNhap");
            }
            else
            {
                ViewBag.Error = "Mật khẩu không khớp!";
                return ChinhSua();
            }
            
        }

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
