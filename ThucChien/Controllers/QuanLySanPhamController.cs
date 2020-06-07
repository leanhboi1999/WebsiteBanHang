using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        private QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n => n.DaXoa == false));
        }
    }
}