using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến nhân viên
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Lấy Danh sách nhà cung cấp cần tìm kiếm. Phân trang.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Lấy thông tin của 1 nhân viên theo mã.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        Employee Get(int employeeID);
        /// <summary>
        /// Bổ sung nhân viên mới.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// Cập nhật thông tin nhân viên theo mã.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// Xóa một nhân viên theo mã.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool Delete(int employeeID);
        /// <summary>
        /// Đếm số lượng.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
    }
}
