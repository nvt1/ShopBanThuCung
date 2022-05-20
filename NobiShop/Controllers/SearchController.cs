using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NobiShop.Models;
namespace NobiShop.Controllers
{
    public class SearchController : Controller
    {
        dbNobiShopDataContext db = new dbNobiShopDataContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string strSearch)
        {
            ViewBag.Search = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
                // var kq = db.SACHes.Where(s=>s.MaCD == int.Parse(strSearch)).OrderByDescending(s=> s.SoLuongBan);
                //var kq = from s in db.SANPHAMs where s.TenSach.Contains(strSearch) select s;
                //var kq = from s in db.SANPHAMs where (s.SoLuongBan >= 5 && s.SoLuongBan <= 10) select s;
                //var kq = from s db.SANPHAMs where s.TenSach.Contains(strSearch)  || s.MoTa.Contains(strSearch) select s;       
                var kq = from s in db.SANPHAMs orderby s.SoLuongBan where (s.SoLuongBan >= 5 && s.SoLuongBan <= 10) select s;
                //var kq = from s in db.SANPHAMs orderby s.SoLuongBan descending where s.TenSach.Contains(strSearch)  select s;               
                return View(kq);
            }
            return View();
        }
        public ActionResult Group()
        {
            var kq = from s in db.SANPHAMs group s by s.MaSP;
            //var kq = db.SACHes.GroupBy(s=>s.MaCD);

            return View(kq);
        }
        public ActionResult ThongKe()
        {
            var kq = from s in db.SANPHAMs
                     join sp in db.SANPHAMs on s.MaSP equals sp.MaSP
                     group s by s.MaSP into g
                     select new ReporInfo
                     {
                         Id = g.Key.ToString(),
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))

                     };
            return View(kq);
        }
    }
}