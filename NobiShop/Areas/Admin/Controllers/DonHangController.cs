using System.Linq;
using System.Web;
using System.Web.Mvc;
using NobiShop.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace NobiShop.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {

        dbNobiShopDataContext db = new dbNobiShopDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }
    }
}