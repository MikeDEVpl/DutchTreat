using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;

        public AppController(IMailService mailService,IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]  //Ręczny routing - teraz nie jest App/Contact tylko sam contact
        public IActionResult Contact()
        {
          return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)    //Wszystki annotacje modelu są OK
            {
                //Send the email
                _mailService.SendMessage("mojadres@ww.lk", model.Subject, $"From: {model.Name} - {model.Email}, Message:{model.Message} ");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear(); // Czyści formularz
            }
            else
            {
                //Show error
            }
            return View();

        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            //var results = _context.Products
            //    .OrderBy(p => p.Category)
            //    .ToList();
            //To samo w linq - to List można dać pózniej wtedy results jest jakby tylko query
            //var results = from p in _context.Products
            //              orderby p.Category
            //               select p;

            //niepotrzebne po dodaniu angulara
            //var results = _repository.GetAllProducts();
            //return View(results);

            return View();
        }
    }
}
