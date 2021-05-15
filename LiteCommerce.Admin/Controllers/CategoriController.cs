using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CategoriController : Controller
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
        public ActionResult List(int page = 1, string searchValue = "")
        {

            int pageSize = 5;

            var listofCategoris = DataService.ListCategoris(page, pageSize, searchValue, out int rowCount);

            var model = new Models.CategoriPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,

                Data = listofCategoris

            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin loại hàng";

            var model = DataService.GetCategoris(id);
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
            ViewBag.Title = "Bổ sung loại hàng mới";

            Categori model = new Categori()
            {
                CategoryID = 0
            };
            return View("Edit",model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                // Xóa Categori có mã ID
                DataService.DeleteCategori(id);

                // Quay lại trang Index.
                return RedirectToAction("Index");
            }
            else
            {
                // Lấy thông tin của Categori cần xóa.
                var del = DataService.GetCategoris(id);
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
        public ActionResult Save(Categori data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("a", "Nhập tên hàng kẹ chết!");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("b", "Nhập mô tả bạn êi.");
                
                if (!ModelState.IsValid)
                {
                    if (data.CategoryID == 0)
                        ViewBag.Title = "Không bổ sung được rồi.";
                    else
                        ViewBag.Title = "Bổ sung thành công.";
                    return View("Edit", data);
                }

                if (data.CategoryID == 0)
                {
                    DataService.AddCategori(data);
                }
                else
                {
                    DataService.UpdateCategori(data);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Quay đầu đi bạn. Lỗi rồi.");
            }
           
        }
    }
}