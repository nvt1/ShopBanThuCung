using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NobiShop.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace NobiShop.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang

        dbNobiShopDataContext db = new dbNobiShopDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, FormCollection f, HttpPostedFileBase fFileUpload)
        {

                if (ModelState.IsValid)
                {
                   
                    kh.HoTen = f["sHoTen"];
                    kh.TaiKhoan = f["sTaiKhoan"];
                    kh.MatKhau = f["sMatKhau"];
                    kh.Email = f["sEmail"];
                    kh.DiaChi = f["sDiaChi"];
                    kh.DienThoai = f["sDienThoai"];
                    kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                    db.KHACHHANGs.InsertOnSubmit(kh);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
               return View();
            
        }

        public ActionResult DeTails(int id)
        {
            var sp = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
            }
            return View(sp);
        }
        public ActionResult Delete(int id)
        {
            var sanpham = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sanpham = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);

            if (sanpham == null)
            {
                Response.StatusCode = 404; return null;
            }
           

            var sanxuat = db.KHACHHANGs.Where(vs => vs.MaKH == id).ToList();
            if (sanxuat != null)
            {
                db.KHACHHANGs.DeleteAllOnSubmit(sanxuat);
                db.SubmitChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sanpham = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id); if (sanpham == null)
            {
                Response.StatusCode = 404; return null;
            }

            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));


            if (ModelState.IsValid)
            {

                kh.HoTen = f["sHoTen"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["sMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["sDienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }
    }
}