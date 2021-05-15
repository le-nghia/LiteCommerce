using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
        public ActionResult Index()
        {
           /* int pageSize = 2;

            var listofShippers = DataService.ListShippers(page, pageSize, searchValue, out int rowCount);
            var model = new Models.ShipperPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,

                Data = listofShippers
            };*/
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 2;

            var listofShippers = DataService.ListShippers(page, pageSize, searchValue, out int rowCount);
            var model = new Models.ShipperPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,

                Data = listofShippers
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin nhà vận chuyển";
            var model = DataService.GetShippers(id);
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
            ViewBag.Title = "Bổ sung nhà vận chuyển mới";

            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            return View("Edit", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {          
            if (Request.HttpMethod == "POST")
            {
                DataService.DeleteShippers(id);

                return RedirectToAction("Index");

            }
            else
            {
                var del = DataService.GetShippers(id);
                if (del == null)
                    return RedirectToAction("Index");
                return View(del);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("a", "Vui lòng nhập tên nhà vận chuyển.");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("b", "Sao không nhập số điện thoại.");

                if (!ModelState.IsValid)
                {
                    if (data.ShipperID == 0)
                        ViewBag.Title = "Éo nào bổ sung được bạn êi.";
                    else
                        ViewBag.Title = "Thôi bổ sung đi.";
                    return View("Edit", data);
                }

                if (data.ShipperID == 0)
                {
                    DataService.AddShippers(data);
                }
                else
                {
                    DataService.UpdateShippers(data);
                }

                return RedirectToAction("Index");

            }
            catch
            {
                return Content("Đi chỗ khác chơi!!.");
            }
            
        }
    }
}