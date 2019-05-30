using System;

namespace ConsoleApp
{
    public class OrderDetail
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public int Quantity { get; set; }
    }
}