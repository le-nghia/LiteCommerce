using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng tác nghiệp liên quan đến hàng hóa.
    /// </summary>
    public static class ProductService
    {
        private static IProductDAL ProductDB;
        /// <summary>
        /// Khoi tao cac chuc nang tac nghiep ( Ham nay phai duoc goi neu muon su dung 
        /// cac tinh nang cua lop).
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    ProductDB = new DataLayers.SQLServer.ProductDAL(connectionString);
                    break;
                default:
                    throw new Exception("No");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> List(int page, int pageSize, int categoryID, int supplierID, string searchValue, out int rowCount)
        {
            rowCount = ProductDB.Count(categoryID, supplierID, searchValue);
            return ProductDB.List(page, pageSize, categoryID, supplierID, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Product Get(int productId)
        {
            return ProductDB.Get(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static ProductEx GetEx(int productID)
        {
            return ProductDB.GetEx(productID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add(Product data)
        {
            return ProductDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update(Product data)
        {
            return ProductDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool Delete(int productId)
        {
            return ProductDB.Delete(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListAttribute(int productId)
        {
            return ProductDB.ListAttributes(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public static ProductAttribute GetAttribute(long attributeId)
        {
            return ProductDB.GetAttribute(attributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddAttribute(ProductAttribute data)
        {
            return ProductDB.AddAttribute(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return ProductDB.UpdateAttribute(data);
        }
        /// <summary>
        /// Xóa nhiều attribute.
        /// </summary>
        /// <param name="attributeId"></param>
        public static void DeleteAttribute(long[] attributeId)
        {
            foreach(var id in attributeId)
            {
                ProductDB.DeleteAttribute(id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static List<ProductGallery> ListGalleries(int galleryId)
        {
            return ProductDB.ListGalleries(galleryId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public static ProductGallery GetGallery(long galleryId)
        {
            return ProductDB.GetGallery(galleryId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddGallery(ProductGallery data)
        {
            return ProductDB.AddGallery(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateGallery(ProductGallery data)
        {
            return ProductDB.UpdateGallery(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryIds"></param>
        public static void DeleteGalleries(long[] galleryIds)
        {
            foreach(var id in galleryIds)
            {
                ProductDB.DeleteGallery(id);
            }
        }
    }
}
