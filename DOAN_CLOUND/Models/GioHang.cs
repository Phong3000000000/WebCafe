using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOAN_CLOUND.Models
{
    public class GioHang
    {
        WebCafeDataContext db = new WebCafeDataContext();


        int id_san_pham;

        public int Id_san_pham { get => id_san_pham; set => id_san_pham = value; }


        string ten_san_pham;

        public string Ten_san_pham { get => ten_san_pham; set => ten_san_pham = value; }


        string anhbia;

        public string Anhbia
        {
            get { return anhbia; }
            set { anhbia = value; }
        }
        float dongia;

        public float Dongia
        {
            get { return dongia; }
            set { dongia = value; }
        }
        int soluong;

        public int Soluong
        {
            get { return soluong; }
            set { soluong = value; }
        }
        float thanhtien;

        public float Thanhtien
        {
            get { return soluong * (float)dongia; }
            set { thanhtien = value; }
        }


        public GioHang(int id)
        {
            id_san_pham = id;
            PRODUCT sanpham = db.PRODUCTs.Single(s => s.ID == id_san_pham);
            ten_san_pham = sanpham.TENHANG;
            anhbia = sanpham.HINHANH;
            dongia = (float)sanpham.GIA;
            soluong = 1;
            thanhtien = soluong * dongia;
        }
    }
}