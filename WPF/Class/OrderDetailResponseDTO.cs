using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Class.Item;

namespace WPF.Class
{
    public class OrderDetailResponseDTO
    {
        //public Guid ItemId { get; set; }
        public virtual ItemResponseDTO Item { get; set; }
        public int Quantity { get; set; }
    }
}
