using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.core.Entites.Product
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public decimal Price { get; set; }
        public virtual List<Photos> Photos { get; set; } = new List<Photos>();
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } 
    }
}
