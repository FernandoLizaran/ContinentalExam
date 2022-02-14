using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using ContinentalExam.DA;
using ContinentalExam.Entity.Catalogs;

namespace ContinentalExam.Business.Catalogs
{
    public class BLProducts
    {
        private CBase AccesoDatos = new CBase();

        public List<EntProduct> GetProducts()
        {
            try
            {
                DataTable myTable = AccesoDatos.consultarDT("SP_Products", null);

                if (myTable != null && myTable.Rows.Count > 0)
                {
                    List<EntProduct> tmpBancos = (from item in myTable.AsEnumerable()
                                                  select new EntProduct
                                                      (Convert.ToInt32(item["ProductId"])
                                                        , item["Product"].ToString()
                                                        , Convert.ToInt32(item["Code"])
                                                        , Convert.ToDouble(item["Price"])
                                                        , Convert.ToBoolean(item["Imported"])
                                                        , new EntProductType
                                                            (Convert.ToInt32(item["ProductTypeId"])
                                                                , item["ProductType"].ToString()
                                                                , Convert.ToInt32(item["Tax"])
                                                            )
                                                      )).ToList<EntProduct>();

                    return tmpBancos;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public bool AddProduct(EntProduct newProduct)
        {
            try
            {
                OleDbParameter[] myParams = new OleDbParameter[5];
                myParams[0] = new OleDbParameter("@Product", newProduct.Product);
                myParams[1] = new OleDbParameter("@Code", newProduct.Code);
                myParams[2] = new OleDbParameter("@Price", newProduct.Price);
                myParams[3] = new OleDbParameter("@ProductTypeId", newProduct.ProductType.ProductTypeId);
                myParams[4] = new OleDbParameter("@Imported", newProduct.Imported);

                return AccesoDatos.consultarIUD("Sp_AddProduct", myParams);

            }
            catch (Exception ex)
            { }
            return false;
        }

        public bool UpdateProduct(EntProduct udpProduct)
        {
            try
            {
                OleDbParameter[] myParams = new OleDbParameter[6];
                myParams[0] = new OleDbParameter("@Product", udpProduct.Product);
                myParams[1] = new OleDbParameter("@Code", udpProduct.Code);
                myParams[2] = new OleDbParameter("@Price", udpProduct.Price);
                myParams[3] = new OleDbParameter("@ProductTypeId", udpProduct.ProductType.ProductTypeId);
                myParams[4] = new OleDbParameter("@ProductId", udpProduct.ProductId);
                myParams[5] = new OleDbParameter("@Imported", udpProduct.Imported);

                return AccesoDatos.consultarIUD("Sp_UpdateProduct", myParams);

            }
            catch (Exception ex)
            { }
            return false;
        }

        public List<EntProductType> GetProductTypes()
        {
            try
            {
                DataTable myTable = AccesoDatos.consultarDT("Sp_ProductTypes", null);

                if (myTable != null && myTable.Rows.Count > 0)
                {
                    List<EntProductType> tmpBancos = (from item in myTable.AsEnumerable()
                                                      select new EntProductType
                                                      (Convert.ToInt32(item["ProductTypeId"])
                                                            , item["ProductType"].ToString()
                                                            , Convert.ToInt32(item["Tax"])
                                                      )).ToList<EntProductType>();

                    return tmpBancos;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }
    }
}
