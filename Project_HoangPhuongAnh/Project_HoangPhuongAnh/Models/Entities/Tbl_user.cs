using System;

namespace Project_HoangPhuongAnh.Models.Entities
{
    public class Tbl_user
    {
        public int _user_id { get; set; }

        public string _email { set; get; }

        public string _password { get; set; }

        public string _full_name { get; set; }

        public string _tel { get; set; }

        public DateTime _birthday { set; get; }

        public string _address_user { set; get; }

        public int _role { get; set; }

        public string _salt_random { set; get; }
    }
}