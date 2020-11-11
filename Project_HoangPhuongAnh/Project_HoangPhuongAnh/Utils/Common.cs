using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HoangPhuongAnh.Utils
{
    public class Common
    {
        /// <summary>
        /// Hàm lấy giá trị của biến, nếu giá trị null gán giá trị bằng giá trị default
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="value">giá trị cần get kiểm tra null</param>
        /// <param name="defaultValue">giá trị default</param>
        /// <returns>trả về giá trị của biến</returns>
        public static string GetData(object value, string defaultValue)
        {
            if (value == null)
            {
                value = defaultValue;
            }
            return value.ToString();
        }
        /// <summary>
        /// Ham fnayf dùng để convert chuỗi khi parse sang kiểu Int32
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="value">chuỗi cần chuyển kiểu sang Int32</param>
        /// <param name="defaultValue">gán về giá trị mặc định</param>
        /// <returns>chuỗi sau khi xử lý</returns>
        public static int ConvertToInteger(string value, int defaultValue)
        {
            int result;
            try
            {
                result = Int32.Parse(value);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Lấy vị trí data cần lấy
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="currentPage">trang hiện tại</param>
        /// <param name="limit">số lượng record tối đa trên 1 trang</param>
        /// <returns>vị trí cần lấy</returns>
        public static int GetOffset(int currentPage, int limit)
        {
            int offset = 0;
            if (currentPage > 0)
            {
                offset = limit * (currentPage - 1);
            }
            return offset;
        }

        /// <summary>
        /// Tạo chuỗi paging
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="totalUser"> tổng số user</param>
        /// <param name="limit"> số lượng record tối đa trên 1 page</param>
        /// <param name="currentPage">số trang hiện tại</param>
        /// <returns>danh sách các trang cần hiển thị ở chuỗi paging theo trang hiện tại</returns>
        public static List<int> GetListPaging(int totalUser, int limit, int currentPage)
        {
            List<int> listPaging = new List<int>();

            int totalPage = GetTotalPage(totalUser, limit);
            // Số page tối đa trong 1 list
            int limitPage = 3;
            // Lấy chỉ số trang hiện tại chia cho số trang tối đa trong 1 list
            // để xem nó ở nhóm list thứ mấy
            int groupPage = currentPage / limitPage;
            // Nếu chỉ số trang hiện tại chia hết cho số trang tối đa trong 1 list
            if (currentPage % limitPage == 0)
            {
                groupPage = groupPage - 1;
            }
            int i = 1;
            while (i <= limitPage)
            {
                int indexPage = groupPage * limitPage + i;
                listPaging.Add(indexPage);
                if (indexPage == totalPage)
                {
                    break;
                }
                i++;
            }
            return listPaging;
        }

        /// <summary>
        /// hàm tính tổng số trang
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="totalUser"> tổng số user</param>
        /// <param name="limit">số lượng tối đa trên 1 trang</param>
        /// <returns>tổng số trang</returns>
        public static int GetTotalPage(int totalUser, int limit)
        {
            int totalPage;
            // lấy tổng số user / số user hiển thị tối đa trên 1 trang
            // nếu k có dư thì thương chính là tổng số page
            if (totalUser % limit == 0)
            {
                totalPage = totalUser / limit;
            }
            else
            {
                // nếu có dư thì tổng số page bằng thương + 1
                totalPage = totalUser / limit + 1;
            }
            return totalPage;
        }

        /// <summary>
        /// Hàm dùng xử lý kí tự WildCard
        /// Created by ThuanTV 27/11/2019
        /// </summary>
        /// <param name="value">chuỗi muốn xử lý</param>
        /// <returns>chuỗi sau khi được xử lý</returns>
        public static string ReplaceWildCard(string value)
        {
            value = value.Replace("|", "!|");
            value = value.Replace("%", "!%");
            value = value.Replace("_", "!_");
            value = value.Replace("[", "![");
            value = value.Replace("]", "!]");
            return value;
        }
    }
}