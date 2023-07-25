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
    public class QLHSController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        // GET: Admin/QLHS
        public ActionResult Index()
        {
            return View(db.HOCSINHs.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop", "TenLop");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HOCSINH hocsinh, FormCollection f)
        {
            ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop", "TenLop");
                if (ModelState.IsValid)
                {  
                    hocsinh.TenHS = f["sTenHS"];
                    hocsinh.TenDN = f["sTenDN"];
                    hocsinh.Pass = f["sPass"];
                    hocsinh.MaLop = (f["MaLop"]);

                    hocsinh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                    hocsinh.NoiSinh = (f["sNoiSinh"]);
                    hocsinh.DiaChi = (f["sDiaChi"]);
                    db.HOCSINHs.InsertOnSubmit(hocsinh);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        public ActionResult Details (int id)
        {
            var hocsinh = db.HOCSINHs.SingleOrDefault(n => n.MaHS == id);
            if (hocsinh == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(hocsinh);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop", "TenLop");
            var hocsinh = db.HOCSINHs.SingleOrDefault(n => n.MaHS == id);
            if (hocsinh == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            
            return View(hocsinh);
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            
            ViewBag.MaLop = new SelectList(db.LOPs.ToList().OrderBy(n => n.TenLop), "MaLop", "TenLop");

            if (ModelState.IsValid)
            {
                var hocsinh = db.HOCSINHs.Where(n => n.MaHS == int.Parse(f["iMaHS"])).SingleOrDefault();
                hocsinh.TenHS = f["sTenHS"];
                hocsinh.TenDN = f["sTenDN"];
                hocsinh.Pass = f["sPass"];
                hocsinh.MaLop = (f["MaLop"]);

                hocsinh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                hocsinh.NoiSinh = (f["sNoiSinh"]);
                hocsinh.DiaChi = (f["sDiaChi"]);
               
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
            var hocsinh = db.HOCSINHs.SingleOrDefault(n => n.MaHS == id);
            if (hocsinh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hocsinh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var hocsinh = db.HOCSINHs.SingleOrDefault(n => n.MaHS == id);
            if (hocsinh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
           
            db.HOCSINHs.DeleteOnSubmit(hocsinh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
        
}
