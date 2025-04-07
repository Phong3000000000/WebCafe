using DOAN_CLOUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_CLOUND.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        WebCafeDataContext db = new WebCafeDataContext();

        // Thêm user
        [HttpGet]
        public ActionResult Create_User()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_User(USER user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = db.USERs.FirstOrDefault(c => c.USERNAME == user.USERNAME);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("", "Username đã tồn tại. Vui lòng chọn tên khác.");
                        return View(user);
                    }
                    db.USERs.InsertOnSubmit(user); // Thêm đối tượng vào bảng CATEGORIES
                    db.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    return RedirectToAction("Index_User", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu người dùng: " + ex.Message);
                }
            }
            return View(user);
        }

        // Xóa user
        [HttpGet]
        public ActionResult Delete_User(int id)
        {
            USER user = db.USERs.Single(c => c.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete_User")]
        public ActionResult Delete_UserConfirmed(int id)
        {
            USER user = db.USERs.Single(c => c.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            db.USERs.DeleteOnSubmit(user);
            db.SubmitChanges();
            return RedirectToAction("Index_User", "Home");
        }

        // Sửa user
        [HttpGet]
        public ActionResult Update_User(int id)
        {
            USER user = db.USERs.Single(c => c.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Update_User(USER updatedUser)
        {
            USER user = db.USERs.Single(c => c.ID == updatedUser.ID);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.HOTEN = updatedUser.HOTEN;
            user.CHUCVU = updatedUser.CHUCVU;
            user.MANV_QUANLY = updatedUser.MANV_QUANLY;
            user.NGAYSINH = updatedUser.NGAYSINH;
            user.GIOITINH = updatedUser.GIOITINH;
            user.CMND = updatedUser.CMND;
            user.EMAIL = updatedUser.EMAIL;
            user.ROLE = updatedUser.ROLE;
            user.DIACHI = updatedUser.DIACHI;
            user.SODT = updatedUser.SODT;
            user.USERNAME = updatedUser.USERNAME;
            user.PASSWORD = updatedUser.PASSWORD;   
            db.SubmitChanges(); // Lưu lại thay đổi (này là với linQ nha)
            return RedirectToAction("Index_User", "Home");
        }
    }
}