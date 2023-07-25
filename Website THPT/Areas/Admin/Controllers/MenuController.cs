using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Website_THPT.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var listMenu = db.MENUs.Where(m => m.ParentId == null).OrderBy(m => m.OrderNumber).ToList();
            int[] a = new int[listMenu.Count()];
            for (int i = 0; i < listMenu.Count; i++)
            {
                var l = db.MENUs.Where(m => m.ParentId == listMenu[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            List<TRANGTIN> tt = db.TRANGTINs.ToList();
            ViewBag.TrangTin = tt;
            return View(listMenu);
        }

        [ChildActionOnly]
        public ActionResult ChildMenu(int parentId)
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENUs.Where(m => m.ParentId == parentId).OrderBy(m => m.OrderNumber).ToList();
            ViewBag.Count = lst.Count();
            int[] a = new int[lst.Count()];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENUs.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView("ChildMenu", lst);
        }

        [ChildActionOnly]
        public ActionResult ChildMenu1(int parentId)
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENUs.Where(m => m.ParentId == parentId).OrderBy(m => m.OrderNumber).ToList();
            ViewBag.Count = lst.Count();
            int[] a = new int[lst.Count()];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENUs.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView("ChildMenu1", lst);
        }

        [HttpPost]
        public ActionResult AddMenu(FormCollection f)
        {
             if (!String.IsNullOrEmpty(f["ThemTrang"]))
            {
                MENU m = new MENU();
                var trang = db.TRANGTINs.Where(c => c.Ma == int.Parse(f["Ma"].ToString())).SingleOrDefault();
                m.MenuName = trang.Ten;
                m.MenuLink = trang.Metatitle;
                if (!String.IsNullOrEmpty(f["ParentId"]))
                {
                    m.ParentId = int.Parse(f["ParentId"]);
                }
                else
                {
                    m.ParentId = null;
                }
                m.OrderNumber = int.Parse(f["Number1"]);
                db.MENUs.InsertOnSubmit(m);
                db.SubmitChanges();

            }
            else if (!String.IsNullOrEmpty(f["ThemLink"]))
            {
                MENU m = new MENU();

                m.MenuName = f["TenMenu"];
                m.MenuLink = f["Link"];
                if (!String.IsNullOrEmpty(f["ParentId"]))
                {
                    m.ParentId = int.Parse(f["ParentId"]);
                }
                else
                {
                    m.ParentId = null;
                }
                m.OrderNumber = int.Parse(f["Number2"]);
                db.MENUs.InsertOnSubmit(m);
                db.SubmitChanges();
            }
            return Redirect("~/Admin/Menu/Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            List<MENU> submn = db.MENUs.Where(m => m.ParentId == id).ToList();
            if (submn.Count() > 0)
            {
                return Json(new { code = 500, msg = "Còn Menu con, không xoá được." }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var mn = db.MENUs.SingleOrDefault(m => m.Id == id);
                db.MENUs.DeleteOnSubmit(mn);
                db.SubmitChanges();
                return Json(new { code = 200, msg = "xoá thành công" }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public JsonResult Update(int id)
        {
            try
            {
                var mn = (from m in db.MENUs
                          where (m.Id == id)
                          select new
                          {
                              Id = m.Id,
                              MenuName = m.MenuName,
                              MenuLink = m.MenuLink,
                              OrderNumber = m.OrderNumber
                          }).SingleOrDefault();
                return Json(new { code = 200, mn = mn, msg = "Lấy thông tin thành công." }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Update(int id, string strTenMenu, string strLink, int STT)
        {
            try
            {
                var mn = db.MENUs.SingleOrDefault(m => m.Id == id);
                mn.MenuName = strTenMenu;
                mn.MenuLink = strLink;
                mn.OrderNumber = STT;
                db.SubmitChanges();
                return Json(new { code = 200, msg = "Sửa menu thành công" }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { code = 200, msg = "Sửa Menu thất bại. Lõi" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}