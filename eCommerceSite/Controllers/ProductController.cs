﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            //Get all products out of database
            List<Product> products = _context.Products.ToList();

            //Send list of products to view to be displayed
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product p)
        {
            if(ModelState.IsValid)
            {
                //Add to DB
                _context.Products.Add(p);
                _context.SaveChanges();

                // success message
                TempData["Message"] = $"{p.ProductId}:{p.Title} was added successfully";

                // redirect to catalog page
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
