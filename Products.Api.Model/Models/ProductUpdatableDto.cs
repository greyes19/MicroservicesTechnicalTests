﻿namespace Products.Api.Services.Models
{
    public class ProductUpdatableDto
    {
        public string? Name { get; set; }
        public int? LotNumber { get; set; }
        public decimal? Cost { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
