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
    public class QLGVController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/QLGV
        public ActionResult Index()
        {
            return View(db.GIAOVIENs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GIAOVIEN gv, FormCollection f)
        {
            
            if (ModelState.IsValid)
            {
                gv.MaGV = f["sMaGV"];
                gv.TenGV = f["sTenGV"];
                gv.TenDN = f["sTenDN"];
                gv.Pass = f["sPass"];
                gv.DiaChi = (f["sDiaChi"]);
                gv.Sdt = (f["sSdt"]);
                gv.Email = f["sEmail"];
                gv.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                db.GIAOVIENs.InsertOnSubmit(gv);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(string id)
        {
            var gv = db.GIAOVIENs.SingleOrDefault(n => n.MaGV == id);
            if (gv == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(gv);
        }

        public ActionResult Edit(string id)
        {
            var gv = db.GIAOVIENs.SingleOrDefault(n => n.MaGV == id);
            if (gv == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
           
            return View(gv);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {



            if (ModelState.IsValid)
            {
                var gv = db.GIAOVIENs.Where(n => n.MaGV == (f["iMaGV"])).SingleOrDefault();
                //gv.MaGV = f["sMaGV"];
                gv.TenGV = f["TenGV"];
                gv.TenDN = f["TenDN"];
                gv.Pass = f["Pass"];
                gv.DiaChi = (f["DiaChi"]);
                gv.Sdt = (f["Sdt"]);
                gv.Email = f["Email"];
                gv.NgaySinh = Convert.ToDateTime(f["NgaySinh"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }

        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var gv = db.GIAOVIENs.SingleOrDefault(n => n.MaGV == id);
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gv);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(string id, FormCollection f)
        {
            var gv = db.GIAOVIENs.SingleOrDefault(n => n.MaGV == id);
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            db.GIAOVIENs.DeleteOnSubmit(gv);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}