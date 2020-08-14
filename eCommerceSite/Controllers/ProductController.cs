﻿using System;
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
        /// Displays a veiw that lists all products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            //Get all products out of database
            List<Product> products = await _context.Products.ToListAsync();

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
                _context.Products.Add(p);
                await _context.SaveChangesAsync();

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
            Product p =
                await (from prod in _context.Products
                       where prod.ProductId == id
                       select prod).SingleAsync();

            // Pass product to view
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ViewData["Message"] = "Product updated successfully";
            }
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product p =
               await (from prod in _context.Products
                     where prod.ProductId == id
                     select prod).SingleAsync();
            return View(p);
        }

    }
}
