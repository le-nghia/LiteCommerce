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
    /// Lớp cung cấp các chức năng tác nghiệp liên quan đến quản lý nhân sự
    /// </summary>
    public static class HRService
    {

        private static IEmployeeDAL EmployeeDB;
        private static ISupplierDAL SupplierDB;
        private static ICategoriDAL CategoriDB;
        private static IShipperDAL ShipperDB;
        private static ICustomerDAL CustomerDB;
        private static IProductDAL ProductDB;
        /// <summary>
        /// Khởi tạo tầng nghiệp vụ
        /// (Hàm này phải được gọi trước khi sửu dụng chức năng khác trong lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:

                    EmployeeDB = new LiteCommerce.DataLayers.SQLServer.EmployeeDAL(connectionString);
                    SupplierDB = new LiteCommerce.DataLayers.SQLServer.SupplierDAL(connectionString);
                    CategoriDB = new LiteCommerce.DataLayers.SQLServer.CategoriDAL(connectionString);
                    ShipperDB = new LiteCommerce.DataLayers.SQLServer.ShipperDAL(connectionString);
                    CustomerDB = new LiteCommerce.DataLayers.SQLServer.CustomerDAL(connectionString);
                    ProductDB = new LiteCommerce.DataLayers.SQLServer.ProductDAL(connectionString);

                    break;
                case DatabaseTypes.MySQL:

                    break;
                default:
                    throw new Exception("Database Type is not supported");
            }
        }
        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /*public static List<Employee> Employee_list()
        {
            return EmployeeDB.List();
        }*/
        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        /*public static List<Supplier> Supplier_list()
        {
            return SupplierDB.List();
        }*/
        /// <summary>
        /// Lấy danh sách loại hàng
        /// </summary>
        /// <returns></returns>
        /*public static List<Categori> Categori_list()
        {
            return CategoriDB.List();
        }*/
        /// <summary>
        /// Lấy danh sách nhà vận chuyển
        /// </summary>
        /// <returns></returns>
        /*public static List<Shipper> Shipper_list()
        {
            return ShipperDB.List();
        }*/
        /// <summary>
        /// Lấy danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        /*public static List<Customer> Customer_list()
        {
            return CustomerDB.List();
        }*/
        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        /// <returns></returns>
        /*public static List<Product> Product_list()
        {
            return ProductDB.List();
        }*/
    }
}
