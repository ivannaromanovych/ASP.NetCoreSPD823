using Komunalka.Entities;
using Komunalka.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext context;
        public ContractsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<ContractViewModel> model = context.Contracts.Select(c => new ContractViewModel
            {
                Id = c.Id,
                Consumer = c.Consumer.Name,
                ConsumerImage = "/Image/75_" + c.Consumer.Image,
                ResourceUnit = c.Resource.Units,
                Price = c.Price,
                DateCreate = c.DateCreate,
                DateFinished = c.DateFinished,
                Resource = c.Resource.Name
            }).ToList();
            return View(model);
        }
        //public IActionResult Index()
        //{
        //    List<ConsumerViewModel> model = context.Consumers.Select(c => new ConsumerViewModel
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Address = c.Address,
        //        Photo = "/Image/75_" + c.Image,
        //    }).ToList();
        //    return View(model);
        //}

    }
}