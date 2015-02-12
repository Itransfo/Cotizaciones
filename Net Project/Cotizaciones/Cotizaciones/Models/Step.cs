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

        [Required]
        [Display(Name = "Responsable")]
        public string Responsible { get; set; }

        [Display(Name = "Tolerancia(hrs)")]
        public int Tolerance { get; set; }

        [Display(Name = "Recordatorio(hrs)")]
        public int Reminder { get; set; }


        public virtual ICollection<StepStakeholder> StepStakeholders { get; set; }

    }
}