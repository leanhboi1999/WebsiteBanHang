using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        //Get data giỏ hàng
        public List<GioHang> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (Session["GioHang"] == null)
            {
                //Không tồn tại session
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        } 

        //Thêm giỏ hàng dùng reload, không dùng ajax
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            return Json("test");
        }


        // GET: GioHang
        public ActionResult XemGioHang()
        {
            return View();
        }

        public ActionResult GioHangPartial()
        {
            return PartialView();
        }
    }
}