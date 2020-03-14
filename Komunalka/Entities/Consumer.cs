using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Entities
{
    [Table("tblConsumers")]
    public class Consumer
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        [Required, StringLength(1000)]
        public string Address { get; set; }

        [StringLength(1000)]
        public string Image { get; set; }        
    }
}
