using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    /// <summary>
    /// Represents individual product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique ID number for product
        /// </summary>
        [Key] // makes primary key in database
        public int ProductId { get; set; }
        /// <summary>
        /// Consumer name of the product
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The retail price as US currency
        /// </summary>
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        /// <summary>
        /// Category products fall under ex. Electronics, Furniture, etc...
        /// </summary>
        public string Category { get; set; }
    }
}
