using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HoangPhuongAnh.Models.Entities
{
    public class ShopInfo
    {
        public string _product { get; set; }
        public string _tensanpham { get; set; }
        public string _gia { get; set; }
        public string _discount { get; set; }
        public string _action { get; set; }
        public int? _currentPage { get; set; }
    }
}