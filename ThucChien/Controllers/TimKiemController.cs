using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: TimKiem
        [HttpGet]
        public ActionResult KQTimKiem(String sTuKhoa, int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            //Tạo biến ghi số page hiện tại
            int PageNumber = (page ?? 1);
            //Tìm kiếm theo list sản phẩm
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSP.OrderBy(n => n.TenSP).ToPagedList(PageNumber, PageSize));
        }

        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(String sTuKhoa)
        {
            return RedirectToAction("KQTimKiem", new { @sTuKhoa = sTuKhoa });
        }

        public ActionResult KQTimKiemPartial(string sTuKhoa)
        {
            //Tìm kiếm theo list sản phẩm
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return PartialView(lstSP.OrderBy(n => n.DonGia));
        }
    }
}