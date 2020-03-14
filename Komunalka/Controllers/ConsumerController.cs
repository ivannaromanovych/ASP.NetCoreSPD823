using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Komunalka.Entities;
using Komunalka.Models;
using Microsoft.AspNetCore.Mvc;

namespace Komunalka.Controllers
{
    public class ConsumerController : Controller
    {
        private readonly ApplicationDbContext context;
        public ConsumerController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<ConsumerViewModel> model = context.Consumers.Select(c => new ConsumerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
       [HttpPost]
        public IActionResult Create(ConsumerCreateViewModel model)
        {
            Consumer consumer = new Consumer
            {
                Name = model.Name,
                Address = model.Address
            };
            context.Consumers.Add(consumer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit( int id)
        {
            ConsumerEditViewModel model= context.Consumers.Select(c => new ConsumerEditViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address

            }).SingleOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ConsumerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Consumer consumer = context.Consumers.SingleOrDefault(x => x.Id == model.Id);
                consumer.Name = model.Name;
                consumer.Address = model.Address;
                context.SaveChanges();

            }            
            return RedirectToAction("Index");
        }


    }
}