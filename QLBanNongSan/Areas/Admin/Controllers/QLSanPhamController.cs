using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanNongSan.Models;
using System.IO;

namespace QLBanNongSan.Areas.Admin.Controllers
{
    public class QLSanPhamController : BaseAdminController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
        //Them moi
        public ActionResult ThemMoi()
        {
            ViewBag.ma_nong_san = new SelectList( data.Loai_nong_sans.ToList().OrderBy(n=> n.ten_loai),"ma_nong_san","ten_loai");
            return View();
        }

       
        [HttpPost]
        public ActionResult ThemMoi(FormCollection f, San_pham sp, HttpPostedFileBase HinhAnh)
        {
            var tensp = f["TenSP"];
            
            var manongsan = f["ma_nong_san"];
            var gia = f["Gia"];
            var soluong = f["Soluong"];
            var donvi = f["DonViTinh"];

            var filename = Path.GetFileName(HinhAnh.FileName);
            var path = Path.Combine(Server.MapPath("~/Public/Images/"), filename);
            if (System.IO.File.Exists(path))
            {
                ViewBag.Error = "Hình ảnh đã tồn tại!";
            }
            else
            {
                var kiemtra = data.San_phams.FirstOrDefault(s => s.ten_san_pham == tensp);
                if (kiemtra == null)
                {
                    sp.ten_san_pham = tensp;
                    sp.hinh_anh = filename;
                    sp.ma_nong_san = int.Parse(manongsan);
                    sp.gia = decimal.Parse(gia);
                    sp.so_luong = int.Parse(soluong);
                    sp.ngay_them = DateTime.Now;
                    sp.don_vi_tinh = donvi;
                    HinhAnh.SaveAs(path);

                    data.San_phams.InsertOnSubmit(sp);
                    data.SubmitChanges();

                    return RedirectToAction("ChinhSua");
                }
                else
                {
                    ViewBag.Error = "Sản phẩm này đã tồn tại";
                    return ThemMoi();
                }
            }
            return ThemMoi();
               
        }

        // Chỉnh sửa
        public ActionResult ChinhSua()
        {
            var sp = from s in data.San_phams orderby s.ma_san_pham descending select s;
            List<San_pham> sanpham = sp.ToList();
            List<Loai_nong_san> loaiNS = data.Loai_nong_sans.ToList();
            ViewBag.loaiNS = loaiNS;
            return View(sanpham);
        }


        // GET: Admin/SanPham/Edit/5
        public ActionResult SuaSanPham(int id)
        {
            //ViewBag.ma_nong_san = new SelectList(data.Loai_nong_sans.ToList().OrderBy(n => n.ten_loai), "ma_nong_san", "ten_loai");
            List<Loai_nong_san> loai = data.Loai_nong_sans.ToList() ;
            
            ViewBag.loai = loai;

            var sp = data.San_phams.SingleOrDefault(s => s.ma_san_pham == id);
            return View(sp);
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        public ActionResult SuaSanPham(int id, FormCollection f, HttpPostedFileBase HinhAnh)
        {
            var sp = data.San_phams.First(m => m.ma_san_pham == id);

            var tensp = f["TenSP"];

            var manongsan = f["ma_nong_san"];
            var don_vi_tinh = f["don_vi_tinh"];
            var gia = f["Gia"];
            var soluong = f["Soluong"];

            if(HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Public/Images/"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Error = "Hình ảnh đã tồn tại!";
                }
                else
                {
                    HinhAnh.SaveAs(path);

                    sp.ten_san_pham = tensp;
                    sp.hinh_anh = filename;
                    sp.don_vi_tinh = don_vi_tinh;
                    sp.ma_nong_san = int.Parse(manongsan);
                    sp.gia = decimal.Parse(gia);
                    sp.so_luong = int.Parse(soluong);
                    sp.ngay_them = DateTime.Now;

                    UpdateModel(sp);
                    data.SubmitChanges();

                    return RedirectToAction("ChinhSua");
                }

            }
            else
            {
                sp.ten_san_pham = tensp;
                sp.hinh_anh = sp.hinh_anh;
                sp.don_vi_tinh = sp.don_vi_tinh;
                sp.ma_nong_san = int.Parse(manongsan);
                sp.gia = decimal.Parse(gia);
                sp.so_luong = int.Parse(soluong);
                sp.ngay_them = DateTime.Now;

                UpdateModel(sp);
                data.SubmitChanges();

                return RedirectToAction("ChinhSua");
            }
            return SuaSanPham(id, f, HinhAnh);
            
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult XoaSanPham(int id)
        {
            var sp = data.San_phams.First(c => c.ma_san_pham == id);

            string filename = sp.hinh_anh;
            var path = Path.Combine(Server.MapPath("~/Public/Images/"), filename);
            System.IO.File.Delete(path);

            data.San_phams.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("ChinhSua");
        }

    }
}
