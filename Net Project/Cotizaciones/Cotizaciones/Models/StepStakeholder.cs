using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class StepStakeholder
    {

        public int StepStakeholderId { get; set; }

        public string  Stakeholder { get; set; }
        
        public virtual Step Step { get; set; }
    }
}