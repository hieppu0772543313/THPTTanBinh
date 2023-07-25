using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;
using System.Text.RegularExpressions;

namespace Website_THPT.Controllers
{
    public class UserController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("DangNhap");
        }
        public ActionResult UserPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (string.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                
                HOCSINH hs = db.HOCSINHs.SingleOrDefault(n => n.TenDN == sTenDN && n.Pass == sMatKhau);
                if (hs != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công";
                    Session["MaHS"] = hs.MaHS;
                    Session["TaiKhoan"] = hs;
                    Session["HoTen"] = hs.TenHS;
                    if (collection["remember"].Contains("true"))
                    {
                        Response.Cookies["TenDN"].Value = sTenDN;
                        Response.Cookies["MatKhau"].Value = sMatKhau;
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                    }
                    return RedirectToAction("Index", "School");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }

                
            }
            return View();
        }
    }
}