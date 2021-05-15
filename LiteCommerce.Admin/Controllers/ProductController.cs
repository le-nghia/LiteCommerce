using LiteCommerce.BusinessLayers;
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

        public ActionResult List(int category = 0, int supplier = 0, string searchValue = "", int page=1)
        {
            try
            {
                int rowCount = 0;
                int pageSize = 5;
                var listProducts = ProductService.List(page, pageSize, category, supplier, searchValue, out rowCount);
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
        public ActionResult Editproduct (int id)
        {
            ViewBag.Title = "Thay đổi thông tin mặt hàng";

            var model = ProductService.Get(id);
            if (model == null)
                return RedirectToAction("index");
            return View(model);
        }
        public ActionResult EditGaller(int id)
        {
            ViewBag.Title = "Thay đổi thuộc tính.";

            var model = ProductService.GetGallery(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Editattributes(int id)
        {
            ViewBag.Title = "Thay đổi thuộc tính.";

            var model = ProductService.GetAttribute(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult DeleteAttrbutes(int id, long[] attributeIds)
        {
            if (attributeIds == null)
                return RedirectToAction("Edit", new {id });

            ProductService.DeleteAttribute(attributeIds);           
            return RedirectToAction("Edit", new { id });

        }

        public ActionResult DeleteGaller(int id, long[] galleryIds)
        {
            if (galleryIds == null)
                return RedirectToAction("Edit", new { id });

            ProductService.DeleteGalleries(galleryIds);
            return RedirectToAction("Edit", new { id });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Addproduct()
        {
            ViewBag.Title = "Bổ sung mặt hàng mới";

            ViewBag.Title = "Bổ sung thuộc tính";
            Product model = new Product()
            {
                ProductID = 0
            };
            return View("Editproduct", model);
        }
        public ActionResult Addattributes()
        {
            ViewBag.Title = "Bổ sung thuộc tính";
            ProductAttribute model = new ProductAttribute()
            {
                AttributeID = 0
            };
            return View("Editattributes", model);
        }
        public ActionResult Addgalleries()
        {
            ViewBag.Title = "Bổ sung thuộc tính";
            ProductGallery model = new ProductGallery()
            {
                GalleryID = 0
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
                        ViewBag.Title = "Tau đố mi bổ sung được.";
                    else
                        ViewBag.Title = "Hên là mi bổ sung đuọc rồi.";
                    return View("Editproduct", data);
                }
                if (data.ProductID == 0)
                {
                    ProductService.Add(data);
                }
                else
                {
                    ProductService.Update(data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi Rồi.");
            }
        }
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
                        ViewBag.Title = "Tau đố mi bổ sung được.";
                    else
                        ViewBag.Title = "Hên là mi bổ sung đuọc rồi.";
                    return View("Editattributes", data);
                }
                if (data.ProductID == 0)
                {
                    ProductService.AddAttribute(data);
                }
                else
                {
                    ProductService.UpdateAttribute(data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi Rồi.");
            }
        }
    }
}