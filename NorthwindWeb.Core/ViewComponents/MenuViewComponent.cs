using Microsoft.AspNetCore.Mvc;
using NorthwindWeb.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindWeb.Core.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly NorthwindDatabase _northwindDatabase;

        public MenuViewComponent(NorthwindDatabase context)
        {
            _northwindDatabase = context;
        }

        /// <summary>
        /// Used to construct the menu.
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var productsCategories = _northwindDatabase.Categories;
            List<string> listOfCategories = new List<string>();

            try
            {
                foreach (var item in productsCategories)
                {
                    string categoryName = item.CategoryName;
                    listOfCategories.Add(categoryName);
                }

                return View(listOfCategories);

            }
            catch (Exception exception)
            {
                //logger.Error(exception.ToString());
                return View();
            }
        }
    }
}
