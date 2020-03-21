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
        [HttpGet]
        public IActionResult Create()
        {
            ContractCreateViewModel model = new ContractCreateViewModel();
            model.Consumers = context.Consumers.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            model.Resources = context.Resources.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContractCreateViewModel model)
        {
            Contract contract = new Contract
            {
                DateCreate = model.DateCreate,
                DateFinished = model.DateFinished,
                ResourceId = model.ResourceId,
                Price = model.Price,
                ConsumerId = model.ConsumerId
            };
            context.Contracts.Add(contract);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(ConsumerCreateViewModel model)
        //{
        //    string filename = Path.GetRandomFileName() + ".jpg";
        //    Consumer consumer = new Consumer
        //    {
        //        Name = model.Name,
        //        Address = model.Address,
        //        Image = filename
        //    };
        //    context.Consumers.Add(consumer);
        //    context.SaveChanges();
        //    await AddFile(model.Image, filename);
        //    return RedirectToAction("Index");
        //}

    }
}