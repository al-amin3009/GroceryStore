using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models.other
{
    public class CreateCategory
    {
        [Required(ErrorMessage = "You must provide a Category title")]
        [DisplayName("Category Title")]
        public string CategoryTitle { get; set; }

        [Required(ErrorMessage = "You must provide a picture")]
        [DisplayName("Picture")]
        public IFormFile CategoryPicture { get; set; }

        public int? Offer { get; set; }
    }
}
