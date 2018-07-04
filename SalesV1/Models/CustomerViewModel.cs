using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesV1.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Customer Name")]
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        [Required]
        [StringLength(200)]
        public string CustomerAddress { get; set; }
    }
}