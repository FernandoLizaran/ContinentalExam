using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinentalExam.Entity.Catalogs
{
    public class EntProduct
    {
        #region Vars

        private int _ProductId;
        private string _Product;
        private int _Code;
        private double _Price;
        private bool _Imported;
        private EntProductType _ProductType;

        #endregion

        #region Constructors

        public EntProduct()
        { }

        public EntProduct(int productId, string product, int code, double price, bool imported, EntProductType productType)
        {
            this.ProductId = productId;
            this.Product = product;
            this.Code = code;
            this.Price = price;
            this.Imported = imported;
            this.ProductType = productType;
        }

        #endregion

        #region Properties

        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }

        public string Product
        {
            get { return _Product; }
            set { _Product = value; }
        }

        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public bool Imported
        {
            get { return _Imported; }
            set { _Imported = value; }
        }

        public EntProductType ProductType
        {
            get { return _ProductType; }
            set { _ProductType = value; }
        }

        #endregion
    }
}
