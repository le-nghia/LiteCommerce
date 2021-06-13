using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDetailDAL
    {
        /// <summary>
        /// Lấy danh sách các đơn hàng.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<OrderDetail> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Lấy thông tin đơn hàng theo mã
        /// </summary>
        /// <param name="orderDetailID"></param>
        /// <returns></returns>
        OrderDetail Get(int orderDetailID);
        /// <summary>
        /// Thêm đơn hàng.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(OrderDetail data);
        /// <summary>
        /// Cập nhật đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(OrderDetail data);
        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(OrderDetail data);
        /// <summary>
        /// Đếm số lượng Oder
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
    }
}
