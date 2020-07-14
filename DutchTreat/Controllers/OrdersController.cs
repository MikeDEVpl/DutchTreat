using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger, IMapper mapper )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true) // domyślnie jest true
        {
            try
            {
                var results = _repository.GetAllOrders(includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(results));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get orders :{ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if(order != null)
                {
                    return Ok(_mapper.Map<Order,OrderViewModel>(order)); //mapowanie order na ordeviewmodell
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order {id}: {ex}");
                return BadRequest($"Failed to get order {id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model) //Gdy nie ma from body bedzie brac ten model z query stringa
        {
            // add to database
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    //To co niżej zrobi automapper

                    //var newOrder = new Order()
                    //{
                    //    OrderDate = model.OrderDate,
                    //    OrderNumber = model.OrderNumber,
                    //    Id = model.OrderId

                    //};

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                       return Created($"/api/orders/{newOrder.Id}", newOrder); //zwraca 201
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
            }

            return BadRequest("Failed to save new order");
        }

    }
}
