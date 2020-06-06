using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        //Get data giỏ hàng
        public List<ItemGioHang> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (Session["GioHang"] == null)
            {
                //Không tồn tại session
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }

            return lstGioHang;
        }

        //Thêm giỏ hàng dùng reload, không dùng ajax
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Tạo list giỏ hàng (thực ra là lấy giỏ hàng ra ngoài)
            List<ItemGioHang> lstGioHang = LayGioHang();
            //Trường hợp sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (spCheck.SoLuong > sp.SoLuongTon)
                {
                    return View("ThongBao");
                }

                //Tăng số lượng và tính lại giá trị tiền
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }

            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (itemGH.SoLuong > sp.SoLuongTon)
            {
                return View("ThongBao");
            }

            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }

        public double TinhTongSoLuong()
        {
            //Lấy giỏ hàng ra ngoài
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }

            return lstGioHang.Sum(n => n.SoLuong);

        }

        //Tính tổng tiền của giỏ hàng
        public decimal TinhTongTien()
        {
            //Lấy giỏ hàng ra ngoài
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }

            return lstGioHang.Sum(n => n.ThanhTien);
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGH = LayGioHang();

            return View(lstGH);
        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }

            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return PartialView();
        }

        //Chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang(int MaSP)
        {
            //KIểm tra session GioHang tồn tại hay không
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Kiểm tra sản phẩm có hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGH = LayGioHang();
            ItemGioHang spCheck = lstGH.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Lấy list giỏ hàng tạo giao diện
            ViewBag.GioHang = lstGH;
            //Nếu tồn tại
            return View(spCheck);
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            //Kiểm tra số lượng tồn
            SanPham spCheck = db.SanPhams.SingleOrDefault(n => n.MaSP == itemGH.MaSP);
            if (spCheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }

            //Lấy list giỏ hàng trong session
            List<ItemGioHang> lstGH = LayGioHang();
            //Lấy sản phẩm cẩn cập nhập từ lstGH, lưu thành 1 object ItemGioHang mới
            ItemGioHang itemSPUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            //Update lại số lương sản phẩm
            itemSPUpdate.SoLuong = itemGH.SoLuong;
            //Update lại giá sản phẩm
            itemSPUpdate.ThanhTien = itemSPUpdate.SoLuong * itemSPUpdate.DonGia;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Kiểm tra sản phẩm có hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGH = LayGioHang();
            ItemGioHang spCheck = lstGH.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Xóa sản phẩm nếu nó đã tồn tại
            lstGH.Remove(spCheck);
            return RedirectToAction("XemGioHang");
        }

        //Chức năng đặt hàng
        public ActionResult DatHang(KhachHang kh)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            KhachHang KhachHangs = new KhachHang();
            if (Session["TaiKhoan"] == null)
            {
                //Thêm khách hàng vào csdl bằng biến kh
                KhachHangs = kh;
                db.KhachHangs.Add(KhachHangs);
                db.SaveChanges();
            }
            else
            {
                //Đối với khách hàng là thành viên
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                KhachHangs.TenKH = tv.HoTen;
                KhachHangs.DiaChi = tv.DiaChi;
                KhachHangs.Email = tv.Email;
                KhachHangs.SoDienThoai = tv.SoDienThoai;
                KhachHangs.MaThanhVien = tv.MaThanhVien;
                db.KhachHangs.Add(KhachHangs);
                db.SaveChanges();
            }

            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            //Add khách hàng thành công, lấy mã khách hàng từ csdl
            ddh.MaKH = KhachHangs.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            foreach (var item in lstGioHang)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);

            }

            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult ThemGioHangAjax(int MaSP, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại hay không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //Tạo list giỏ hàng (thực ra là lấy giỏ hàng ra ngoài)
            List<ItemGioHang> lstGioHang = LayGioHang();
            //Trường hợp sản phẩm đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                if (spCheck.SoLuong > sp.SoLuongTon)
                {
                    return Content("<script>alert(\"Sản phẩm đã hết hàng\")</script>");
                }

                //Tăng số lượng và tính lại giá trị tiền
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TinhTongTien();
                return PartialView("GioHangPartial");
            }

            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (itemGH.SoLuong > sp.SoLuongTon)
            {
                return Content("<script>alert(\"Sản phẩm đã hết hàng\")</script>");
            }

            lstGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView("GioHangPartial");

        }
    }

}