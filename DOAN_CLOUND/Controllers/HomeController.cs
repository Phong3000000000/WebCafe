using DOAN_CLOUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DOAN_CLOUND.Models;


namespace DOAN_CLOUND.Controllers
{
    public class HomeController : Controller
    {

        WebCafeDataContext db = new WebCafeDataContext();
        List<GioHang> listgiohang = new List<GioHang>();


        public ActionResult about()
        {
            return View();
        }
        public ActionResult blog_single()
        {
            return View();
        }

        public ActionResult blog()
        {
            return View();
        }

        public ActionResult cart()
        {
            return View();
        }

        public ActionResult checkout()
        {
            return View();
        }

        public ActionResult contact()
        {
            return View();
        }

        public ActionResult menu()
        {
            var listProduct = db.PRODUCTs.ToList();
            return View(listProduct);
        }

        public ActionResult product_single()
        {
            return View();
        }

        public ActionResult services()
        {
            return View();
        }

        public ActionResult shop()
        {
            var listProduct = db.PRODUCTs.ToList();
            return View(listProduct);
        }
        public ActionResult index()
        {
            var listProduct = db.PRODUCTs.ToList();
            return View(listProduct);
        }

        public ActionResult Index_Products()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            var listProduct = db.PRODUCTs.ToList();
            return View(listProduct);
        }

        public ActionResult Index_Category()
        {
            var listCategories = db.CATEGORies.ToList();
            return View(listCategories);
        }

        public ActionResult Index_User()
        {
            var listUser = db.USERs.ToList();
            return View(listUser);
        }

        public ActionResult Index_Order()
        {
            var listOrder = db.ORDERs.ToList();
            return View(listOrder);
        }

        public ActionResult Index_Order_Details()
        {
            var listOrderDetails = db.ORDER_DETAILs.ToList();
            return View(listOrderDetails);
        }

        public ActionResult Index_Order_Details_Id(int mahd)
        {
            var listOrderDetails = db.ORDER_DETAILs.Where(x => x.MAHOADON == mahd).ToList();
            return View(listOrderDetails);
        }


        

