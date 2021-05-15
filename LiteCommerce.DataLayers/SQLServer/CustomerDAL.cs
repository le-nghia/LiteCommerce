using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các tính năng xử lý dữ liệu Khách hàng trong csdl sql server
    /// </summary>
    public class CustomerDAL : _BaseDAL, ICustomerDAL
    {
        public CustomerDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            int customerID = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Customers
                                    (
	                                    CustomerName, ContactName, Address, City, PostalCode, Country
                                    )
                                    VALUES
                                    (
	                                    @customerName, @contactName, @address, @city, @postalCode, @country
                                    );
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@customerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);

                customerID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return customerID;
        }
        /// <summary>
        /// Đếm số lượng
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select COUNT(*)
                                    from Customers
	                                where (@searchValue = '') OR
                                        (
                                            CustomerName LIKE @searchValue
							                OR ContactName LIKE @searchValue
							                OR Address LIKE @searchValue
							                OR Country LIKE @searchValue 
                                        )";

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }
            return result;
        }
        /// <summary>
        /// Xóa 1 khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool Delete(int customerID)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Customers
                                    WHERE CustomerID = @customerID AND NOT EXISTS(SELECT * FROM Orders WHERE CustomerID = Customers.CustomerID) ";

                cmd.Parameters.AddWithValue("@customerID", customerID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// lấy thông tin 1 khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer Get(int customerID)
        {
            Customer data = null;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Customers WHERE CustomerID = @customerID";

                cmd.Parameters.AddWithValue("@customerID", customerID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                        };

                    }
                }

                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// hiển thị danh sách.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            List<Customer> data = new List<Customer>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (
	                                select *, ROW_NUMBER() OVER (ORDER BY CustomerName) AS RowNumber
	                                from Customers
	                                where (@searchValue = '') OR (CustomerName LIKE @searchValue
							                                  OR ContactName LIKE @searchValue
							                                  OR Address LIKE @searchValue
							                                  OR City LIKE @searchValue 
                                                              OR Country LIKE @searchValue)
                                    ) AS s where s.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {

                        data.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])

                        });

                    }
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Customer data)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Customers
                                    SET CustomerName = @customerName,
	                                    ContactName = @contactName,
	                                    Address = @address,
	                                    City = @city,
	                                    PostalCode = @postalCode,
	                                    Country = @country
                                    WHERE CustomerID = @customerID;";

                cmd.Parameters.AddWithValue("@customerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@customerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        ///Cach Thuong.
        /*private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Customer> List()
        {
            List<Customer> data = new List<Customer>();
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = this.connectionString;
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = " SELECT * FROM Customers";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])

                        });
                    }
                }

                cn.Close();
            }
            return data;
        }*/
    }
}
