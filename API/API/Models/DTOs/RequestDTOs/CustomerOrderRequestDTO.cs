namespace API.Models.DTOs.RequestDTOs
{
    public class CustomerOrderRequestDTO
    {
        public string Status { get; set; }

        public Guid CustomerId { get; set; }
    }
}
