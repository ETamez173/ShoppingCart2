using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart2.Models
{
    public class ShoppingItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String ProductName { get; set; }

        [Required]
        public String ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
