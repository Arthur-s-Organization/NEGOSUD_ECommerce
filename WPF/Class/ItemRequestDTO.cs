using Microsoft.AspNetCore.Http;

namespace WPF.Class
{
    public class ItemRequestDTO
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string OriginCountry { get; set; }
        public Guid SupplierId { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public string? AlcoholVolume { get; set; }
        public string? Year { get; set; }
        public float? Capacity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid? AlcoholFamilyId { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
