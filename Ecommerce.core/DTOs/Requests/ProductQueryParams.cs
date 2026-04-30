using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.core.DTOs.Requests
{
    public class ProductQueryParams
    {
        public string? Search { get; set; }

        // Filters
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStock { get; set; }

        // Sorting
        public string SortBy { get; set; } = "name";       // name, price, createdAt
        public string SortDir { get; set; } = "asc";       // asc, desc

        // Pagination
        private int _pageSize = 10;
        public int Page { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 50 ? 50 : value;    // max 50 per page
        }
    }
}
