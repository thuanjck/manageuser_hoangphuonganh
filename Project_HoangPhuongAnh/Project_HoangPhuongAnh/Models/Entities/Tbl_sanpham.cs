using System;

namespace Project_HoangPhuongAnh.Models.Entities
{
    public class Tbl_sanpham
    {
        public int _masp { get; set; }

        public int _maloai { set; get; }

        public string _tensanpham { get; set; }

        public string _gia_mua { get; set; }

        public string _gia_ban { get; set; }

        public String _size { set; get; }

        public int _soluong { set; get; }

        public string _thongtin { get; set; }

        public DateTime _ngaynhaphang { set; get; }

        public string _hinhanh { set; get; }

        public int discount { set; get; }
    }
}