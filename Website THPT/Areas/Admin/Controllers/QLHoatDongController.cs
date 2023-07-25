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
    public class QLHoatDongController : Controller
    {
        THPTDataContext db = new THPTDataContext();

        public ActionResult Index()
        {
            return View(db.TINTUCs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TINTUC tt)
        {
            if (ModelState.IsValid)
            {

                tt.NgayTao = DateTime.Now;
                db.TINTUCs.InsertOnSubmit(tt);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tt = db.TINTUCs.Where(t => t.Ma == id);
            return View(tt.SingleOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var tt = db.TINTUCs.Where(t => t.Ma == int.Parse(f["Ma"])).SingleOrDefault();
                tt.TenTin = f["TenTin"];
                tt.NoiDung = f["NoiDung"];
                tt.NgayTao = Convert.ToDateTime(f["NgayTao"]);
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
            var tt = from t in db.TINTUCs where t.Ma == id select t;
            return View(tt.SingleOrDefault());

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var tt = (from t in db.TINTUCs where t.Ma == id select t).SingleOrDefault();
            db.TINTUCs.DeleteOnSubmit(tt);
            db.SubmitChanges();
            return RedirectToAction("Index");

        }
    }
}
