using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductsController : Controller
    {
        storeEntities db = new storeEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult getImage(string fileName)
        {
            return File("~/ProductImages/" + fileName, "image/jpeg");
        }

        public ActionResult getImageByte(int id)
        {
            Products products = db.Products.Find(id);
            byte[] img = products.BytesImage;
            return File(img, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.datas = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products productForm, HttpPostedFileBase ProductImage)
        {
            string message = "檔案新增成功";
            if (ProductImage != null)
            {
                string strPath = Request.PhysicalApplicationPath + @"ProductImages\" + ProductImage.FileName;
                ProductImage.SaveAs(strPath);

                var imgSize = ProductImage.ContentLength;
                byte[] imgByte = new byte[imgSize];
                ProductImage.InputStream.Read(imgByte, 0, imgSize);
                productForm.BytesImage = imgByte;
                productForm.ProductImage = ProductImage.FileName;
                db.Products.Add(productForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                message = "請加入圖片";
            }
            ViewBag.datas = db.Categories.ToList();
            ViewBag.message = message;
            return View();
        }

        [HttpGet]
        public ActionResult Update(int id = 0)
        {
            ViewBag.datas = db.Categories.ToList();
            return View(db.Products.Find(id));
        }

        [HttpPost]
        public ActionResult Update(Products productForm, HttpPostedFileBase _ProductImage, string ProductImage)
        {
            string message = "檔案修改成功";
            if (_ProductImage != null)
            {
                string strPath = Request.PhysicalApplicationPath + @"ProductImages\" + _ProductImage.FileName;
                _ProductImage.SaveAs(strPath);

                var imgSize = _ProductImage.ContentLength;
                byte[] imgByte = new byte[imgSize];
                _ProductImage.InputStream.Read(imgByte, 0, imgSize);
                productForm.BytesImage = imgByte;
                productForm.ProductImage = _ProductImage.FileName;

            }
            db.Entry(productForm).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            ViewBag.datas = db.Categories.ToList();
            ViewBag.message = message;

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id = 0)
        {
            db.Products.Remove(db.Products.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}