using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Identificador")]
        public Nullable<int> Identifier { get; set; }

        [Display(Name = "Pre-Proyecto")]
        public Nullable<int> Preproject { get; set; }

        [Display(Name = "Fecha de creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Descripción de la petición")]
        public string RequestDescription { get; set; }

        [Display(Name = "Cliente")]
        public virtual Client Client{ get; set; }

        [Display(Name = "Paso Actual")]
        public virtual Step Step { get; set; }

    }
}