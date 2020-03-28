using Komunalka.Entities;
using Komunalka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            model.Consumers = context.Consumers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            model.Resources = context.Resources.Select(c => new SelectListItem
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
        public IActionResult Create(ContractCreateViewModel model)
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");
            ContractEditViewModel model = context.Contracts.Select(c => new ContractEditViewModel
            {
                Id = c.Id,
                DateCreate = c.DateCreate.ToString("dd.MM.yyyy", culture),
                DateFinished = c.DateFinished.ToString("dd.MM.yyyy", culture),
                Price = c.Price,
                ConsumerId = c.ConsumerId,
                ResourceId = c.ResourceId
            }).SingleOrDefault(x => x.Id == id);
            model.Consumers = context.Consumers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            model.Resources = context.Resources.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ContractEditViewModel model)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");
            if (ModelState.IsValid)
            {
                Contract contract = context.Contracts.SingleOrDefault(x => x.Id == model.Id);
                contract.DateCreate = DateTime.Parse(model.DateCreate, culture);
                contract.DateFinished = DateTime.Parse(model.DateFinished, culture);
                contract.Price = model.Price;
                contract.ConsumerId = model.ConsumerId;
                contract.ResourceId = model.ResourceId;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-UA");
            ContractViewModel model = context.Contracts.Select(c => new ContractViewModel
            {
                Id = c.Id,
                DateCreate = c.DateCreate.ToString("dd.MM.yyyy", culture),
                DateFinished = c.DateFinished.ToString("dd.MM.yyyy", culture),
                Price = c.Price,
                Consumer = c.Consumer.Name,
                ConsumerImage = c.Consumer.Image,
                Resource = c.Resource.Name,
                ResourceUnit = c.Resource.Units,
            }).SingleOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(ContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                Contract contract = context.Contracts.SingleOrDefault(x => x.Id == model.Id);
                context.Contracts.Remove(contract);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}