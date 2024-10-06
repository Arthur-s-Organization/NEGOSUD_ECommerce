namespace API.Models.DTOs.RequestDTOs
{
    public class SupplierOrderRequestDTO
    {
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public Guid SupplierId { get; set; }
    }
}
