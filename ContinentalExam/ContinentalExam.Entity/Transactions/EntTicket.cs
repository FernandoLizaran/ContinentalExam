using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ContinentalExam.Entity.Transactions
{
    public class EntTicket
    {
        #region Vars

        private List<EntSale> _Sales;

        #endregion

        #region Constructors

        public EntTicket()
        { }

        public EntTicket(List<EntSale> sales)
        {
            this.Sales = sales;
        }

        #endregion

        #region Properties

        public List<EntSale> Sales
        {
            get { return _Sales; }
            set { _Sales = value; }
        }

        public double Total
        {
            get
            {
                return (from item in this.Sales
                        select item.Total + item.FinalTax).Sum();
            }
        }

        public double TotalTax
        {
            get
            {
                return Math.Round((from item in this.Sales
                                   select item.FinalTax).Sum(), 2);
            }
        }

        #endregion

        #region methods

        public bool MakeTicket(string FilePath, ref string ErrorMsg)
        {
            try
            {

                Document doc = new Document();
                doc.SetPageSize(new iTextSharp.text.Rectangle(210, 500));

                PdfWriter writer = PdfWriter.GetInstance(doc,
                                            new FileStream(FilePath, FileMode.Create));

                doc.AddTitle("Ticket");
                doc.AddCreator("System");
                doc.Open();

                doc.SetMargins(0, 0, 0, 0);
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("../../Resources/Images/Logo.png");
                jpg.ScaleToFit(160f, 70f);
                jpg.SpacingBefore = 0f;
                jpg.SpacingAfter = 0f;
                jpg.Alignment = Element.ALIGN_CENTER;

                doc.Add(jpg);
                doc.Add(Chunk.NEWLINE);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("Ticket Of Sale");
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                paragraph = new iTextSharp.text.Paragraph(string.Format("Ddate {0}", DateTime.Now));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                paragraph = new iTextSharp.text.Paragraph("********************************************");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);


                var GroupingSales = Sales
                                        .GroupBy(person => person.Product.Product)
                                        .Select(grouping => new { Product = grouping.Key, Count = grouping.Count() })
                                        .ToList();

                foreach (var group in GroupingSales)
                {
                    string Product = group.Product;
                    double Taxes = Sales.Find(c => c.Product.Product == Product).TotalWTax;
                    int Count = group.Count;

                    if (Count > 1)
                    {
                        paragraph = new iTextSharp.text.Paragraph(string.Format("{0}: ${1} ({2} @ {3})", Product, Math.Round(Taxes * Count, 2), Count, Taxes));
                    }
                    else
                    {
                        EntSale tSale = Sales.Find(c => c.Product.Product == Product);
                        if (tSale.Quantity > 1)
                        {
                            paragraph = new iTextSharp.text.Paragraph(string.Format("{0}: ${1} ({2} @ {3})", tSale.Product.Product, tSale.TotalWTax
                                , tSale.Quantity, Math.Round((tSale.TotalWTax / tSale.Quantity), 2)));
                        }
                        else
                        {
                            paragraph = new iTextSharp.text.Paragraph(string.Format("{0}: ${1}", Product, Taxes));
                        }
                    }

                    paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                    paragraph.Font.Size = 6;
                    doc.Add(paragraph);
                    doc.Add(Chunk.NEWLINE);

                }

                paragraph = new iTextSharp.text.Paragraph("********************************************");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                paragraph = new iTextSharp.text.Paragraph(string.Format("Sales Taxes: ${0}", this.TotalTax));
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                paragraph = new iTextSharp.text.Paragraph(string.Format("Total: ${0}", this.Total));
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                paragraph = new iTextSharp.text.Paragraph("********************************************");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                paragraph = new iTextSharp.text.Paragraph("This receipt is a summary of purchase. for alcaraciones request your e-voucher");
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font.Size = 8;
                doc.Add(paragraph);
                doc.Add(Chunk.NEWLINE);

                doc.Close();
                writer.Close();

                return true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
        }

        #endregion

    }
}
