using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NobiShop.Models;

using PagedList;
using PagedList.Mvc;

namespace NobiShop.Controllers
{
    public class NobiShopController : Controller
    {
        dbNobiShopDataContext data = new dbNobiShopDataContext();
        // GET: NobiShop
        private List<SANPHAM> LaySanPham (int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }
        private List<SANPHAM> PhuKien(int max)
        {
            return data.SANPHAMs.OrderByDescending(a => a.MaSP).Take(max).ToList();
        }
        public ActionResult Index()
        {
            var listSanPhamBanChay = LaySanPham(8);
            return View(listSanPhamBanChay);
        }
        public ActionResult SliderPartial()
        {
            return PartialView();
        }
        public ActionResult ThuCungPartial()
        {
            var listThuCung = from tc in data.LOAIs select tc;
            return PartialView(listThuCung);
        }
        public ActionResult DichVuPartial()
        {
            var listDichVu = from dv in data.DICHVUs select dv;
            return PartialView(listDichVu);
        }
        public ActionResult PhuKienPartial()
        {
            var phukienmoi = PhuKien(4);
            return PartialView(phukienmoi);

        }
        public ActionResult NavbarPartial()
        {
            var listThuCung = from tc in data.LOAIs select tc;
            return PartialView(listThuCung);
        }
        public ActionResult LoaiThuCung(int id,int ? page)
        {
            ViewBag.MaLoai = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sanpham = from s in data.SANPHAMs where s.MaLoai == id select s;
            return View(sanpham.ToPagedList(iPageNum,iSize));
        }
        public ActionResult LoaiDichVu(int id )
        {
            var listdichvu = from s in data.SANPHAMs where s.MaDV == id select s;
            return View(listdichvu);
        }
        public ActionResult ChiTietSanPham(int id)
        {
            var sanpham = from s in data.SANPHAMs where s.MaSP == id select s;
            return View(sanpham.Single());
        }

    }
}