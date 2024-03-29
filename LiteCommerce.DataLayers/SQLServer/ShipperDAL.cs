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
    /// Cài đặt các tính năng xử lý dữ liệu Nhà vận chuyển trong csdl sql server
    /// </summary>
    public class ShipperDAL : _BaseDAL, IShipperDAL
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Shipper data)
        {
            int shipperID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Shippers(ShipperName, Phone)
                                    VALUES (@ShipperName, @Phone);
                                    SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                shipperID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return shipperID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }

            int result = 0;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) FROM Shippers
                                    where (@searchValue = '') OR
                                        (
                                            ShipperName LIKE @searchValue
							                OR Phone LIKE @searchValue							                
                                        )";
                cmd.Parameters.AddWithValue(@"searchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public bool Delete(int shipperID)
        {
            bool result = false;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Shippers
                                  WHERE ShipperID = @ShipperID";

                cmd.Parameters.AddWithValue(@"shipperID", shipperID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public Shipper Get(int shipperID)
        {
            Shipper data = null;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Shippers WHERE ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue(@"ShipperID", shipperID);

                using(SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
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
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Shipper> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            List<Shipper> data = new List<Shipper>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from (
	                                select *, ROW_NUMBER() OVER (ORDER BY ShipperName) AS RowNumber
	                                from Shippers
	                                where (@searchValue = '') OR (ShipperName LIKE @searchValue							  							                                  
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


                        data.Add(new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            

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
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Shipper data)
        {
            bool result = false;
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Shippers SET
                                        ShipperName = @ShipperName,
                                        Phone = @Phone
                                    WHERE ShipperID = @ShipperID;";

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = cmd.ExecuteNonQuery() > 0;

            }
            return result;
        }
        /*private string connectionString;

public ShipperDAL(string connectionString)
{
   this.connectionString = connectionString;
}
public List<Shipper> List()
{
   List<Shipper> data = new List<Shipper>();
   using (SqlConnection cn = new SqlConnection())
   {
       cn.ConnectionString = this.connectionString;
       cn.Open();

       SqlCommand cmd = new SqlCommand();
       cmd.CommandText = " SELECT * FROM Shippers";
       cmd.CommandType = System.Data.CommandType.Text;
       cmd.Connection = cn;
       using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
       {
           while (dbReader.Read())
           {
               data.Add(new Shipper()
               {
                   ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                   ShipperName = Convert.ToString(dbReader["ShipperName"]),
                   Phone = Convert.ToString(dbReader["Phone"])                          

               });
           }
       }

       cn.Close();
   }
   return data;
}*/
    }
}
