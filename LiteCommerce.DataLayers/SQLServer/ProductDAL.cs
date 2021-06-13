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
    /// Cài đặt các tính năng xử lý dữ liệu mặt hàng trong csdl sql server
    /// </summary>
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int productID = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Products(
                                      ProductName, SupplierID, CategoryID, 
                                       Unit, Price, Photo)
                                       VALUES(
                                        @ProductName, @SupplierID, @CategoryID,
                                        @Unit, @Price, @Photo );
                                        SELECT @@IDENTITY;";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                productID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }

            return productID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddAttribute(ProductAttribute data)
        {
            int attributeID = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes(
                                      ProductID, AttributeName, AttributeValue, 
                                       DisplayOrder)
                                       VALUES(
                                        @ProductID, @AttributeName, @AttributeValue,
                                        @DisplayOrder);
                                        SELECT @@IDENTITY;";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);


                attributeID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }

            return attributeID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddGallery(ProductGallery data)
        {
            int galleryID = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductGallery(
                                      ProductID, Photo, Description, 
                                       DisplayOrder, IsHidden)
                                    VALUES(
                                        @ProductID, @Photo, @Description,
                                        @DisplayOrder, @IsHidden);
                                        SELECT @@IDENTITY;";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                galleryID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }

            return galleryID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(int categoryID, int supplierID, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%"; //Tim kiem tuong doi.

            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) FROM  Products
                                    WHERE   (@categoryId = 0 OR CategoryId = @categoryId)
                                            AND  (@supplierId = 0 OR SupplierId = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)";
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryId", categoryID);
                cmd.Parameters.AddWithValue("@supplierId", supplierID);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();

            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool Delete(int productID)
        {
            
                bool result = false;

                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = @"DELETE FROM Products  WHERE ProductID = @ProductID AND NOT EXISTS
                                   ( 
                                    SELECT * FROM OrderDetails
                                    where ProductID = Products.ProductID and ProductID = Products.ProductID )";

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    result = cmd.ExecuteNonQuery() > 0;
                    cn.Close();
                }

                return result;
                        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeId)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes WHERE AttributeID = @AttributeID";

                cmd.Parameters.AddWithValue("@AttributeID", attributeId);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public bool DeleteGallery(long galleryId)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductGallery WHERE GalleryID = @GalleryID";

                cmd.Parameters.AddWithValue("@GalleryID", galleryId);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product Get(int productID)
        {
            Product data = null;

            using (SqlConnection cn = GetConnection())  // kết nối sql
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @ProductID";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", productID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())    // Nếu có dữ liệu thì. (dùng if vì chỉ truy vấn sql ra dc 1 dòng)
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"])
                        };
                    }
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeId)
        {
            ProductAttribute data = null;

            using (SqlConnection cn = GetConnection())  // kết nối sql
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @AttributeID";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AttributeID", attributeId);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())    // Nếu có dữ liệu thì. (dùng if vì chỉ truy vấn sql ra dc 1 dòng)
                    {
                        data = new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        };
                    }
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public ProductEx GetEx(int productID)
        {
            // có thêm danh sách của product attribute + product Gallery
            Product product = Get(productID);
            if (product == null)
            {
                return null;
            }


            List<ProductAttribute> listAttributes = this.ListAttributes(productID);
            List<ProductGallery> listGallery = this.ListGalleries(productID);

            ProductEx data = new ProductEx()
            {
                ProductID = product.ProductID,
                CategoryID = product.CategoryID,
                Photo = product.Photo,
                Price = product.Price,
                ProductName = product.ProductName,
                SupplierID = product.SupplierID,
                Unit = product.Unit,

                Attributes = listAttributes,
                Galleries = listGallery
            };

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public ProductGallery GetGallery(long galleryId)
        {
            ProductGallery data = null;

            using (SqlConnection cn = GetConnection())  // kết nối sql
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ProductGallery WHERE GalleryID = @GalleryID";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@GalleryID", galleryId);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())    // Nếu có dữ liệu thì. (dùng if vì chỉ truy vấn sql ra dc 1 dòng)
                    {
                        data = new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToBoolean(dbReader["IsHidden"])
                        };
                    }
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Product> List(int page, int pageSize, int categoryID, int supplierID, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<Product> data = new List<Product>();
            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText= @"SELECT * FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM    Products 
                                        WHERE   (@categoryID = 0 OR CategoryID = @categoryId)
                                            AND  (@supplierID = 0 OR SupplierID = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)                                          
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
               

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Unit = Convert.ToString(dbReader["Unit"])

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
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM ProductAttributes WHERE ProductID = @productId";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@productId", productID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
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
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductGallery> ListGalleries(int productId)
        {
            List<ProductGallery> data = new List<ProductGallery>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM ProductGallery WHERE ProductID = @productId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@productId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToBoolean(dbReader["IsHidden"])
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
        public bool Update(Product data)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Products 
                                        SET ProductName = @ProductName,
                                        SupplierID = @SupplierID,
                                        CategoryID = @CategoryID,
                                        Unit = @Unit,
                                        Price = @Price,
                                        Photo =@Photo
                                        where ProductID = @ProductID;";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductAttributes 
                                        SET ProductID = @ProductID,
                                        AttributeName = @AttributeName,
                                        AttributeValue = @AttributeValue,
                                        DisplayOrder = @DisplayOrder
                                        where AttributeID = @AttributeID;";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateGallery(ProductGallery data)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductGallery 
                                        SET ProductID = @ProductID,
                                        Photo = @Photo,
                                        Description = @Description,
                                        DisplayOrder = @DisplayOrder,
                                        IsHidden = @IsHidden
                                        where GalleryID = @GalleryID;
                                        ";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@GalleryID", data.GalleryID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                result = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }

            return result;
        }
    }
}
