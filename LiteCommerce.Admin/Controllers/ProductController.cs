﻿using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class ProductController : Controller
    {
        [Authorize]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="supplier"></param>
        /// <param name="searchValue"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult List(int categoryID = 0, int supplierID = 0, string searchValue = "", int page = 1)
        {
            try
            {
                int rowCount = 0;
                int pageSize = 5;
                var listProducts = ProductService.List(page, pageSize, categoryID, supplierID, searchValue, out rowCount);
                var model = new Models.ProductPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listProducts
                };

                return View(model);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {

            var model = ProductService.GetEx(id);
            if (model == null)
                return RedirectToAction("index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /*public ActionResult Editproduct (int id)
        {
            ViewBag.Title = "Thay đổi thông tin mặt hàng";

            var model = ProductService.GetEx(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditGaller(int id)
        {
            ViewBag.Title = "Thay đổi thuộc tính.";

            var model = ProductService.GetGallery(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Editattributes(int id)
        {
            ViewBag.Title = "Thay đổi thuộc tính.";

            var model = ProductService.GetAttribute(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeIds"></param>
        /// <returns></returns>
        public ActionResult DeleteAttrbutes(int id, long[] attributeIds)
        {
            //int id1 = id;
            if (attributeIds == null)
                return RedirectToAction("Edit", new { id = id });

            ProductService.DeleteAttribute(attributeIds);           
            return RedirectToAction("Edit", new { id = id });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="galleryIds"></param>
        /// <returns></returns>
        public ActionResult DeleteGaller(int id, long[] galleryIds)
        {
            //int id1 = id;
            if (galleryIds == null)
                return RedirectToAction("Edit", new { id = id });

            ProductService.DeleteGalleries(galleryIds);
            return RedirectToAction("Edit", new { id = id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Addproduct()
        {
            ViewBag.Title = "Bổ sung mặt hàng mới";
            Product model = new Product()
            {
                ProductID = 0
            };
            return View("Addproduct", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Addattributes(int id)
        {
            ViewBag.Title = "Bổ sung thuộc tính";
            
            ProductAttribute model = new ProductAttribute()
            {
                ProductID = id
                //AttributeID = 0
            };
            return View("Editattributes", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Addgalleries(int id)
        {
            ViewBag.Title = "Bổ sung thuộc tính";

            ProductGallery model = new ProductGallery()
            {
                ProductID = id
                //GalleryID = 0
            };
            return View("EditGaller", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                // Xóa Supplier có mã ID
                ProductService.Delete(id);

                // Quay lại trang Index.
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của Supplier cần xóa.
                var del = ProductService.Get(id);
                if (del == null)
                    return RedirectToAction("Index");

                // Trả thông tin về cho view hiển thị.
                return View(del);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Product data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ProductName))
                    ModelState.AddModelError("a", "Vui lòng nhập tên hàng.");

                if (string.IsNullOrWhiteSpace(data.Unit))
                    ModelState.AddModelError("b", "Vui lòng nhập đơn vị.");

                if (!ModelState.IsValid)
                {
                    if (data.SupplierID == 0)
                        ViewBag.Title = "Bổ sung mặt hàng.";
                    else
                        ViewBag.Title = "Cập nhật.";
                    //return RedirectToAction("Edit", new { id = data.ProductID });
                    return View("Addproduct", data);
                }

                if (data.ProductID == 0)
                {
                    int resufl = ProductService.Add(data);
                    return RedirectToAction("Edit", new { id = resufl });
                }
                else
                {
                    ProductService.Update(data);
                    return RedirectToAction("Edit", new { id = data.ProductID });
                }
             
            }
            catch
            {
                return Content("Lỗi Rồi.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(data.AttributeName))
                    ModelState.AddModelError("a", "Vui lòng nhập tên thuộc tính.");
                if (string.IsNullOrWhiteSpace(data.AttributeValue))
                    ModelState.AddModelError("b", "Vui lòng nhập giá trị thuộc tính.");
                
                if (!ModelState.IsValid)
                {
                    if (data.AttributeID == 0)
                        ViewBag.Title = "Không thể bổ sung được.";
                    else
                        ViewBag.Title = "Bổ sung thành công.";
                    return View("Editattributes", data);

                }
                if (data.AttributeID == 0)
                {
                    ProductService.AddAttribute(data);
                    
                }
                else
                {
                    ProductService.UpdateAttribute(data);
                    
                }

                return RedirectToAction("Edit", new { id = data.ProductID });
            }
            catch
            {
                return Content("Lỗi Rồi.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveGaller(ProductGallery data)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("b", "Vui lòng miêu tả.");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    ModelState.AddModelError("a", "Vui lòng thêm ảnh.");

                if (!ModelState.IsValid)
                {
                    if (data.GalleryID == 0)
                        ViewBag.Title = "Không thể bổ sung được.";
                    else
                        ViewBag.Title = "Bổ sung thành công.";
                    return View("EditGaller", data);
                }
                if (data.GalleryID == 0)
                {
                    ProductService.AddGallery(data);
                }
                else
                {
                    ProductService.UpdateGallery(data);
                }

                return RedirectToAction("Edit", new { id = data.ProductID });
            }
            catch
            {
                return Content("Lỗi Rồi.");
            }
        }
    }
}