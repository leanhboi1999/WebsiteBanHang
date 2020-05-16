using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using ThucChien.Models;
using CaptchaMvc;

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

        [HttpGet]
        public ActionResult DangKy()
        {
            //ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            //ViewBag.CauHoi = new SelectList(LoadCauHoi());
            //Kiểm tra captcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Thêm thành công";
                    //Thêm khách hàng mới vào database
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();
                    return View();
                }

            }0
            ViewBag.ThongBao = "Sai mã captcha";
            return View();
        }

        [HttpGet]
        public ActionResult DangKy1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy1(ThanhVien tv)
        {
            return View();
        }

        [HttpPost]
        //Action đăng nhập
        public ActionResult DangNhap(FormCollection Form)
        {
            //Check pass và user
            string User = Form["user"].ToString();
            string Password = Form["password"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == User && n.MatKhau == Password);
            if (tv != null)
            {
                Session["taikhoan"] = tv;
                return Content("<script>Windows.location.Reload</script>");
            }
            return Content("Tài khoản hoặc mật khẩu không đúng");
        }

        public ActionResult DangXuat()
        {
            Session["taikhoan"] = null;
            return RedirectToAction("Index");
        }

        //public List<String> LoadCauHoi()
        //{
        //    List<String> lstCauHoi = new List<string>();
        //    return lstCauHoi;
        //}
    }
}