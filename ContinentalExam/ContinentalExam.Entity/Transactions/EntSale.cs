using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContinentalExam.Entity.Catalogs;

namespace ContinentalExam.Entity.Transactions
{
    public class EntSale
    {
        #region Vars

        private EntProduct _Product;
        private int _Quantity;

        #endregion

        #region Constructors

        public EntSale()
        { }

        public EntSale(EntProduct product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        #endregion

        #region Properties

        public EntProduct Product
        {
            get { return _Product; }
            set { _Product = value; }
        }

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public double Total
        {
            get
            {
                return this.Product.Price * this.Quantity;
            }
        }

        public double Tax
        {
            get
            {
                return Math.Round(this.Total * (this.Product.ProductType.Tax / 100.0), 2);
            }
        }

        public double ImportedTax
        {
            get
            {
                return this.Product.Imported ? Math.Round(this.Total * 0.05, 2) : 0.00;
            }
        }

        public double FinalTax
        {
            get
            {
                return Round();
            }
        }

        public double TotalWTax
        {
            get
            {
                return Math.Round(Total + FinalTax, 2);
            }
        }

        #endregion

        #region methods

        private double Round()
        {
            double fTax = Math.Round(Tax + ImportedTax, 2);
            if (fTax > 0.0)
            {
                string fTaxString = Math.Round(fTax, 2).ToString("N");
                string LastDigit = fTaxString.Substring(fTaxString.Length - 1, 1);
                double RountAumont = 0.0;

                switch (LastDigit)
                {
                    case "0":
                        RountAumont = .00;
                        break;
                    case "1":
                        RountAumont = .04;
                        break;
                    case "2":
                        RountAumont = .03;
                        break;
                    case "3":
                        RountAumont = .02;
                        break;
                    case "4":
                        RountAumont = .01;
                        break;
                    case "5":
                        RountAumont = .00;
                        break;
                    case "6":
                        RountAumont = .04;
                        break;
                    case "7":
                        RountAumont = .03;
                        break;
                    case "8":
                        RountAumont = .02;
                        break;
                    case "9":
                        RountAumont = .01;
                        break;
                    default:
                        RountAumont = .00;
                        break;
                }

                return fTax + RountAumont;

            }
            else
                return .00;

        }

        #endregion
    }
}
