using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Các chức năng nghiệp vụ liên quan đến quản lý dữ liệu chung.
    /// </summary>
    public static class DataService
    {
        private static ICountryDAL CountryDB;
        private static ICityDAL CityDB;

        private static ISupplierDAL SupplierDB;
        private static ICategoriDAL CategoriDB;
        private static IEmployeeDAL EmployeeDB;
        private static ICustomerDAL CustomerDB;
        private static IOrderDAL OrderDB;
        private static IShipperDAL ShipperDB;

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

                    EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
                    CountryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
                    CityDB = new DataLayers.SQLServer.CityDAL(connectionString);
                    SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
                    CategoriDB = new DataLayers.SQLServer.CategoriDAL(connectionString);
                    CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
                    OrderDB = new DataLayers.SQLServer.OrderDAL(connectionString);
                    ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);

                    break;

                default:
                    throw new Exception("Database Type is not Supported");
            }
        }
        /// <summary>
        /// Danh sach cac quoc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListCoutries()
        {
            return CountryDB.List();
        }
        /// <summary>
        /// Danh sach cac thanh pho.
        /// </summary>
        /// <returns></returns>
        public static List<City> ListCities()
        {
            return CityDB.List();
        }
        /// <summary>
        /// Danh sach cac thanh pho thuoc quoc gia.
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static List<City> ListCities(string countryName)
        {
            return CityDB.List();
        }
        /// <summary>
        /// Danh sách nhà cung cấp (phân trang, tìm kiếm).
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Lay ten loai hang
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListNameSuppliers()
        {
            return SupplierDB.ListOfNameSuppliers();
        }
        /// <summary>
        /// Bổ sung 1 nhà cung cấp.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật 1 nhà cung cấp.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSuppliers(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        /// <summary>
        /// Xóa 1 nhà cung cấp.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSuppliers(int supplierID)
        {
            return SupplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp.
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static Supplier GetSuppliers(int supplierId)
        {
            return SupplierDB.Get(supplierId);
        }
        /// <summary>
        /// Danh sach nha phan loai.Phan trang tim kiem.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Categori> ListCategoris(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CategoriDB.Count(searchValue);
            return CategoriDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Lay ten loai hang
        /// </summary>
        /// <returns></returns>
        public static List<Categori> ListNameCategoryes()
        {
            return CategoriDB.listOfNameCategorys();
        }
        /// <summary>
        /// Xoa mot loai hang.
        /// </summary>
        /// <param name="categoriID"></param>
        /// <returns></returns>
        public static bool DeleteCategori(int categoriID)
        {
            return CategoriDB.Delete(categoriID);
        }
        /// <summary>
        /// Cap nhat loai hang
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategori(Categori data)
        {
            return CategoriDB.Update(data);
        }
        /// <summary>
        /// Them mot loai hang
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategori(Categori data)
        {
            return CategoriDB.Add(data);
        }
        /// <summary>
        /// Lay thong tin cua mot loai hang.
        /// </summary>
        /// <param name="categoriId"></param>
        /// <returns></returns>
        public static Categori GetCategoris(int categoriId)
        {
            return CategoriDB.Get(categoriId);
        }

        /// <summary>
        /// Lấy Danh sách Nhân Viên
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListEmpolyees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Lấy 1 thông tin nhân viên.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }
        /// <summary>
        /// Thêm 1 nhân viên.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật lại một nhân viên.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        /// <summary>
        /// Xóa một nhân viên có mã là ID.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return EmployeeDB.Delete(employeeID);
        }

        /// <summary>
        /// Lấy Danh sách Khách Hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Bổ sung khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        // <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return CustomerDB.Delete(customerID);
        }
        /// <summary>
        /// Lấy thông tin 1 khách hàng.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomers(int customerID)
        {
            return CustomerDB.Get(customerID);
        }

        /// <summary>
        /// Lấy danh sách nhà vận chuyển.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// lấy thông tin 1 nhà shipper.
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShippers(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }
        /// <summary>
        /// Xóa 1 nhà vận chuyển
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteShippers(int shipperID)
        {
            return ShipperDB.Delete(shipperID);
        }
        /// <summary>
        /// Cập nhật 1 nhà vận chuyển.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShippers(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        /// <summary>
        /// Thêm 1 nhà vận chuyển.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShippers(Shipper data)
        {
            return ShipperDB.Add(data);
        }

        public static List<Order> ListOrders(int page, int pageSize, int customerID, string searchValue, out int rowCount)
        {
            rowCount = OrderDB.Count(searchValue);
            return OrderDB.List(page, pageSize, customerID, searchValue);
        }
        public static Order GetOrder(int orderID)
        {
            return OrderDB.Get(orderID);
        }
        public static int AddOrder(Order data)
        {
            return OrderDB.Add(data);
        }
        public static bool Update(Order data)
        {
            return OrderDB.Update(data);
        }
        public static bool Delete(int orderID)
        {
            return OrderDB.Delete(orderID);
        }
    }
}
