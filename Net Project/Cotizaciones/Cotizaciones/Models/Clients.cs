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

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre(s)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Empresa")]
        public string Company { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Ciudad / Municipio")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        public string State { get; set; }

        [Display(Name = "País")]
        public string Country { get; set; }
        
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Extensión")]
        public Nullable<int> Extension { get; set; }

        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Tipo de Cliente")]
        public string Category { get; set; }

        public string getFullName()
        {
            return string.Format("{0} {1}",Name,LastName);
        }
    }
}