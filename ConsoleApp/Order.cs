using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
    }
}
