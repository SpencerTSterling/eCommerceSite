using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a veiw that lists a page products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            int PageNum = id ?? 1;
            const int PageSize = 3;

            int numProducts = await ProductDb.GetTotalProductsAsync(_context);
            int totalPages = (int)Math.Ceiling( (double)numProducts / PageSize );

            ViewData["MaxPage"] = totalPages;
            ViewData["CurrentPage"] = PageNum;

            //Get all products out of database
            List<Product> products = await ProductDb.GetProductsAsync(_context, PageSize, PageNum);

            //Send list of products to view to be displayed
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product p)
        {
            if(ModelState.IsValid)
            {
                //Add to DB
                await ProductDb.AddProductAsync(_context, p);

                // success message
                TempData["Message"] = $"{p.Title} was added successfully";

                // redirect to catalog page
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Get product with corressponding id
            Product p = await ProductDb.SelectProductAsync(_context, id);

            // Pass product to view
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (Product p)
        {
            if (ModelState.IsValid)
            {
                await ProductDb.EditProductAsync(_context, p);
                ViewData["Message"] = "Product updated successfully";
            }
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product p = await ProductDb.SelectProductAsync(_context, id);
            return View(p);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            Product p = await ProductDb.SelectProductAsync(_context, id);

            await ProductDb.DeleteProductAsync(_context, p);

            TempData["Message"] = $"{p.Title} was deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
