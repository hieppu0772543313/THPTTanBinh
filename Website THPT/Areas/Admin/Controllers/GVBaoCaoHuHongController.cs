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
    public class GVBaoCaoHuHongController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/BaoCaoHuHong
        public ActionResult Index()
        {
            return View(db.BAOCAOHUHONGs.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaThietBi = new SelectList(db.COSOVATCHATs.ToList().OrderBy(n => n.TenThietBi), "MaThietBi", "TenThietBi");
            ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop","TenLop");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BAOCAOHUHONG bc, FormCollection f)
        {
            ViewBag.MaThietBi = new SelectList(db.COSOVATCHATs.ToList().OrderBy(n => n.TenThietBi),"MaThietBi", "TenThietBi");
            ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop","TenLop");
            if (ModelState.IsValid)
            {
                bc.MaThietBi = int.Parse(f["MaThietBi"]);
                bc.TinhTrang =  f["TinhTrang"];
                bc.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop),"MaLop", "TenLop", f["MaLop"]);
                bc.Status = f["TrangThai"];
                db.BAOCAOHUHONGs.InsertOnSubmit(bc);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        
    }
}