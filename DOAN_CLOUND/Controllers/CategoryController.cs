using DOAN_CLOUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_CLOUND.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        WebCafeDataContext db = new WebCafeDataContext();   

        // Thêm category
        [HttpGet] 
        public ActionResult Create_Category()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_Category(CATEGORy category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = db.CATEGORies.FirstOrDefault(c => c.NAME ==  category.NAME);
                    if (existingCategory != null)
                    {
                        ModelState.AddModelError("", "Tên danh mục đã tồn tại. Vui lòng chọn tên khác.");
                        return View(category);
                    }
                    db.CATEGORies.InsertOnSubmit(category); // Thêm đối tượng vào bảng CATEGORIES
                    db.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    return RedirectToAction("Index_Category", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu danh mục: " + ex.Message);
                }
            }
            return View(category);
        }

        // Xóa category
        [HttpGet]
        public ActionResult Delete_Category(int id)
        {
            CATEGORy category = db.CATEGORies.Single(c => c.ID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete_Category")]
        public ActionResult Delete_CategoryConfirmed(int id)
        {
            CATEGORy category = db.CATEGORies.Single(c => c.ID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.CATEGORies.DeleteOnSubmit(category);
            db.SubmitChanges();
            return RedirectToAction("Index_Category", "Home");
        }

        // Sửa category
        [HttpGet]
        public ActionResult Update_Category(int id)
        {
            CATEGORy category = db.CATEGORies.Single(c => c.ID == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Update_Category(CATEGORy updatedCategory)
        {
            CATEGORy category = db.CATEGORies.Single(c => c.ID == updatedCategory.ID);
            if (category == null)
            {
                return HttpNotFound();
            }

            category.NAME = updatedCategory.NAME;
            db.SubmitChanges(); // Lưu lại thay đổi (này là với linQ nha)
            return RedirectToAction("Index_Category", "Home");
        }

    }
}