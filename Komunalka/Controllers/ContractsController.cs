using Komunalka.Entities;
using Komunalka.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");

            List<ContractViewModel> model = context.Contracts
                //.ToList()
                .Select(c => new ContractViewModel
            {
                Id = c.Id,
                Consumer = c.Consumer.Name,
                ConsumerImage = "/Image/75_" + c.Consumer.Image,
                ResourceUnit = c.Resource.Units,
                Price = c.Price,
                DateCreate = c.DateCreate.ToString("dd.MM.yyyy", culture),
                DateFinished = c.DateFinished.ToString("dd.MM.yyyy", culture),
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
            CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");
            model.DateCreate = DateTime.Now.ToString("dd.MM.yyyy", culture);
            model.DateFinished = DateTime.Now.AddYears(1).ToString("dd.MM.yyyy", culture);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContractCreateViewModel model)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");
            Contract contract = new Contract
            {
                DateCreate = DateTime.Parse(model.DateCreate, culture),
                DateFinished = DateTime.Parse(model.DateFinished, culture),
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