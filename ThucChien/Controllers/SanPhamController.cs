using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        //Use partial view
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult SanPham1()
        {
            //Cách này có thể chèn partial bằng view 
            var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            ViewBag.listSP = lstSanPhamLTM;

            var lstSanPhamDT = db.SanPhams.Where(n => n.MaLoaiSP == 2);
            ViewBag.listDT = lstSanPhamDT;
            return View();
        }

        public ActionResult SanPham2()
        {
            var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            ViewBag.listSP = lstSanPhamLTM;
            return View();
        }
        //Create partial view
        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            ////Cách dưới chỉ chạy khi view chính gọi partial bằng action 
            //var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            //return PartialView(lstSanPhamLTM);
            return PartialView();
        }
    }
}