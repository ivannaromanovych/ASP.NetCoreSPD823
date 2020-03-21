using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Models
{
    public class ContractViewModel
    {
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateFinished { get; set; }

        public decimal Price { get; set; }

        public string Consumer { get; set; }
        public string ConsumerImage { get; set; }

        public string Resource { get; set; }
        public string ResourceUnit { get; set; }
    }
    public class ContractCreateViewModel
    {
        public DateTime DateCreate { get; set; }
        public DateTime DateFinished { get; set; }

        public decimal Price { get; set; }

        public int ConsumerId { get; set; }
        public List<SelectListItem> Consumers { get; set; }
        public int ResourceId { get; set; }
        public List<SelectListItem> Resources { get; set; }


    }
}
