using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
	public class StepChange
	{
        public int StepChangeId { get; set; }

        [Display(Name = "Fecha de cambio")]
        public DateTime DateChanged { get; set; }

        [Display(Name = "Orden")]
        public virtual Order Order { get; set; }

        [Display(Name = "Cambiado de")]
        public int PreviousStepId { get; set; }

        [Display(Name = "Cambiado a")]
        public int NextStepId { get; set; }

        [Display(Name = "Cambiado por")]
        public string User { get; set; }
	}
}