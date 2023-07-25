using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;

namespace Website_THPT.Controllers
{
    public class XemDiemController : Controller
    {
        THPTDataContext db = new THPTDataContext();
        public ActionResult Index(string sMaHK)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            ViewBag.MaHS = Session["MaHS"];
            
            ViewBag.MaHK = sMaHK;
            List<KETQUA> ketqua = db.KETQUAs.ToList();
            List<MONHOC> monhoc = db.MONHOCs.ToList();
            List<MONHOCCHITIET> monhocct = db.MONHOCCHITIETs.ToList();
            List<HOCSINH> hocsinh = db.HOCSINHs.ToList();
            List<HOCKY> hocky = db.HOCKies.ToList();
            if (ViewBag.MaHS != null && !String.IsNullOrEmpty(sMaHK) ) { 
                var xemdiem = from mh in monhoc join mhct in monhocct on mh.MaMon equals mhct.MaMon into table1
                          from mhct in table1.ToList() join kq in ketqua on mhct.MaMonCT equals kq.MaMonCT into table2
                          from kq in table2.ToList() join hs in hocsinh on kq.MaHS equals hs.MaHS into table3
                          from hs in table3.ToList() where hs.MaHS == ViewBag.MaHS  && kq.MaHK == sMaHK
                          select new Diem
                          {
                              ketqua = kq,
                              monhocct = mhct,
                              hocsinh = hs,
                              monhoc = mh,
                              DiemTB = Math.Round(((kq.DiemHS1.GetValueOrDefault() + (kq.DiemHS2.GetValueOrDefault()*2) + (kq.DiemThi.GetValueOrDefault()*3))/6),1)
                          };

                return View(xemdiem);
            }
            return View();
        }
    }
}