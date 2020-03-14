using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Komunalka.Models
{
    public class ConsumerViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Постачальник")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Адреса")]
        public string Address { get; set; }
    }
    public class ConsumerCreateViewModel
    {
        [Required]
        [Display(Name = "Постачальник")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Адреса")]
        public string Address { get; set; }
    }
}
