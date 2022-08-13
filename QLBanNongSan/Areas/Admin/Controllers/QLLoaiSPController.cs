using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanNongSan.Models;
using System.IO;

namespace QLBanNongSan.Areas.Admin.Controllers
{
    public class QLLoaiSPController : BaseAdminController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();

        
        public ActionResult ChinhSua()
        {
            List<Loai_nong_san> loaiNS = data.Loai_nong_sans.ToList();
            return View(loaiNS);
        }
        //Them moi
        public ActionResult ThemMoi()
        {
            return View();

        }
        [HttpPost]
        public ActionResult ThemMoi(FormCollection f, Loai_nong_san lns)
        {
            var tenloai = f["tenloai"];

            if (ModelState.IsValid)
            {
                var kiemtra = data.Loai_nong_sans.FirstOrDefault(s => s.ten_loai == tenloai);
                if (kiemtra == null)
                {
                    lns.ten_loai= tenloai;
                    data.Loai_nong_sans.InsertOnSubmit(lns);
                    data.SubmitChanges();

                    return RedirectToAction("Index", "QLLoaiSP");
                }
                else
                {
                    ViewBag.Error = "Đã tồn tại loại nông sản này";
                    return ThemMoi();
                }
            }


            return this.ThemMoi();
        }


        public ActionResult SuaLoaiNS(int id)
        {
            
            var loaiNS = data.Loai_nong_sans.SingleOrDefault(s => s.ma_nong_san == id);
            return View(loaiNS);
        }

        [HttpPost]
        public ActionResult SuaLoaiNS(int id, FormCollection f)
        {
            var loaiNS = data.Loai_nong_sans.First(m => m.ma_nong_san == id);

            loaiNS.ten_loai = f["tenloai"];
            UpdateModel(loaiNS);
            data.SubmitChanges();

            return RedirectToAction("Index", "QLLoaiSP");

        }

        public ActionResult XoaLoaiNS(int id)
        {
            var sp = data.Loai_nong_sans.First(c => c.ma_nong_san == id);

            data.Loai_nong_sans.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return Redirect("/Admin/QLLoaiSP/");
        }
    }
}