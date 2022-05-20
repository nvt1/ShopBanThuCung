using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NobiShop.Models;
namespace NobiShop.Controllers
{
    public class SanPhamController : Controller
    {
        dbNobiShopDataContext data = new dbNobiShopDataContext();
        // GET: SanPham
        public ActionResult Index()
        {
            return View(from sp in data.SANPHAMs select sp);
        }
        public ActionResult Details()
        {
            int masp = int.Parse(Request.QueryString["id"]);
            var result = data.SANPHAMs.Where(sp => sp.MaSP == masp).SingleOrDefault();
            return View(result);
        }
        public ActionResult Edit(int id)
        {
            SANPHAM sp = data.SANPHAMs.Where(n => n.MaSP == id).SingleOrDefault();
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SANPHAM sp)
        {
            if (ModelState.IsValid)
            {
                sp.TenSP = Request.Form["TenSP"];
                sp.MoTa = Request.Form["MoTa"];
                sp.AnhBia = Request.Form["AnhBia"];

                UpdateModel(sp);
                data.SubmitChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
 
    }
}