using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx,
            IWebHostEnvironment hosting, 
            UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("shawn@dutchtreat.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Shawn",
                    LastName = "Wilder",
                    Email = "shawn@dutchtreat.com",
                    UserName = "shawn@dutchtreat.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!"); //tworzy usera z haslrm
                
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user");
                }
            
            }

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
                    order.User = user;
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
