using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class Step
    {
        public int StepId { get; set; }

        [Required]
        [Display(Name = "Orden")]
        public int Order { get; set; }

        [Required]
        [Display(Name = "Paso")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Valor / Peso")]
        public int Value { get; set; }
    }
}