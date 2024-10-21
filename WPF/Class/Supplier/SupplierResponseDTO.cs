using WPF.Class.Adrsess;

namespace WPF.Class.Supplier
{
    public class SupplierResponseDTO
    {
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual AddressResponseDTO Address { get; set; }
    }
}
