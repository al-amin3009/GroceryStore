using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[0-9]*$")]
        [DisplayName("Products Number")]
        [StringLength(5)]
        public string ProductNumber { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Item Picture")]
        public string Picture { get; set; }

        public decimal Price { get; set; }

        [Required]
        [DisplayName("Details")]
        public string Details { get; set; }

        public int Offer { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
