using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến nhà vận chuyển
    /// </summary>
    public interface IShipperDAL
    {
        /// <summary>
        /// Lấy Ds nhà vận chuyển cần tìm kiếm, phân trang
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số dòng cần hiển thị trên mỗi trang.</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo Shiper</param>
        /// <returns></returns>
        List<Shipper> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Bổ sung 1 nhà vận chuyển mới ( Hàm trả về mã nhà vận chuyển nếu bổ sung thành công).
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin mã nhà vận chuyển cần bổ sung.</param>
        /// <returns></returns>
        int Add(Shipper data);
        /// <summary>
        /// Lấy thông tin 1 nhà vận chuyển theo mã.
        /// </summary>
        /// <param name="shipperID">Mã của nhà vận chuyển cần lấy.</param>
        /// <returns></returns>
        Shipper Get(int shipperID);
        /// <summary>
        /// Đếm số lượng nhà vận chuyển thỏa điều kiện tìm kiếm.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Cập nhật thông tin 1 nhà vận chuyển.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);
        /// <summary>
        /// Xóa một nhà vận chuyển dựa vào mã.
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        bool Delete(int shipperID);
    }
}
