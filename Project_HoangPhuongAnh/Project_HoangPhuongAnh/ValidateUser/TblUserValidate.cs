using Project_HoangPhuongAnh.Models.DAO;
using Project_HoangPhuongAnh.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HoangPhuongAnh.ValidateUser
{
    public class TblUserValidate
    {
        TblUserDao userDao = new TblUserDao();

        /// <summary>
        /// validate kiểm tra trường user_name và password, thao tác với csdl của màn hình ADM001
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<string> ValidateLogin(string email, string password)
        {
            List<string> listErrorMess = new List<string>();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                if (!userDao.CheckLogin(email, password))
                {
                    listErrorMess.Add(ErrorMessage.ER001);
                }

            }
            return listErrorMess;
        }
    }
}