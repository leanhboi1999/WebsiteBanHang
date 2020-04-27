using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        // GET: Home
        public ActionResult Index()
        {
            //Create ViewBag get list SanPham from database
            //List điện thoại newest
            var lstDTM = db.SanPhams.Where(n => n.Moi == 1 && n.MaLoaiSP == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.listDTM = lstDTM;

            //List Laptop newest
            var lstLT = db.SanPhams.Where(n => n.Moi == 1 && n.MaLoaiSP == 2 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.listLT = lstLT;

            //List Table newest
            var lstMTB = db.SanPhams.Where(n => n.Moi == 1 && n.MaLoaiSP == 3 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.listMTB = lstMTB;
            return View();
        }

        public ActionResult MenuPartial()
        {
            //Truy vấn lấy về 1 list sản phẩm
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
            //}
        }
    }
}