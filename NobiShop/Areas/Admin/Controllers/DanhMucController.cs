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

    public class DanhMucController : Controller
    {
        dbNobiShopDataContext db = new dbNobiShopDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.LOAIs.ToList().OrderBy(n => n.MaLoai).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(LOAI loai ,FormCollection f)
        { 
                if (ModelState.IsValid)
                {
                  
                    loai.TenLoaiCho = f["sTenLoaiCho"];

                    db.LOAIs.InsertOnSubmit(loai);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
        }
        public ActionResult DeTails(int id)
        {
            var sp = db.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
            }
            return View(sp);
        }
        public ActionResult Delete(int id)
        {
            var sanpham = db.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sanpham = db.LOAIs.SingleOrDefault(n => n.MaLoai == id);

            if (sanpham == null)
            {
                Response.StatusCode = 404; return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaSP == id); if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sản phẩm này đang có trong bảng Chi tiết đặt hàng <br>" + " Nếu muốn xóa thì phải xóa hết mã sản phẩm này trong bảng Chi tiết đặt hàng";
                return View(sanpham);
            }

            var sanxuat = db.LOAIs.Where(vs => vs.MaLoai == id).ToList();
            if (sanxuat != null)
            {
                db.LOAIs.DeleteAllOnSubmit(sanxuat);
                db.SubmitChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sanpham = db.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404; 
                return null;
            }
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var sanpham = db.LOAIs.SingleOrDefault(n => n.MaLoai == int.Parse(f["iMaLoai"]));
            if (ModelState.IsValid)
            {
                sanpham.TenLoaiCho = f["sTenLoaiCho"];

                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }

    }
}