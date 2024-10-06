﻿namespace API.Models.DTOs.RequestDTOs
{
    public class ItemRequestDTO
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string OriginCountry { get; set; }
        public Guid SupplierId { get; set; }
    }
}
