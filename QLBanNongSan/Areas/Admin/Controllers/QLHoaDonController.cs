using System;
using QLBanNongSan.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBanNongSan.Areas.Admin.Controllers
{
    public class QLHoaDonController : BaseAdminController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Admin/HoaDon
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangXuLy()
        {
            List<Hoa_don> donHang = (from s in data.Hoa_dons where s.trang_thai == "processing" select s).ToList();
            List<Khach_hang> kh = data.Khach_hangs.ToList();
            ViewBag.khachHang = kh;
            return View(donHang);
        }

        public ActionResult HoanThanh()
        {
            List<Hoa_don> donHang = (from s in data.Hoa_dons where s.trang_thai == "completed" select s).ToList();
            List<Khach_hang> kh = data.Khach_hangs.ToList();
            ViewBag.khachHang = kh;
            return View(donHang);
        }

        public ActionResult DangGiao()
        {
            List<Hoa_don> donHang = (from s in data.Hoa_dons where s.trang_thai == "shipping" select s).ToList();
            List<Khach_hang> kh = data.Khach_hangs.ToList();
            ViewBag.khachHang = kh;
            return View(donHang);
        }
        // Change status order 
        public ActionResult DoiTrangThai(int id, String status, String redirect)
        {
            var hoaDon = data.Hoa_dons.First(m => m.ma_hoa_don == id);
            hoaDon.trang_thai = status;
            UpdateModel(hoaDon);
            data.SubmitChanges();

            return RedirectToAction(redirect);  
        }
        
    }
}
