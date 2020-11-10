namespace Project_HoangPhuongAnh.Models.Entities
{
    public class Tbl_khachhang
    {
        public int _makh { get; set; }

        public string _username { get; set; }

        public string _password { get; set; }

        public string _ful_name { get; set; }

        public int _gioitinh { get; set; }

        public string _address_user { get; set; }

        public string _email { get; set; }

        public string _tel { get; set; }

        public string _salt_user { set; get; }
    }
}