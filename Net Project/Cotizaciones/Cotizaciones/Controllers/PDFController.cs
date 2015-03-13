using Cotizaciones.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizaciones.Controllers
{
    public class PDFController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: PDF
        public ActionResult Index()
        {
            ViewBag.Clients = db.Clients.ToList();
            return View();
        }

        public ActionResult CreatePDF(int clientId, string title)
        {
            DocumentContent documentContent = new DocumentContent();
            documentContent.title = title;
            documentContent.fileName = title+".pdf";
            documentContent.author = "Itransfo";
            Client client = db.Clients.Find(clientId);
            documentContent.clientFullName = client.getFullName();
            documentContent.clientCompany = client.Company;
            documentContent.clientPhoneNumber = client.Phone;
            documentContent.clientPhoneExtension = client.Extension.ToString();
            documentContent.clientEmail = client.Email;
            documentContent.date = DateTime.Now.ToString("dd/MM/yyyy");
            documentContent.validUntilDate = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");
            documentContent.currency = "Dólares Estadounidenses";
            documentContent.changeRate = "15.2000";
            documentContent.payConditions = "Contado con la O.C.";
            documentContent.deliveryTime = "90 días naturales";
            documentContent.deliveryPlace = client.Address != null ? client.Address : string.Empty;
            
            documentContent.totalCurrency = "TOTAL USD.";
            documentContent.totalWritten = "IMPORTE CON LETRA:";
            documentContent.taxNote = "I.V.A. NO INCLUIDO";

            PDFUtil pdfUtil = new PDFUtil(documentContent);
            using (MemoryStream stream = new MemoryStream())
            {
                var document = pdfUtil.getPDFDocument();
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf");
            }

        }

        public ActionResult fromOrder(int orderId)
        {
            Order order = db.Orders.Find(orderId);
            DocumentContent documentContent = new DocumentContent();
            documentContent.title = order.Identifier;
            documentContent.fileName = order.Identifier + ".pdf";
            documentContent.author = User.Identity.Name;
            Client client = order.Client;
            documentContent.clientFullName = client.getFullName();
            documentContent.clientCompany = client.Company;
            documentContent.clientPhoneNumber = client.Phone;
            documentContent.clientPhoneExtension = client.Extension.ToString();
            documentContent.clientEmail = client.Email;
            documentContent.date = DateTime.Now.ToString("dd/MM/yyyy");
            documentContent.validUntilDate = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy");

            int itemCount = 1;
            foreach (OrderProduct orderProduct in order.OrderProducts)
            {
                DocumentItem documentItem = new DocumentItem();
                documentItem.item = itemCount.ToString("D3");
                documentItem.description = orderProduct.Product.Name;
                documentItem.description += orderProduct.Product.AdditionalData != null ? orderProduct.Product.AdditionalData : string.Empty;
                documentItem.quantity = ((int)orderProduct.Quantity).ToString("D2");
                documentItem.unitPrice = "$"+orderProduct.Product.ProviderPrice.ToString();
                documentItem.price = "$" + (orderProduct.Product.ProviderPrice * orderProduct.Quantity).ToString();
                documentContent.items.Add(documentItem);
                itemCount++;
            }

            //Not yet implemented
            documentContent.currency = "Dólares Estadounidenses";
            //Not yet implemented - Get dollar rate
            documentContent.changeRate = "15.2000";
            documentContent.payConditions = "Contado con la O.C.";
            //Not yet implemented
            documentContent.deliveryTime = "90 días naturales";
            documentContent.deliveryPlace = client.Address != null ? client.Address : string.Empty;
            //Not yet implemented
            documentContent.totalCurrency = "TOTAL USD.";
            documentContent.total = order.getTotal().ToString();
            //Not yet implemented
            documentContent.totalWritten = "IMPORTE CON LETRA:";
            //Not yet implemented
            documentContent.taxNote = "I.V.A. NO INCLUIDO";

            PDFUtil pdfUtil = new PDFUtil(documentContent);
            using (MemoryStream stream = new MemoryStream())
            {
                var document = pdfUtil.getPDFDocument();
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf");
            }
        }
    }
}