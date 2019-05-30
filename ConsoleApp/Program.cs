using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ConsoleDbContext>()
                .UseSqlServer(@"Server=.;Database=Blogging;Integrated Security=True")
                .Options;
            using (var dbContext =new ConsoleDbContext(options))
            {
                var details = dbContext.OrderDetails.AsQueryable();
                var result = from order in dbContext.Orders
                    select new
                    {
                        order.Id,
                        order.Code,
                        MaxQuantity = details.Where(d => d.OrderId == order.Id).Max(d => (int?)d.Quantity)
                    };
                foreach (var item in result.Where(i => i.MaxQuantity > 100))
                {
                    Console.WriteLine($"{item.Code}, {item.MaxQuantity}");
                }
            }
        }
    }
}
