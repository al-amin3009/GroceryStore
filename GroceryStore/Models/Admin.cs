using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must provide the username")]
        [DisplayName("User Name")]
        public string userName { get; set; }

        [Required(ErrorMessage = "You must provide the password")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}
