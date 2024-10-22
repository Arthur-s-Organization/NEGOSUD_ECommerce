using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Class.SupplierOrder
{
    public class SupplierOrderRequestDTO
    {
        public string Status { get; set; }
        public Guid SupplierId { get; set; }
    }
}
