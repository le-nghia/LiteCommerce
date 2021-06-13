using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến loại hàng
    /// </summary>
    public interface ICategoriDAL
    {
        /// <summary>
        /// Đếm số lượng.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Categori data);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Categori data);
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool Delete(int categoryID);
        /// <summary>
        /// Lấy thông tin của một người.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        Categori Get(int categoryID);
        /// <summary>
        /// Lấy DS nhà cung cấp cần Tìm Kiếm.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Categori> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Lấy Tên Hàng.
        /// </summary>
        /// <returns></returns>
        List<Categori> listOfNameCategorys();
    }
}
