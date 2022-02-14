using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinentalExam.Entity.Catalogs;
using ContinentalExam.Entity.Transactions;
using ContinentalExam.Business.Catalogs;
using System.Diagnostics;
using System.Windows;


namespace ContinentalExam.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Input1()
        {
            EntTicket ticketInput1 = new EntTicket();
            string ErrorMsg = string.Empty;
            string Path = @".\TmpTicket.pdf";

            ticketInput1.Sales = new List<EntSale>();
            ticketInput1.Sales.Add(new EntSale(new EntProduct(5, "Book", 1, 12.49, false, new EntProductType(1, "Books", 0)), 1));
            ticketInput1.Sales.Add(new EntSale(new EntProduct(5, "Book", 1, 12.49, false, new EntProductType(1, "Books", 0)), 1));
            ticketInput1.Sales.Add(new EntSale(new EntProduct(6, "Music CD", 2, 14.99, false, new EntProductType(18, "Audio", 10)), 1));
            ticketInput1.Sales.Add(new EntSale(new EntProduct(7, "Chocolate bar", 3, 0.85, false, new EntProductType(2, "Food", 0)), 1));

            if (ticketInput1.MakeTicket(Path, ref ErrorMsg))
            {
                Process.Start(Path);
            }
        }

        [TestMethod]
        public void Input2()
        {
            EntTicket ticketInput2 = new EntTicket();
            string ErrorMsg = string.Empty;
            string Path = @".\TmpTicket.pdf";

            ticketInput2.Sales = new List<EntSale>();
            ticketInput2.Sales.Add(new EntSale(new EntProduct(8, "1 Imported box of chocolates", 4, 10, true, new EntProductType(2, "Food", 0)), 1));
            ticketInput2.Sales.Add(new EntSale(new EntProduct(9, "Imported bottle of perfume Women", 5, 47.5, true, new EntProductType(16, "Women's Fashion", 10)), 1));

            if (ticketInput2.MakeTicket(Path, ref ErrorMsg))
            {
                Process.Start(Path);
            }
        }

        [TestMethod]
        public void Input3()
        {
            EntTicket ticketInput3 = new EntTicket();
            string ErrorMsg = string.Empty;
            string Path = @".\TmpTicket.pdf";

            ticketInput3.Sales = new List<EntSale>();
            ticketInput3.Sales.Add(new EntSale(new EntProduct(10, "Imported bottle of perfume Men", 6, 27.99, true, new EntProductType(17, "Men's Fashion", 10)), 1));
            ticketInput3.Sales.Add(new EntSale(new EntProduct(11, "Bottle of perfume", 7, 18.99, false, new EntProductType(17, "Men's Fashion", 10)), 1));
            ticketInput3.Sales.Add(new EntSale(new EntProduct(9, "Packet of headache pills", 8, 9.75, false, new EntProductType(3, "Medical", 0)), 1));
            ticketInput3.Sales.Add(new EntSale(new EntProduct(12, "Imported box of chocolates M&M", 9, 11.25, true, new EntProductType(2, "Food", 0)), 1));
            ticketInput3.Sales.Add(new EntSale(new EntProduct(12, "Imported box of chocolates M&M", 9, 11.25, true, new EntProductType(2, "Food", 0)), 1));

            if (ticketInput3.MakeTicket(Path, ref ErrorMsg))
            {
                Process.Start(Path);
            }
        }
    }
}
