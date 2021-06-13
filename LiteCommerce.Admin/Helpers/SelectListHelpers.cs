using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{ 
    /// <summary>
    /// Cung cap cac tien ich lien quan den selectListItem
    /// </summary>
    public static class SelectListHelpers
    {
        /// <summary>
        /// Tra ve danh sach cac quoc gia ( Duoi dang SelectList).
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach(var item in DataService.ListCoutries())
            {
                list.Add(new SelectListItem() 
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            return list;
        }
        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in DataService.ListCities())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CityName,
                    Text = item.CityName
                });
            }
            return list;
        }
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListNameCategoryes())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Supplers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListNameSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }
    }
}