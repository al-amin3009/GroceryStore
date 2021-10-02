using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models.other
{
    public class CreateProducts
    {
        [Required]
        [RegularExpression(@"^[A-Z]+[0-9]*$")]
        [StringLength(5)]
        public string ProductNumber { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string Title { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Details { get; set; }

        public int? Offer { get; set; }

       
        public int CategoryId { get; set; }
    }
}
