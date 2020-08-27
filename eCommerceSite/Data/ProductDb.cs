using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
    public static class ProductDb
    {
        /// <summary>
        /// Returns the total count of products
        /// </summary>
        /// <param name="_context">Database context to use</param>
        /// <returns></returns>
        public async static Task<int> GetTotalProductsAsync(ProductContext _context)
        {
            return await(from p in _context.Products
                         select p).CountAsync();
        }

        /// <summary>
        /// Get a page worth of products
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="PageSize">Number of products per page</param>
        /// <param name="PageNum">Page or products to return</param>
        /// <returns></returns>
        public async static Task<List<Product>> GetProductsAsync(ProductContext _context, int PageSize, int PageNum)
        {
            return
                    await (from p in _context.Products
                           orderby p.Title ascending
                           select p)
                           .Skip(PageSize * (PageNum - 1)) // skip must be before take
                           .Take(PageSize)
                           .ToListAsync();
        }

        /// <summary>
        /// Adds a product to the database
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="p">The product being added to the database</param>
        /// <returns></returns>
        public async static Task<Product> AddProductAsync(ProductContext _context, Product p)
        {
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
            return p;
        }


        /// <summary>
        /// Saves edits to a product in the database
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="p">Product that is updated</param>
        /// <returns></returns>
        public async static Task<Product> EditProductAsync(ProductContext _context, Product p)
        {
            _context.Entry(p).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return p;
        }

        /// <summary>
        /// Deletes a product from the database
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="id">ProductId of the product being deleted</param>
        /// <returns></returns>
        public async static Task<Product> GetProductAsync(ProductContext _context, int id)
        {
            Product p =
                await (from prod in _context.Products
                       where prod.ProductId == id
                       select prod).SingleAsync();
            return p;
        }

        public async static Task<Product> DeleteProductAsync(ProductContext _context, Product p)
        {
            _context.Entry(p).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return p;
        }
    }
}
