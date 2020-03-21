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
}
