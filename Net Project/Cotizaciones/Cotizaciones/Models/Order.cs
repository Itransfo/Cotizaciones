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
        public string Identifier { get; set; }

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

        public virtual ICollection<StepChange> StepChanges { get; set; }

        public virtual ICollection<OrderComment> OrderComments { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public string onTime()
        {
            var stepChanges = StepChanges.Where(s => s.NextStepId.Equals(Step.StepId));
            if (stepChanges.Count() > 0)
            {
                StepChange stepChange = stepChanges.ElementAt(0);
                if (Step.Tolerance < (DateTime.Now - stepChange.DateChanged).TotalHours)
                    return "Atrasado";
                else
                    return "A tiempo";
            }
            else
            {
                if (Step.Tolerance < (DateTime.Now - DateCreated).TotalHours)
                    return "Atrasado";
                else
                    return "A tiempo";
            }
        }

        public decimal getTotal()
        {
            decimal result = 0;
            foreach(OrderProduct orderProduct in OrderProducts)
            {
                if (orderProduct.Product.ProviderPrice != null)
                    result += (decimal) (orderProduct.Quantity * orderProduct.Product.ProviderPrice);
            }
            return result;
        }
    }
}