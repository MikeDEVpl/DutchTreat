using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;

        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any()) //zwraca true jesli cos jest w bazie
            {
                var filepath = Path.Combine(_hosting.ContentRootPath,"Data/art.json");
                //Need to create data
                var json = File.ReadAllText(filepath); // odczytuje json z produktami
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json); // deserializuje json do listy produktow
                _ctx.Products.AddRange(products); //dodaje produkty do bazy

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if(order != null)
                {
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _ctx.SaveChanges(); //zapisuje zmiany
            }

        }
    }
}
