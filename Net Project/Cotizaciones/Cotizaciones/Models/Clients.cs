using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        
        [Required]
        [Display(Name = "Nombre(s)")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public string Company { get; set; }

        [Display(Name = "Ciudad / Municipio")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        public string State { get; set; }

        [Display(Name = "País")]
        public string Country { get; set; }
        
        [Display(Name = "Teléfonos")]
        [DataType(DataType.PhoneNumber)]
        public List<string> Phone { get; set; }

        [Display(Name = "Email(s)")]
        [DataType(DataType.EmailAddress)]
        public List<string> Email { get; set; }

        [Display(Name = "Tipo de Cliente")]
        public string Category { get; set; }
    }
}