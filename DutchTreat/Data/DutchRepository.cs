using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly ILogger<DutchRepository> _logger;
        private readonly DutchContext _ctx;
        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems) // include items bedzie parameterm w query stringu
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList(); // Include dodaje itemsy do wyjsciowej listy orderow
            }
            else
            {
                return _ctx.Orders
                    .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersbyUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList(); // Include dodaje itemsy do wyjsciowej listy orderow
            }
            else
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            //Przyklad poprawnego uzycia logowania
            try
            {
                return _ctx.Products
                       .OrderBy(p => p.Title)
                       .ToList();
            }catch(Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                 .Include(o => o.Items)
                 .ThenInclude(i => i.Product)
                 .Where(o => o.Id == id && o.User.UserName == username)
                 .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            _logger.LogInformation("Get products by cateory was called"); //poziom musi byc taki jak level domyslny w config.json
            return _ctx.Products
                   .Where(p => p.Category == category)
                   .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
