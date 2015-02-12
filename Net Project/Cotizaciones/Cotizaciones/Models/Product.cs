using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [Display(Name = "ID del Proveedor")]
        public string ProviderId { get; set; }

        [Required]
        [Display(Name = "Proveedor")]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Familia de producto")]
        public string ProductFamily { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Precio del proveedor")]
        public string ProviderPrice { get; set; }

        [Display(Name = "Moneda del precio")]
        public string Currency { get; set; }

        [Display(Name = "Información Adicional")]
        public string AdditionalData { get; set; }

        [Display(Name = "Imágen")]
        public string ImageURL { get; set; }

        [Display(Name = "Productos Compatibles")]
        public virtual ICollection<Product> CompatibleProducts { get; set; }
    }
}