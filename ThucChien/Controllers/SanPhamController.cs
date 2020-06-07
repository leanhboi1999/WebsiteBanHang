using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;
using PagedList;

namespace ThucChien.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        //Use partial view
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        //2 Partial view để hiển thị sản phẩm theo 2 cách khác nhau
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }

        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }

        //Create page ChiTietSanPham
        public ActionResult XemChiTiet(int? id)
        {
            //Check xem id có được truyền vào không
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get data from database
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false);
            if (sp == null)
            {
                //Thông báo nếu không có sản phẩm đó
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(sp);
        }

        public ActionResult SanPham(int? MaLoaiSP, int? MaNSX, int? page)
        {
            //Check tài khoản đăng nhập hay chưa
            //if (Session["taikhoan"] == null || Session["taikhoan"].ToString() == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //Get sản phẩm theo 2 tiêu chí MaLoaiSP và MaNSX
            if (MaLoaiSP == null || MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
            if (lstSP.Count() == 0)
            {
                //Thông báo nếu không có sản phẩm đó
                return HttpNotFound();
            }

            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //Thực hiện chức năng phân trang
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            //Tạo biến ghi số page hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;

            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }

        //public ActionResult SanPham1()
        //{
        //    //Cách này có thể chèn partial bằng view 
        //    var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
        //    ViewBag.listSP = lstSanPhamLTM;

        //    var lstSanPhamDT = db.SanPhams.Where(n => n.MaLoaiSP == 2);
        //    ViewBag.listDT = lstSanPhamDT;
        //    return View();
        //}

        //public ActionResult SanPham2()
        //{
        //    var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
        //    ViewBag.listSP = lstSanPhamLTM;
        //    return View();
        //}
        ////Create partial view
        //[ChildActionOnly]
        //public ActionResult SanPhamPartial()
        //{
        //    ////Cách dưới chỉ chạy khi view chính gọi partial bằng action 
        //    //var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
        //    //return PartialView(lstSanPhamLTM);
        //    return PartialView();
        //}
    }
}