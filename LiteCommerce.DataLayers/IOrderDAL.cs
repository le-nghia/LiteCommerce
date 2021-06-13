using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến đơn hàng.
    /// </summary>
    public interface IOrderDAL
    {
        /// <summary>
        /// Lấy danh sách các đơn hàng.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="CustomerID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Order> List(int page, int pageSize, int customerID, string searchValue);
        /// <summary>
        /// Thêm đơn hàng.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Order data);
        /// <summary>
        /// Xóa đơn hàng.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        bool Delete(int orderID);
        /// <summary>
        /// Cập nhật đơn hàng.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        bool Update(Order data);
        /// <summary>
        /// Đếm Sl đơn hàng.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Order Get(int orderID);
    }
}
