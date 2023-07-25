using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;
using System.Text.RegularExpressions;

namespace Website_THPT.Controllers
{
    public class UserAdminController : Controller
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
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDn == sTenDN && n.Pass == sMatKhau);
                if (ad != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công";
                    Session["TaiKhoan"] = ad;
                    Session["HoTen"] = ad.Ten;
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
                    return RedirectToAction("Index", "Home");
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