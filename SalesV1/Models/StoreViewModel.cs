using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesV1.Models
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Store Name")]
        [Required]
        [StringLength(100)]
        public string StoreName { get; set; }
        [Display(Name = "Store Address")]
        [Required]
        [StringLength(200)]
        public string StoreAddress { get; set; }
    }
}