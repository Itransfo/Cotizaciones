using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class OrderProduct
    {
        public int OrderProductId { get; set; }
        
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public Nullable<int> Quantity { get; set;  }
    }
}