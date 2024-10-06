namespace API.Models.DTOs.RequestDTOs
{
    public class CustomerOrderRequestDTO
    {
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public Guid CustomerId { get; set; }
    }
}
