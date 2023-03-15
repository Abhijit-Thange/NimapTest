using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    [Table("Product")]
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        
       // [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

       // public virtual Category Category { get; set; }
    }
}