using Ecommerce.core.Entites.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.core.DTOs.Requests
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFileCollection Photos { get; set; } 
        [Required]
        public int? CategoryId { get; set; }
    }
}
