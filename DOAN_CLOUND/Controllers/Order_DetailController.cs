using DOAN_CLOUND.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN_CLOUND.Controllers
{
    public class Order_DetailController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        WebCafeDataContext db = new WebCafeDataContext();

        //// Thêm Order Detail
        //[HttpGet]
        //public ActionResult Create_Order_Detail()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create_Order_Detail(ORDER_DETAIL order_detail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var existingOrderDetail = db.ORDER_DETAILs.FirstOrDefault(od => od.MAHOADON == order_detail.MAHOADON && od.MASP == order_detail.MASP);
        //            if (existingOrderDetail != null)
        //            {
        //                ModelState.AddModelError("", "Chi tiết hóa đơn đã tồn tại. Vui lòng chọn hóa đơn khác.");
        //                return View(order_detail);
        //            }
        //            db.ORDER_DETAILs.InsertOnSubmit(order_detail); // Thêm đối tượng vào bảng ORDER DETAIL
        //            db.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

        //            return RedirectToAction("Index_Order", "Home");
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", "Có lỗi xảy ra khi lưu chi tiết hóa đơn: " + ex.Message);
        //        }
        //    }
        //    return View(order_detail);
        //}

        // Xóa Order Detail
        [HttpGet]
        public ActionResult Delete_Order_Detail(int mahoadon, int masp)
        {
            ORDER_DETAIL order_detail = db.ORDER_DETAILs.Single(c => c.MAHOADON == mahoadon && c.MASP == masp);
            if (order_detail == null)
            {
                return HttpNotFound();
            }
            return View(order_detail);
        }

        [HttpPost, ActionName("Delete_Order_Detail")]
        public ActionResult Delete_Order_DetailConfirmed(int mahoadon, int masp)
        {
            ORDER_DETAIL order_detail = db.ORDER_DETAILs.Single(c => c.MAHOADON == mahoadon && c.MASP == masp);
            if (order_detail == null)
            {
                return HttpNotFound();
            }
            db.ORDER_DETAILs.DeleteOnSubmit(order_detail);
            db.SubmitChanges();
            return RedirectToAction("Index_Order_Details", "Home");
        }

        // Sửa Order Detail
        [HttpGet]
        public ActionResult Update_Order_Detail(int mahoadon, int masp)
        {
            ORDER_DETAIL order_detail = db.ORDER_DETAILs.Single(c => c.MAHOADON == mahoadon && c.MASP == masp);
            if (order_detail == null)
            {
                return HttpNotFound();
            }
            return View(order_detail);
        }

        [HttpPost]
        public ActionResult Update_Order_Detail(ORDER_DETAIL updatedOrder_Details)
        {
            ORDER_DETAIL order_detail = db.ORDER_DETAILs.Single(c => c.MAHOADON == updatedOrder_Details.MAHOADON && c.MASP == updatedOrder_Details.MASP);
            if (order_detail == null)
            {
                return HttpNotFound();
            }

            order_detail.SOLUONG = updatedOrder_Details.SOLUONG;
            //order_detail.GIA = updatedOrder_Details.GIA;
            db.SubmitChanges(); // Lưu lại thay đổi (này là với linQ nha)
            return RedirectToAction("Index_Order_Details", "Home");
        }
    }
}