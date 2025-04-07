using DOAN_CLOUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_CLOUND.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        WebCafeDataContext db = new WebCafeDataContext();

        // Thêm Order
        [HttpGet]
        public ActionResult Create_Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create_Order(ORDER order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrder = db.ORDERs.FirstOrDefault(o => o.MAHOADON == order.MAHOADON);
                    if (existingOrder != null)
                    {
                        ModelState.AddModelError("", "Hóa đơn đã tồn tại. Vui lòng chọn hóa đơn khác.");
                        return View(order);
                    }
                    db.ORDERs.InsertOnSubmit(order); // Thêm đối tượng vào bảng ORDER
                    db.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    return RedirectToAction("Index_Order", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu hóa đơn: " + ex.Message);
                }
            }
            return View(order);
        }

        // Xóa Order
        [HttpGet]
        public ActionResult Delete_Order(int mahoadon)
        {
            ORDER order = db.ORDERs.Single(c => c.MAHOADON == mahoadon);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete_Order")]
        public ActionResult Delete_OrderConfirmed(int mahoadon)
        {
            ORDER order = db.ORDERs.Single(c => c.MAHOADON == mahoadon);
            if (order == null)
            {
                return HttpNotFound();
            }
            db.ORDERs.DeleteOnSubmit(order);
            db.SubmitChanges();
            return RedirectToAction("Index_Order", "Home");
        }

        // Sửa Order
        [HttpGet]
        public ActionResult Update_Order(int mahoadon)
        {
            ORDER order = db.ORDERs.Single(c => c.MAHOADON == mahoadon);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Update_Order(ORDER updatedOrder)
        {
            ORDER order = db.ORDERs.Single(c => c.MAHOADON == updatedOrder.MAHOADON);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.MAKH = updatedOrder.MAKH;
            order.NGAYLAP = updatedOrder.NGAYLAP;
            order.TONGGIA = updatedOrder.TONGGIA;
            db.SubmitChanges(); // Lưu lại thay đổi (này là với linQ nha)
            return RedirectToAction("Index_Order", "Home");
        }
    }
}