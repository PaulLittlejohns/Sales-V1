using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesV1.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Display(Name = "Product Price")]
        [Required]
        [Range(1,UInt32.MaxValue)]
        public Nullable<decimal> ProductPrice { get; set; }
    }
}