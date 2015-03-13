using Cotizaciones.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cotizaciones.Controllers
{
    //Reference: http://www.pdfsharp.net/wiki/Invoice-sample.ashx
    public class PDFUtil
    {
        DocumentContent documentContent;
        Document document;
        Section section;
        Table itemTable;
        Table headerTable;

        public PDFUtil()
        {
            documentContent = new DocumentContent();
            Initiate();
        }

        public PDFUtil(DocumentContent documentContent)
        {
            this.documentContent = documentContent;
            Initiate();
        }

        private void Initiate()
        {
            document = CreateDocument();
            document.DefaultPageSetup.PageWidth = "8.50in";
            document.DefaultPageSetup.PageHeight = "11.0in";
            DefineStyles();
            CreatePage();
            CreateHeaderTable();
            FillHeaderTable();
            CreateItemTable();
            FillItemTable();
            AddNotes();
        }

        private void AddNotes()
        {
            //Written total
            TextFrame textFrame = section.AddTextFrame();
            textFrame.Width = "17cm";
            textFrame.Left = ShapePosition.Left;
            textFrame.WrapFormat.Style = WrapStyle.None;
            textFrame.AddParagraph();
            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = "10pt";
            paragraph.Format.Font.Name = "Arial";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText(documentContent.totalWritten);
            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = "10pt";
            paragraph.Format.Font.Name = "Arial";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText(documentContent.taxNote);
            paragraph = section.AddParagraph();
            paragraph.Format.Font.Size = "10pt";
            paragraph.Format.Font.Name = "Arial";
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText(documentContent.notes);
        }

        private void CreateItemTable()
        {
            //Create table
            itemTable = section.AddTable();
            itemTable.Style = "Table";
            itemTable.Borders.Color = Color.Parse("black");
            itemTable.Borders.Width = 1;
            itemTable.Rows.LeftIndent = 0;
            //Column definition
            Column column = itemTable.AddColumn("1.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = itemTable.AddColumn("8.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = itemTable.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = itemTable.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = itemTable.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            // Table starts here
            Row row = itemTable.AddRow();
            //row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.VerticalAlignment = VerticalAlignment.Top;
            row.Format.Font.Size = 10;
            row.Format.Font.Bold = true;
            row.Format.Font.Italic = true;
            row.Shading.Color = Color.Parse("black");
            row.Format.Font.Color = Color.Parse("white");
            row.Cells[0].AddParagraph("ITEM");
            row.Cells[1].AddParagraph("Descripción");
            row.Cells[2].AddParagraph("Cantidad y Unidad");
            row.Cells[3].AddParagraph("Precio x equipo");
            row.Cells[4].AddParagraph("Importe");
            //table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        public void FillItemTable()
        {
            foreach(DocumentItem i in documentContent.items)
            {
                Row row = itemTable.AddRow();
                row.Borders.Bottom.Visible = false;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.VerticalAlignment = VerticalAlignment.Top;
                row.Format.Font.Size = 9;
                row.Cells[0].AddParagraph(i.item);
                string[] lines = i.description.Split('\n');
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].AddParagraph(lines[0]);
                row.Cells[2].AddParagraph(i.quantity);
                row.Cells[3].AddParagraph(i.unitPrice);
                row.Cells[4].AddParagraph(i.price);
                for(int j = 1; j < lines.Count(); j++)
                {
                    row = itemTable.AddRow();
                    row.Borders.Top.Visible = false;
                    row.Format.Alignment = ParagraphAlignment.Left;
                    row.VerticalAlignment = VerticalAlignment.Top;
                    row.Format.Font.Size = 9;
                    if(j != lines.Count()-1)
                        row.Borders.Bottom.Visible = false;
                    row.Cells[1].AddParagraph(lines[j]);
                }
            }
            Row lastRow = itemTable.AddRow();
            lastRow.Format.Alignment = ParagraphAlignment.Left;
            lastRow.VerticalAlignment = VerticalAlignment.Top;
            lastRow.Format.Font.Size = 9;
            lastRow.Format.Font.Bold = true;
            lastRow.Cells[3].AddParagraph(documentContent.totalCurrency);
            lastRow.Cells[4].AddParagraph(documentContent.total);
        }
        
        private void CreateHeaderTable()
        {
            //Create table
            headerTable = section.AddTable();
            headerTable.Style = "Table";
            headerTable.Borders.Color = Color.Parse("black");
            headerTable.Borders.Top.Width = 8;
            headerTable.Borders.Left.Width = 0.5;
            headerTable.Borders.Right.Width = 0.5;
            headerTable.Rows.LeftIndent = 0;
            //Column definition
            Column column = headerTable.AddColumn("7cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = headerTable.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = headerTable.AddColumn("6cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            // Table starts here
            Row row = headerTable.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Left;
            row.VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].Borders.Right.Visible = false;
            row.Cells[1].Borders.Left.Visible = false;
            row.Cells[1].Borders.Right.Visible = false;
            row.Cells[2].Borders.Left.Visible = false;
        }
        private void FillHeaderTable()
        {
            headerTable.Rows[0].Format.Font.Size = 9;
            
            headerTable.Rows[0].Cells[0].Format.Font.Bold = true;
            headerTable.Rows[0].Cells[0].Format.Font.Italic = true;
            headerTable.Rows[0].Cells[0].AddParagraph(documentContent.clientCompany);
            headerTable.Rows[0].Cells[0].AddParagraph();
            headerTable.Rows[0].Cells[0].AddParagraph(documentContent.clientFullName);
            headerTable.Rows[0].Cells[0].AddParagraph(documentContent.clientPosition);
            headerTable.Rows[0].Cells[0].AddParagraph();
            headerTable.Rows[0].Cells[0].AddParagraph(string.Format("Tel: {0} ext. {1}", documentContent.clientPhoneNumber, documentContent.clientPhoneExtension));
            headerTable.Rows[0].Cells[0].AddParagraph();
            headerTable.Rows[0].Cells[0].AddParagraph(string.Format("Email: {0}", documentContent.clientEmail));

            headerTable.Rows[0].Cells[1].AddParagraph().AddFormattedText("Número de cotización:", TextFormat.Bold);
            headerTable.Rows[0].Cells[1].AddParagraph();
            headerTable.Rows[0].Cells[1].AddParagraph("Fecha:");
            headerTable.Rows[0].Cells[1].AddParagraph("Vigente hasta:");
            headerTable.Rows[0].Cells[1].AddParagraph("Moneda:");
            headerTable.Rows[0].Cells[1].AddParagraph("Tipo de Cambio:");
            headerTable.Rows[0].Cells[1].AddParagraph("Condiciones de Pago:");
            headerTable.Rows[0].Cells[1].AddParagraph().AddFormattedText("Plazo de engrega:", TextFormat.Bold);
            headerTable.Rows[0].Cells[1].AddParagraph("Lugar de entrega:");

            headerTable.Rows[0].Cells[2].AddParagraph().AddFormattedText(documentContent.quoteNumber, TextFormat.Bold);
            headerTable.Rows[0].Cells[2].AddParagraph();
            headerTable.Rows[0].Cells[2].AddParagraph(documentContent.date);
            headerTable.Rows[0].Cells[2].AddParagraph(documentContent.validUntilDate);
            headerTable.Rows[0].Cells[2].AddParagraph(documentContent.currency);
            headerTable.Rows[0].Cells[2].AddParagraph(documentContent.changeRate);
            headerTable.Rows[0].Cells[2].AddParagraph(documentContent.payConditions);
            headerTable.Rows[0].Cells[2].AddParagraph().AddFormattedText(documentContent.deliveryTime, TextFormat.Bold);
            headerTable.Rows[0].Cells[2].AddParagraph(documentContent.deliveryPlace);
        }

        public bool saveDocument()
        {
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            try
            {
                pdfRenderer.PdfDocument.Save(documentContent.fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public PdfDocument getPDFDocument()
        {
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            try
            {
                return pdfRenderer.PdfDocument;
            }
            catch
            {
                return null;
            }
        }

        private Document CreateDocument()
        {
            this.document = new Document();
            this.document.Info.Title = documentContent.title;
            this.document.Info.Author = documentContent.author;
            return this.document;
        }

        private void DefineStyles()
        {
            Style style = this.document.Styles["Normal"];
            style.Font.Name = "Tahoma";
        }
        
        Section CreatePage()
        {
            section = this.document.AddSection();
            section.PageSetup.TopMargin = "4.8cm";
            section.PageSetup.BottomMargin = "4cm";
            //Header
            //Image
            Image image = section.Headers.Primary.AddImage(HttpContext.Current.Server.MapPath("~/Content/images/headerPicture.png"));
            image.Height = "3.5cm";
            image.LockAspectRatio = true;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;
            image.RelativeVertical = RelativeVertical.Page;
            image.RelativeHorizontal = RelativeHorizontal.Page;
            image.WrapFormat.Style = WrapStyle.None;
            image.WrapFormat.DistanceTop = "0.5cm";
            
            //Code
            TextFrame textFrame = section.Headers.Primary.AddTextFrame();
            textFrame.Width = document.DefaultPageSetup.PageWidth;
            textFrame.Left = ShapePosition.Right;
            textFrame.WrapFormat.Style = WrapStyle.None;
            textFrame.RelativeVertical = RelativeVertical.Page;
            textFrame.RelativeHorizontal = RelativeHorizontal.Page;
            textFrame.WrapFormat.DistanceTop = "3cm";
            textFrame.WrapFormat.DistanceRight = "3cm";

            Paragraph paragraph = textFrame.AddParagraph();
            paragraph.Format.Font.Size = "8pt";
            paragraph.Format.Font.Name = "Arial";
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(documentContent.quoteCode);

            //Title
            textFrame = section.Headers.Primary.AddTextFrame();
            textFrame.Width = document.DefaultPageSetup.PageWidth;
            textFrame.Left = ShapePosition.Center;
            textFrame.WrapFormat.Style = WrapStyle.None;
            textFrame.RelativeVertical = RelativeVertical.Page;
            textFrame.RelativeHorizontal = RelativeHorizontal.Page;
            textFrame.WrapFormat.DistanceTop = "4cm";

            paragraph = textFrame.AddParagraph();
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = "10pt";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(string.Format("COTIZACIÓN {0}",documentContent.title));

            //Footer
            //Break
            textFrame = section.Footers.Primary.AddTextFrame();
            textFrame.WrapFormat.Style = WrapStyle.None;
            textFrame.RelativeVertical = RelativeVertical.Page;
            textFrame.RelativeHorizontal = RelativeHorizontal.Page;
            textFrame.WrapFormat.DistanceBottom = "-1cm";
            textFrame.WrapFormat.DistanceLeft = "2cm";
            textFrame.Width = document.DefaultPageSetup.PageWidth;
                        
            paragraph = textFrame.AddParagraph();
            image = paragraph.AddImage(HttpContext.Current.Server.MapPath("~/Content/images/footerPicture.png"));
            image.Width = "18cm";
            //Company
            paragraph = textFrame.AddParagraph();
            paragraph.Format.Font.Size = "8pt";
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddFormattedText("CONFIABILIDAD EN EXACTITUD Y PRECISIÓN S.A. DE C.V.",TextFormat.Bold);
            paragraph.AddLineBreak();
            //Address
            paragraph = textFrame.AddParagraph();
            paragraph.AddText("Av. Parque América #116 Col. Vista Hermosa, C.P. 62290 Cuernavaca, Morelos, RFC: CEP 1102219EW4");
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            //Pages
            paragraph.AddText("Página ");
            paragraph.AddPageField();
            paragraph.AddText(" de ");
            paragraph.AddNumPagesField();
            return section;
        }
    }
}
