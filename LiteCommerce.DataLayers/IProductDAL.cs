using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên liên quan đến mặt hàng
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// Lấy danh sách các mặt hàng.
        /// </summary>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="categoryID">Mã loại hàng.  0 nếu lọc không có</param>
        /// <param name="supplierID">Mã nhà cung cấp. 0 nếu lọc không có.</param>
        /// <param name="searchValue">Tên mặt hàng cần tìm kiếm</param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, int categoryID, int supplierID, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryID, int supplierID, string searchValue);
        /// <summary>
        /// Bổ sung một mặt hàng mới.
        /// Hàm trả về bổ sung nếu thành công.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// Cập nhật thông tin mặt hàng.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// Xóa mặt hàng.
        /// Khi xóa mặt hàng thì xóa các thuộc tính và thư viện hình ảnh của mặt hàng.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        bool Delete(int productID);
        /// <summary>
        /// Lấy thông tin mặt hàng theo mã.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Product Get(int productID);
        /// <summary>
        /// Lấy tthông tin mặt hàngg liên quan đến mã hàng.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        ProductEx GetEx(int productID);

        /// <summary>
        /// Lấy danh sách các thuộc tính của 1 product. ( Sắp xếp theo DisplayOrder).
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productID);
        /// <summary>
        /// Lấy thông tin chi tiết của một thuộc tính.
        /// </summary>
        /// <param name="atributeId"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long atributeId);
        /// <summary>
        /// Bổ sung 1 thuộc tính cho mặt hàng.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// Xóa 1 thuộc tính.
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeId);

        /// <summary>
        /// Lấy danh sách của một mặt hàng.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductGallery> ListGalleries(int productId);
        /// <summary>
        /// Lấy thông tin của một hình ảnh.
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryId);
        long AddGallery(ProductGallery data);
        /// <summary>
        /// Cập nhật thông tin.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);
        /// <summary>
        /// Xóa 1 ảnh.
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryId);

    }
}
