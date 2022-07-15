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

        // GET: Admin/HoaDon/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/HoaDon/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/HoaDon/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/HoaDon/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/HoaDon/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/HoaDon/Delete/5
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
