using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public CartController(ProductContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Add the product to the shopping cart
        /// </summary>
        /// <param name="id">ProductId</param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id) // id of product to add
        {
            // get product from data base
            Product p = await ProductDb.GetProductAsync(_context, id);

            // add product to cart cookie
            _httpContext.HttpContext.Response.Cookies

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
