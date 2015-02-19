using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class OrderComment
    {
        public int OrderCommentId { get; set; }

        [Display(Name = "Comentario")]
        public string Comment { get; set; }

        [Display(Name = "Usuario")]
        public string User { get; set; }
        
        [Display(Name = "Fecha agregado")]
        public DateTime Date { get; set; }

        [Display(Name = "Paso")]
        public virtual Step Step { get; set; }
    }
}