        // đăng nhập - đăng ký
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(USER user, FormCollection fc)
        {
            var hoten = fc["HotenKH"];

            var dienthoai = fc["Dienthoai"];
            var email = fc["Email"];
            var diachi = fc["Diachi"];

            var taikhoan = fc["TenDN"];
            var matkhau = fc["Matkhau"];

            bool kiemtra = true;

            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Ho ten khong duoc de trong";
                kiemtra = false;
            }

            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Dien Thoai khong duoc de trong";
                kiemtra = false;
            }


            if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Loi2"] = "Ten dang nhap khong duoc de trong";
                kiemtra = false;
            }

            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mat Khau khong duoc de trong";
                kiemtra = false;
            }

            if (kiemtra == true)
            {
                user.HOTEN = hoten;
                user.DIACHI = diachi;
                user.EMAIL = email;
                user.USERNAME = taikhoan;
                user.PASSWORD = matkhau;
                user.ROLE = "USER";
                user.SODT = dienthoai;


                //khach.DienThoai = dienthoai;
                //khach.Email = email;
                //khach.DiaChi = diachi;
                //khach.TaiKhoan = taikhoan;
                //khach.MatKhau = matkhau;


                db.USERs.InsertOnSubmit(user);
                db.SubmitChanges();
                return RedirectToAction("DangNhap", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(USER user, FormCollection fc)
        {
            var taikhoan = fc["TenDN"];
            var matkhau = fc["Matkhau"];
            Session["tendn"] = fc["TenDN"];
            bool kiemtra = true;

            Session ["tendn"] = fc["TenDN"];

            if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Loi1"] = "Ten dang nhap khong duoc de trong";
                kiemtra = false;
            }

            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mat Khau khong duoc de trong";
                kiemtra = false;
            }

            if (kiemtra == true)
            {
                user.USERNAME = taikhoan;
                user.PASSWORD = matkhau;
                //db.KhachHangs.InsertOnSubmit(khach);
                //db.SubmitChanges();

                USER us = db.USERs.SingleOrDefault(c => c.USERNAME == taikhoan && c.PASSWORD == matkhau);

                if (us != null)
                {
                    if (us.ROLE == "USER")
                    {
                        ViewBag.TB = "Đăng Nhập Thành Công";
                        Session["taikhoan"] = us;
                    }
                    if (us.ROLE == "ADMIN")
                    {
                        Session["taikhoan"] = us;
                        return RedirectToAction("Index_Products", "Home");
                    }
                }
                else
                {
                    ViewBag.TB = "Đăng Nhập Thất Bại, Sai Tài Khoản Hoặc Mật Khẩu";
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult DangXuat()
        {
            Session["taikhoan"] = null;
            return RedirectToAction("index", "Home");
        }

        //[HttpPost]
        //public ActionResult DangNhap(KhachHang dn)
        //{
        //    Session["loi"] = "Dang nhap khong thanh cong (sai thong tin)";
        //    Session["thongbao"] = "Dang nhap thanh cong";

        //    UsersRepository condn = new UsersRepository();
        //    string role = condn.lay_role(dn.UserName, dn.Pass);
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.kt = condn.kiemtra(dn.UserName, dn.Pass);


        //        if (ViewBag.kt == 0)
        //        {
        //            return View();
        //        }
        //        else if (role == "ADMIN")
        //        {
        //            return RedirectToAction("Index_User", "Home");
        //        }
        //        else
        //        {
        //            return RedirectToAction("index", "Home");
        //        }

        //    }

        //    return View();
        //}

        public ActionResult GioHang()
        {
            List<GioHang> listgiohang = LayGioHang();
            if (listgiohang.Count == 0)
            {
                return RedirectToAction("GioHangRong", "Home");
            }

            ViewBag.tongtien = TinhTongTien();
            ViewBag.soluong = TinhSoLuong();
            return View(listgiohang);
        }

        public ActionResult GioHangRong()
        {
            return View();
        }


        public List<GioHang> LayGioHang()
        {
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang == null)
            {
                listgiohang = new List<GioHang>();
                Session["GioHang"] = listgiohang;
            }

            return listgiohang;
        }

        public ActionResult ThemGioHang(int id, string strUrl)
        {
            List<GioHang> listgiohang = LayGioHang();

            strUrl = strUrl + "#menu-scroll";

            GioHang sanphammoi = listgiohang.Find(sp => sp.Id_san_pham == id);
            if (sanphammoi == null)
            {
                sanphammoi = new GioHang(id);
                listgiohang.Add(sanphammoi);
                return Redirect(strUrl);
            }
            else
            {
                sanphammoi.Soluong++;
                return Redirect(strUrl);
            }
        }


        public float TinhTongTien()
        {
            float tongtien = 0;
            List<GioHang> listgiohang = LayGioHang();
            foreach (GioHang item in listgiohang)
            {
                tongtien += item.Thanhtien;
            }
            return tongtien;
        }

        public int TinhSoLuong()
        {
            int soluong = 0;
            List<GioHang> listgiohang = LayGioHang();
            foreach (GioHang item in listgiohang)
            {
                soluong += item.Soluong;
            }
            return soluong;
        }


        public ActionResult SoLuongGioHang()
        {
            ViewBag.soluong = TinhSoLuong();
            return View();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> listgiohang = LayGioHang();

            GioHang sanphammoi = listgiohang.Find(sp => sp.Id_san_pham == id);
            if (sanphammoi != null)
            {
                listgiohang.RemoveAll(sp => sp.Id_san_pham == id);
                return RedirectToAction("GioHang", "Home");
            }
            else
            {
                return RedirectToAction("GioHangRong", "Home");
            }
        }

        public ActionResult DeleteAll()
        {
            string id;
            List<GioHang> listgiohang = LayGioHang();

            //foreach (var item in listgiohang)
            //{
            //    masach = item.Masach;
            //    listgiohang.RemoveAll(sp => sp.Masach == masach);
            //}
            listgiohang.Clear();
            return RedirectToAction("GioHangRong", "Home");
        }

        //public ActionResult UpdateGioHang(int id, FormCollection f)
        //{
        //    List<GioHang> listgiohang = LayGioHang();

        //    GioHang sanpham = listgiohang.Find(sp => sp.Id_san_pham == id);

        //    if (sanpham != null)
        //    {
        //        sanpham.Soluong = int.Parse(f["txtsoluong"].ToString());

        //    }

        //    return RedirectToAction("GioHang", "Home");
        //}

        [HttpPost]
        public JsonResult UpdateGioHang(int id, FormCollection f)
        {
            List<GioHang> listgiohang = LayGioHang();
            GioHang sanpham = listgiohang.Find(sp => sp.Id_san_pham == id);

            if (sanpham != null)
            {
                sanpham.Soluong = int.Parse(f["txtsoluong"].ToString());
            }

            //// Tính toán tổng tiền cho giỏ hàng, bạn có thể thay đổi cách tính nếu cần  
            //float tongtien = TinhTongTien();

            //return Json(new { totalAmount = sanpham.Thanhtien }); // Cập nhật giá trị tổng trong response  

            // Tính toán tổng tiền cho từng sản phẩm  
            float productTotal = sanpham.Dongia * sanpham.Soluong; 
            sanpham.Thanhtien = productTotal;

            // Tính toán tổng tiền cho giỏ hàng  
            float cartTotal = TinhTongTien();

            // Tính tổng số lượng sản phẩm trong giỏ hàng  
            int quantityTotal = TinhSoLuong();

            return Json(new { totalAmount = productTotal, cartTotal = cartTotal, quantityTotal = quantityTotal });
        }

        // kết thúc phần giỏ hàng

        // bắt đầu phần thanh toán đơn hàng
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["taikhoan"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("menu", "Home");
            }

            List<GioHang> listgiohang = LayGioHang();
            ViewBag.TongSoLuong = TinhSoLuong();
            ViewBag.TongThanhTien = TinhTongTien();

            USER user = (USER)Session["taikhoan"];
            ViewBag.makh = user.ID;
            ViewBag.hoten = user.HOTEN;
            ViewBag.dienthoai = user.SODT;
            ViewBag.email = user.EMAIL;
            ViewBag.diachi = user.DIACHI;
            return View(listgiohang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection fc)
        {
            ORDER order = new ORDER();
            USER user = (USER)Session["taikhoan"];
            ViewBag.makh = user.ID;
            ViewBag.hoten = user.HOTEN;
            ViewBag.taikhoan = user.USERNAME;



            List<GioHang> gh = LayGioHang();
            order.MAKH = user.ID;
            order.NGAYLAP = DateTime.Now;
            order.TONGGIA = 0;
            db.ORDERs.InsertOnSubmit(order);
            db.SubmitChanges();

            foreach (var item in gh)
            {
                ORDER_DETAIL chitiet = new ORDER_DETAIL();
                chitiet.MAHOADON = order.MAHOADON;
                chitiet.MASP = item.Id_san_pham;
                chitiet.SOLUONG = item.Soluong;
                chitiet.GIA = (decimal)item.Dongia;
                db.ORDER_DETAILs.InsertOnSubmit(chitiet);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "Home");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }


    }
}