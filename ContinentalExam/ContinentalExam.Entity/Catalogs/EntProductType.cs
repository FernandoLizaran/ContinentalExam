using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinentalExam.Entity.Catalogs
{
    public class EntProductType
    {
        #region Vars

        private int _ProductTypeId;
        private string _ProductType;
        private int _Tax;

        #endregion

        #region Constructors

        public EntProductType()
        { }

        public EntProductType(int productTypeId, string productType, int tax)
        {
            this.ProductTypeId = productTypeId;
            this.ProductType = productType;
            this.Tax = tax;
        }

        #endregion

        #region Properties

        public int ProductTypeId
        {
            get { return _ProductTypeId; }
            set { _ProductTypeId = value; }
        }

        public string ProductType
        {
            get { return _ProductType; }
            set { _ProductType = value; }
        }

        public int Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }

        #endregion
    }
}
