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
    public class TrangTinController : Controller
    {
        // GET: Admin/TrangTin
        THPTDataContext db = new THPTDataContext();
        public ActionResult Index()
        {
            return View(db.TRANGTINs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TRANGTIN tt)
        {
            if (ModelState.IsValid)
            {
                tt.Metatitle = tt.Ten.RemoveDiacritics().Replace(" ", "-");
                tt.NgayTao = DateTime.Now;
                db.TRANGTINs.InsertOnSubmit(tt);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tt = db.TRANGTINs.Where(t => t.Ma == id);
            return View(tt.SingleOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var tt = db.TRANGTINs.Where(t => t.Ma == int.Parse(f["Ma"])).SingleOrDefault();
                tt.Ten = f["Ten"];
                tt.NoiDung = f["NoiDung"];
                tt.NgayTao = Convert.ToDateTime(f["NgayTao"]);
                tt.Metatitle = f["Ten"].RemoveDiacritics().Replace(" ", "-");
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
            var tt = from t in db.TRANGTINs where t.Ma == id select t;
            return View(tt.SingleOrDefault());

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var tt = (from t in db.TRANGTINs where t.Ma == id select t).SingleOrDefault();
            db.TRANGTINs.DeleteOnSubmit(tt);
            db.SubmitChanges();
            return RedirectToAction("Index");

        }
    }
}