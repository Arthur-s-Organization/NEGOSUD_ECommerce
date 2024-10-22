using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Class.Customer;

namespace WPF.Class.CustomerOrder
{
    public class CustomerOrderResponseDTO
    {
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        //public Guid CustomerId { get; set; }

        public virtual CustomerResponseDTO Customer { get; set; }
        public virtual IEnumerable<OrderDetailResponseDTO> OrderDetails { get; set; }
    }
}
