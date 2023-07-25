using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;
using PagedList;
using PagedList.Mvc;

namespace Website_THPT.Controllers
{
    public class SchoolController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: School
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult NavPartial()
        {
            List<MENU> lst = new List<MENU>();
            lst = db.MENUs.Where(m => m.ParentId == null).OrderBy(m => m.OrderNumber).ToList();
            int[] a = new int[lst.Count()];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.MENUs.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView(lst);
        }
        [ChildActionOnly]
        public ActionResult LoadChildMenu(int parentId)
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
            return PartialView("LoadChildMenuPartial", lst);
        }

        public ActionResult SliderPartial()
        {
            return PartialView();
        }

        public ActionResult ThongBaoPartial()
        {
            var listThongBao = (from tb in db.THONGBAOs select tb).Take(3);
            return PartialView(listThongBao);
        }
        public ActionResult DsThongBao()
        {
            var listDsThongBao = from dstb in db.THONGBAOs select dstb;
            return View(listDsThongBao);
        }
        public ActionResult ThongBao(int ma)
        {
            var tb = (from t in db.THONGBAOs where t.Ma == ma select t).Single();
            return View(tb);
        }



        public ActionResult HoatDongPartial()
        {
            var listHoatDong = (from hd in db.TINTUCs select hd).Take(3);
            return PartialView(listHoatDong);
        }
        public ActionResult DsHoatDong()
        {
            var listDsHoatDong = from dshd in db.TINTUCs select dshd;
            return View(listDsHoatDong);
        }
        public ActionResult HoatDong(int ma)
        {
            var hd = (from t in db.TINTUCs where t.Ma == ma select t).Single();
            return View(hd);
        }

        public ActionResult FooterPartial()
        {
            return PartialView();
        }

        public ActionResult TrangTin(string metatitle)
        {
            var tt = (from t in db.TRANGTINs where t.Metatitle == metatitle select t).Single();
            return View(tt);
        }

        
    }
}