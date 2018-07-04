using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace SalesV1.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }
        [Display (Name = "Product ")]
        public int ProductId { get; set; }
        [Display(Name = "Customer ")]
        public int CustomerId { get; set; }
        [Display(Name = "Store ")]
        public int StoreId { get; set; }
        [Display(Name = "Date ")]
        public System.DateTime SaleDate { get; set; }

        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
        public IEnumerable<SelectListItem> StoreList { get; set; }
    }
}