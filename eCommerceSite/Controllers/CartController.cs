using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id) // id of product to add
        {
            // get product from data base

            // add product to cart cookie

            // redirect to previous page
            return View();
        }

        public IActionResult Summary()
        {
            //Display all products in the shopping cart
            return View();
        }
    }
}
