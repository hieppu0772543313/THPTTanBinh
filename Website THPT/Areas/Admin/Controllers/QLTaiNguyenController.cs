﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_THPT.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Website_THPT.Areas.Admin.Controllers
{
    public class QLTaiNguyenController : Controller
    {
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }
            List<ObjFile> ObjFiles = new List<ObjFile>();
            foreach (string strfile in Directory.GetFiles(Server.MapPath("~/Files")))
            {
                FileInfo fi = new FileInfo(strfile);
                ObjFile obj = new ObjFile();

                obj.File = fi.Name;
                obj.Size = fi.Length;
                obj.Type = GetFileTypeByExtension(fi.Extension);
                ObjFiles.Add(obj);
            }

            return View(ObjFiles);
        }

        public FileResult Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Files"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "Text Document";
                case ".jpg":
                case ".png":
                    return "Image";
                default:
                    return "Unknown";
            }
        }
        [HttpPost]
        public ActionResult Index(ObjFile doc)
        {
            foreach (var file in doc.files)
            {

                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Files"), fileName);
                    file.SaveAs(filePath);
                }
            }
            TempData["Message"] = "files uploaded successfully";
            return RedirectToAction("Index");
        }

    }
}

