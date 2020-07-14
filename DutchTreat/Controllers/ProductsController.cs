using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    //Annotacje i ACtionResult<> dla maksymalnego zabez\pieczenia publicznego api
    [ApiController]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase 
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]  
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>>  Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts()); //Zwraca 200 i liste produktów
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products"); // Zwraca 400
            }
        }
    }
}
