using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến nhà cung cấp
    /// </summary>
    public interface ISupplierDAL
    {
        
        /// <summary>
        /// Bổ sung một nhà cugn cấp mới. Hàm trả về mã nhà cung cấp nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của nhà cung cấp cần bổ sung.</param>
        /// <returns></returns>
        int Add(Supplier data);
        /// <summary>
        /// Lấy danh sách nhà cung cấp cần tièm kiếm, phân trang.
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu.</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang.</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo Supplier, ContactName, Adress, Phone. Chuỗi rỗng nếu không tìm kiếm.</param>
        /// <returns></returns>
        List<Supplier> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng nhà cung cấp thỏa điều kiện tìm kiếm.
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo Supplier, ContactName, Adress, Phone. Chuỗ rỗng nếu không tìm kiếm.</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một nhà cung cấp theo mã. Trong trường hợp nhà cung cấp không tồn tại, hàm trả về giá trị NULL.
        /// </summary>
        /// <param name="supplierID">Mã của nhà cung cấp cần lấy thông tin.</param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Cập nhật thông tin một nhà cung cấp. Hàm trả về giá trị Boolean cho biết việc cập nhật có thành công hay không.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// Xóa một nhà cung cấp dựa vào Mã. Hàm trả về giá trị bool cho biết việc xóa có thực hiện được hay không. 
        /// ( Lưu ý: Không được xóa nhà cung cấp nếu đang có mặt hàng tham chiếu đến nhà cug cấp.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool Delete(int supplierID);
        List<Supplier> ListOfNameSuppliers();

    }
}
