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
    
    public class SanPhamController : Controller
    {
        dbNobiShopDataContext db = new dbNobiShopDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize)); 
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaDV = new SelectList(db.DICHVUs.ToList().OrderBy(n => n.TenDV), "MaDV", "TenDV");
            ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoaiCho), "MaLoai", "TenLoaiCho");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sanpham, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaDV = new SelectList(db.DICHVUs.ToList().OrderBy(n => n.TenDV), "MaDV", "TenDV");
            ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoaiCho), "MaLoai", "TenLoaiCho");


            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaDV = new SelectList(db.DICHVUs.ToList().OrderBy(n => n.TenDV), "MaDV", "TenDV", int.Parse(f["MaDS"]));
                ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoaiCho), "MaLoai", "TenLoaiCho", int.Parse(f["MaLoai"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sanpham.TenSP = f["sTenSP"];
                    sanpham.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    sanpham.AnhBia = sFileName;
                    sanpham.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sanpham.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sanpham.GiaBan = decimal.Parse(f["mGiaBan"]);
                    sanpham.MaDV = int.Parse(f["MaDV"]);
                    sanpham.MaLoai = int.Parse(f["MaLoai"]);
                    db.SANPHAMs.InsertOnSubmit(sanpham);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public ActionResult DeTails(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
            }
            return View(sp);
        }
        public ActionResult Delete(int id)
        {
            var sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);

            if (sanpham == null)
            {
                Response.StatusCode = 404; return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaSP == id); if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sản phẩm  này đang có trong bảng Chi tiết đặt hàng <br>" + " Nếu muốn xóa thì phải xóa hết mã sản phẩm  này trong bảng Chi tiết đặt hàng";
                return View(sanpham);
            }

            var sanxuat = db.SANPHAMs.Where(vs => vs.MaSP == id).ToList();
            if (sanxuat != null)
            {
                db.SANPHAMs.DeleteAllOnSubmit(sanxuat);
                db.SubmitChanges();
            }
            return RedirectToAction("Index");
        }

 
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id); if (sanpham == null)
            {
                Response.StatusCode = 404; return null;
            }
            ViewBag.MaDV = new SelectList(db.DICHVUs.ToList().OrderBy(n => n.TenDV), "MaDV", "TenDV", sanpham.MaDV);
            ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoaiCho), "MaLoai", "TenLoaiCho", sanpham.MaLoai);
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"])); 
            ViewBag.MaDV = new SelectList(db.DICHVUs.ToList().OrderBy(n => n.TenDV),"MaDV", "TenDV", sanpham.MaDV);
            ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoaiCho), "MaLoai", "TenLoaiCho", sanpham.MaLoai);

            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sanpham.AnhBia = sFileName;
                }
                sanpham.TenSP = f["sTenSP"];
                sanpham.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                sanpham.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sanpham.SoLuongBan = int.Parse(f["iSoLuong"]);
                sanpham.GiaBan = decimal.Parse(f["mGiaBan"]);
                sanpham.MaDV = int.Parse(f["MaDV"]);
                sanpham.MaLoai = int.Parse(f["MaLoai"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }

    }
}