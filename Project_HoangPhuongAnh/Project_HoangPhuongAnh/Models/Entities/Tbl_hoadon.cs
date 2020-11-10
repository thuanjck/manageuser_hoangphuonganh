using System;

namespace Project_HoangPhuongAnh.Models.Entities
{
    public class Tbl_hoadon
    {
        public int _mahd { get; set; }

        public string _full_name { get; set; }

        public DateTime _ngaylaphoadon { get; set; }

        public DateTime _ngaygiaohang { get; set; }

        public string _diachi_giaohang { get; set; }

        public int _trangthai { get; set; }

    }
}