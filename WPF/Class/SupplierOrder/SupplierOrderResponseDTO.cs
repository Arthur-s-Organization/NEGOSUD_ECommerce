using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Class.Supplier;

namespace WPF.Class.SupplierOrder
{
    public class SupplierOrderResponseDTO
    {
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        //public Guid SupplierId { get; set; }
        public virtual SupplierResponseDTO Supplier { get; set; }
        public virtual IEnumerable<OrderDetailResponseDTO> OrderDetails { get; set; }
    }
}
