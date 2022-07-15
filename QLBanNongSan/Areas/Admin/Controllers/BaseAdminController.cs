using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanNongSan.Models;

namespace QLBanNongSan.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Admin/BaseAdmin
        public ActionResult Index()
        {
            
            if (System.Web.HttpContext.Current.Session["UserAdmin"]==null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Account/DangNhap");
            }

            ViewBag.slSP = data.San_phams.ToList().Count();
            ViewBag.slHD = data.Hoa_dons.ToList().Count();
            ViewBag.slKH = data.Khach_hangs.ToList().Count();
            ViewBag.slLNS = data.Loai_nong_sans.ToList().Count();

            var kh = data.Khach_hangs.ToList();


            return View(kh);
        }
    }
}