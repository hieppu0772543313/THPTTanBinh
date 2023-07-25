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
    public class QL_ADMINController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/QL_ADMIN
        public ActionResult Index()
        {
            return View(db.ADMINs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ADMIN ad, FormCollection f)
        {

            if (ModelState.IsValid)
            {
               
                ad.Ten = f["sTen"];
                ad.TenDn = f["sTenDN"];
                ad.Pass = f["sPass"];
                db.ADMINs.InsertOnSubmit(ad);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var ad = db.ADMINs.SingleOrDefault(n => n.ID == id);
            if (ad == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(ad);
        }

        public ActionResult Edit(int id)
        {
            var ad = db.ADMINs.Where(n => n.ID == id);
            if (ad == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }

            return View(ad.SingleOrDefault());

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {



            if (ModelState.IsValid)
            {
                var ad = db.ADMINs.Where(n => n.ID == int.Parse(f["iID"])).SingleOrDefault();
                //gv.MaGV = f["sMaGV"];
                ad.Ten = f["Ten"];
                ad.TenDn = f["TenDn"];
                ad.Pass = f["Pass"];
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
            var ad = db.ADMINs.SingleOrDefault(n => n.ID == id);
            if (ad == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ad);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var ad = db.ADMINs.SingleOrDefault(n => n.ID == id);
            if (ad == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            db.ADMINs.DeleteOnSubmit(ad);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }

}