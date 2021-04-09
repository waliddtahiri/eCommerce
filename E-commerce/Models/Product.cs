using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ForeignKey("Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
