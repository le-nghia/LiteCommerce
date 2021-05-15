using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            /*var model = HRService.Supplier_list();*/

            /*   int rowCount = 0;
               int pageSize = 5; // Kichs thuoc trang. Đổi tên: ctrl + r + r

               var listofSuppliers = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);
               int pageCount = rowCount / pageSize;
               if (rowCount % pageSize > 0)
                   pageCount += 1;

               ViewBag.Page = page;
               ViewBag.RowCount = rowCount;
               ViewBag.PageCount = pageCount;
               ViewBag.SearchValue = searchValue;

               return View(listofSuppliers);*/

            return View();
            
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 5;

            var listofSuppliers = DataService.ListSuppliers(page, pageSize, searchValue, out int rowCount);

            var model = new Models.SupplierPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,

                Data = listofSuppliers

            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin nhà cung cấp";

            var model = DataService.GetSuppliers(id);
            if (model == null)
                RedirectToAction("Index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp mới";

            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View("Edit", model);
        }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ActionResult Delete(int id)
        {
            if(Request.HttpMethod == "POST")
            {
                // Xóa Supplier có mã ID
                DataService.DeleteSuppliers(id);

                // Quay lại trang Index.
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của Supplier cần xóa.
                var del = DataService.GetSuppliers(id);
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
        public ActionResult Save(Supplier data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("a", "Vui lòng nhập tên của nhà cung câp.");

                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("b", "Bạn chưa nhập tên liên hệ của nhà cung cấp.");

                if (string.IsNullOrEmpty(data.Address))
                {
                    data.Address = "";
                }

                if (string.IsNullOrEmpty(data.Country))
                {
                    data.Country = "";
                }
                if (string.IsNullOrEmpty(data.City))
                {
                    data.City = "";
                }
                if (string.IsNullOrEmpty(data.PostalCode))
                {
                    data.PostalCode = "";
                }
                if (string.IsNullOrEmpty(data.Phone))
                {
                    data.Phone = "";
                }

                if (!ModelState.IsValid)
                {
                    if (data.SupplierID == 0)
                        ViewBag.Title = "Tau đố mi bổ sung được.";
                    else
                        ViewBag.Title = "Hên là mi bổ sung đuọc rồi.";
                    return View("Edit", data);
                }


                if (data.SupplierID == 0)
                {
                    DataService.AddSupplier(data);
                }
                else
                {
                    DataService.UpdateSuppliers(data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Erorl! Trang này lừa đảo bạn êi.");
            }
            
        }

    }
}