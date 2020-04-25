using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucChien.Models;

namespace ThucChien.Controllers
{
    public class DemoAjaxController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: DemoAjax
        public ActionResult DemoAjax()
        {
            return View();
        }

        //Xử lý ajax
        public ActionResult LoadAjaxActionLink()
        {
            System.Threading.Thread.Sleep(2000);
            return Content("Hello Ajax");
        }

        //Xử lý ajax bằng BeginForm
        public ActionResult LoadAjaxBeginForm(FormCollection form)
        {
            string KQ = form["txt1"].ToString();
            return Content(KQ);
        }

        //Xử lý ajax theo Jquery
        public ActionResult LoadAjaxJquery(int a, int b)
        {
            System.Threading.Thread.Sleep(2000);
            return Content((a + b).ToString());
        }

        //Use ajax load partial view
        public ActionResult LoadSanPham()
        {
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
    }
}