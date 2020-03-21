using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Entities
{
    [Table("tblContracts")]
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime DateFinished { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Consumer")]
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
