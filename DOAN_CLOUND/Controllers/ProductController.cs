using DOAN_CLOUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_CLOUND.Controllers
{
    public class ProductController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        WebCafeDataContext db = new WebCafeDataContext();

        // Thêm Order
        [HttpGet]
        public ActionResult Create_Product()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_Product(PRODUCT product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = db.PRODUCTs.FirstOrDefault(c => c.TENHANG == product.TENHANG);
                    if (existingProduct != null)
                    {
                        ModelState.AddModelError("", "Tên danh mục đã tồn tại. Vui lòng chọn tên khác.");
                        return View(product);
                    }
                    db.PRODUCTs.InsertOnSubmit(product); // Thêm đối tượng vào bảng PRODUCTs
                    db.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    return RedirectToAction("Index_Products", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu danh mục: " + ex.Message);
                }
            }
            return View(product);
        }

        // Xóa Order
        [HttpGet]
        public ActionResult Delete_Product(int id)
        {
            PRODUCT product = db.PRODUCTs.Single(c => c.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete_Product")]
        public ActionResult Delete_ProductConfirmed(int id)
        {
            PRODUCT product = db.PRODUCTs.Single(c => c.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.PRODUCTs.DeleteOnSubmit(product);
            db.SubmitChanges();
            return RedirectToAction("Index_Products", "Home");
        }

        // Sửa Order
        [HttpGet]
        public ActionResult Update_Product(int id)
        {
            PRODUCT product = db.PRODUCTs.Single(c => c.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Update_Product(PRODUCT updatedProduct)
        {
            PRODUCT product = db.PRODUCTs.Single(c => c.ID == updatedProduct.ID);
            if (product == null)
            {
                return HttpNotFound();
            }

            product.TENHANG = updatedProduct.TENHANG;
            product.DVT = updatedProduct.DVT;
            product.CATE_ID = updatedProduct.CATE_ID;
            product.HINHANH = updatedProduct.HINHANH;
            product.GIA = updatedProduct.GIA;
            db.SubmitChanges(); // Lưu lại thay đổi (này là với linQ nha)
            return RedirectToAction("Index_Products", "Home");
        }

        public ActionResult Search(string name)
        {
            List<PRODUCT> item = db.PRODUCTs.Where(sp => sp.TENHANG.Contains(name)).ToList();
            return View(item);
        }
    }
}