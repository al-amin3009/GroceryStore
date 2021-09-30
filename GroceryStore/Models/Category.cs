using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Title")]
        public string CategoryTitle { get; set; }
        [Required]
        public string CategoryPicture { get; set; }
        public int Offer { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
