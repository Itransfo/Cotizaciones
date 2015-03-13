using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotizaciones.Models
{
    public class DocumentItem
    {
        public string item { get; set; }

        public string description { get; set; }

        public string quantity { get; set; }

        public string unitPrice { get; set; }

        public string price { get; set; }

        public DocumentItem()
        {
            item        = string.Empty;
            description = string.Empty;
            quantity    = string.Empty;
            unitPrice   = string.Empty;
            price       = string.Empty;
        }
    }

    public class DocumentContent
    {
        public string title { get; set; }
        public string author { get; set; }
        public string fileName { get; set; }
        public string quoteCode { get; set; }

        public string clientCompany { get; set; }
        
        public string clientFullName { get; set; }

        public string clientPosition { get; set; }

        public string clientPhoneNumber { get; set; }

        public string clientPhoneExtension { get; set; }

        public string clientEmail { get; set; }

        public string quoteNumber { get; set; }

        public string date { get; set; }
        
        public string validUntilDate { get; set; }

        public string currency { get; set; }

        public string changeRate { get; set; }

        public string payConditions { get; set; }

        public string deliveryTime { get; set; }

        public string deliveryPlace { get; set; }

        public List<DocumentItem> items { get; set; }

        public string totalCurrency { get; set; }

        public string total { get; set; }

        public string totalWritten { get; set; }

        public string taxNote { get; set; }

        public string notes { get; set; }

        public DocumentContent()
        {
            title = "Sample PDF";
            author = "Author";
            fileName = "Sample PDF.pdf";
            title                   = string.Empty;
            author                  = string.Empty;
            fileName                = string.Empty;
            quoteCode               = string.Empty;
            clientCompany           = string.Empty;
            clientFullName          = string.Empty;
            clientPosition          = string.Empty;
            clientPhoneNumber       = string.Empty;
            clientPhoneExtension    = string.Empty;
            clientEmail             = string.Empty;
            quoteNumber             = string.Empty;
            date                    = string.Empty;
            validUntilDate          = string.Empty;
            currency                = string.Empty;
            changeRate              = string.Empty;
            payConditions           = string.Empty;
            deliveryTime            = string.Empty;
            deliveryPlace           = string.Empty;
            totalCurrency           = string.Empty;
            total                   = string.Empty;
            totalWritten            = string.Empty;
            taxNote                 = string.Empty;
            notes                   = string.Empty;
            items = new List<DocumentItem>();
        }

    }
}
