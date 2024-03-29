﻿using LiteCommerce.DomainModels;
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
    /// Cài đặt các tính năng xử lý dữ liệu nhà cung cấp trong csdl sql server
    /// </summary>
    public class SupplierDAL : _BaseDAL, ISupplierDAL
    {
        public SupplierDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int supplierID = 0;

            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Suppliers 
                                    (
                                        SupplierName, ContactName, Address, City, PostalCode, Country, Phone
                                    ) VALUES 
                                        (
                                            @SupplierName, @ContactName, @Address, @City, @PostalCode, @Country, @Phone
                                        );
                                            SELECT @@IDENTITY;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);


                supplierID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return supplierID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Supplier> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (
	                                select *, ROW_NUMBER() OVER (ORDER BY SupplierName) AS RowNumber
	                                from Suppliers
	                                where (@searchValue = '') OR (SupplierName LIKE @searchValue
							                                  OR ContactName LIKE @searchValue
							                                  OR Address LIKE @searchValue
							                                  OR Phone LIKE @searchValue )
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
                        /*Country country = new Country();
                        country.CountryName = Convert.ToString(dbReader["CountryName"]);
                        data.Add(country);*/

                        /*Country country = new Country()
                        {
                            country.CountryName = Convert.ToString(dbReader["CountryName"])
                        };
                        data.Add(country);*/


                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                         
                        });
                    }
                }


                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            int result = 0;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select COUNT(*)
                                    from Suppliers
	                                where (@searchValue = '') OR
                                        (
                                            SupplierName LIKE @searchValue
							                OR ContactName LIKE @searchValue
							                OR Address LIKE @searchValue
							                OR Phone LIKE @searchValue 
                                        )";

                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier Get(int supplierID)
        {
            ///Lấy thông tin của 1 Supplierr
            Supplier data = null;
            using (SqlConnection cn = GetConnection())
            {

                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"select * from Suppliers where SupplierID = @SupplierID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) //Using se giai phong vung nho sau khi thuc hien.
                {
                    if (dbReader.Read()) //Neu co du lieu dua ra ket qua.
                    {
                        data = new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"])

                        };
                    }
                }

                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            bool result = false;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Suppliers 
                                        SET 
	                                        SupplierName = @SupplierName,
	                                        ContactName = @ContactName,
	                                        Address = @Address,
	                                        City = @City,
	                                        PostalCode = @PostalCode,
	                                        Country = @Country,
	                                        Phone = @Phone
                                        WHERE SupplierID = @SupplierID;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public bool Delete(int supplierID)
        {
            bool result = false;

            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Suppliers WHERE SupplierID = @SupplierID 
                                    AND NOT EXISTS
                                    (
                                        SELECT * FROM Products WHERE SupplierID = Suppliers.SupplierID
                                    )";
                cmd.Parameters.AddWithValue("@supplierID", supplierID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        public List<Supplier> ListOfNameSuppliers()
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT SupplierID, SupplierName FROM Suppliers";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"])
                        });;
                    }
                }
                cn.Close();
            }
            return data;
        }
    }
}
