using DutchTreat.Data.Entities;
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
