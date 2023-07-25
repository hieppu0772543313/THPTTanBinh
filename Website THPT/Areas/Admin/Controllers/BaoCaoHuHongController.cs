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
    public class BaoCaoHuHongController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/BaoCaoHuHong
        public ActionResult Index()
        {
            List<BAOCAOHUHONG> baocaohuhong = db.BAOCAOHUHONGs.ToList();
            List<COSOVATCHAT> cosovatchat = db.COSOVATCHATs.ToList();
            List<LOP> lop = db.LOPs.ToList();
            var baocao = from bc in baocaohuhong
                         join cs in cosovatchat on bc.MaThietBi equals cs.MaThietBi
                         join l in lop on bc.MaLop equals l.MaLop
                         select new ThongKeBaoCao
                         {
                             baocaohuhong = bc,
                             cosovatchat = cs,
                             lop =l
                         };
            return View(baocao);
        }




        public ActionResult Edit(int id)
        {


            var bc = db.BAOCAOHUHONGs.SingleOrDefault(n => n.MaBaoCao == id);
            if (bc == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            ViewBag.MaThietBi = new SelectList(db.COSOVATCHATs.ToList().OrderBy(n => n.TenThietBi), "MaThietBi", "TenThietBi");
            ViewBag.TenLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop", "TenLop");
            return View(bc);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {


            if (ModelState.IsValid)
            {
                var bc = db.BAOCAOHUHONGs.Where(n => n.MaBaoCao == int.Parse(f["iMaBaoCao"])).SingleOrDefault();
                ViewBag.MaThietBi = new SelectList(db.COSOVATCHATs.ToList().OrderBy(n => n.TenThietBi), "MaThietBi", "TenThietBi");
                ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop", "TenLop");
                bc.MaThietBi = int.Parse(f["MaThietBi"]);
                bc.TinhTrang = f["TinhTrang"];
                bc.SoLuong = int.Parse(f["iSoLuong"]);
                //bc.MaLop = f["iMaLop"];
                bc.Status = f["TrangThai"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var bc = db.BAOCAOHUHONGs.SingleOrDefault(n => n.MaBaoCao == id);
            if (bc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(bc);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var bc = db.BAOCAOHUHONGs.SingleOrDefault(n => n.MaBaoCao == id);
            if (bc == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            db.BAOCAOHUHONGs.DeleteOnSubmit(bc);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}