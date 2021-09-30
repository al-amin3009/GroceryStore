using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStore.Models.other
{
    public class EditCategory : CreateCategory
    {
        public int Id { get; set; }
        public string ExistPath { get; set; }
    }
}
