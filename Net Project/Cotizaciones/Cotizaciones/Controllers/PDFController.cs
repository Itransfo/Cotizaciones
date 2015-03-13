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
    }
}