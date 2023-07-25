using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;
using PagedList;
using PagedList.Mvc;


namespace Website_THPT.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult index1()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Nav_Partial()
        {
            return PartialView();
        }
        public ActionResult NavLeft_Partial()
        {
            return PartialView();
        }
        public ActionResult NavLeft_GV_Partial()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
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
                GIAOVIEN gv = db.GIAOVIENs.SingleOrDefault(m => m.TenDN == sTenDN && m.Pass == sMatKhau);
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
                if (gv != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công";
                    Session["TaiKhoan"] = gv;
                    Session["HoTen"] = gv.TenGV;
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
                    return RedirectToAction("Index1", "Home");
                }
               
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
                
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("Login");
        }
    }
}