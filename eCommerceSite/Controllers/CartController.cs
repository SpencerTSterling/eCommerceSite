using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            const string CartCookie = "CartCookie";

            // get existing cart items
            string existingItems = _httpContext.HttpContext.Request.Cookies[CartCookie];

            List<Product> cartProducts = new List<Product>();

            if (existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }

            // add current product to existing cart
            cartProducts.Add(p);


            // add products to cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            // redirect to previous page
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Summary()
        {
            //Display all products in the shopping cart
            string cookieData = _httpContext.HttpContext.Request.Cookies["CartCookie"];

            List<Product> cartProducts = JsonConvert.DeserializeObject<List<Product>>(cookieData);

            return View(cartProducts);
        }
    }
}
