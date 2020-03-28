using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Models
{
    public class ContractViewModel
    {
        public int Id { get; set; }
        [Display(Name="Дата заключення")]
        public string DateCreate { get; set; }
        [Display(Name = "Дата закінчення")]
        public string DateFinished { get; set; }
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Постачальник")]
        public string Consumer { get; set; }
        [Display(Name = "Постачальник фото")]
        public string ConsumerImage { get; set; }
        [Display(Name = "Послуга")]
        public string Resource { get; set; }
        [Display(Name = "Одиниці")]
        public string ResourceUnit { get; set; }
    }
    public class ContractCreateViewModel
    {
        [Display(Name = "Дата заключення")]
        public string DateCreate { get; set; }
        [Display(Name = "Дата закінчення")]
        public string DateFinished { get; set; }
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Постачальник")]
        public int ConsumerId { get; set; }
        public List<SelectListItem> Consumers { get; set; }
        [Display(Name = "Послуга")]
        public int ResourceId { get; set; }
        public List<SelectListItem> Resources { get; set; }


    }
}
