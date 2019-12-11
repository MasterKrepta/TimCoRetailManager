using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManger.Library.Internal.DataAccess;
using TRMDataManger.Library.Models;

namespace TRMDataManger.Library.DataAccess
{
    public class SaleData
    {



        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO make this solid/dry
            List<SaleDetailDbModel> details = new List<SaleDetailDbModel>();
            ProductData products = new ProductData(); //cringe
            var taxRate = ConfigHelper.GetTaxRate()/100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDbModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //Get product info
                var productInfo = products.GetProductById(detail.ProductId);
                
                if (productInfo == null)
                {
                    throw new Exception($"The product id of {detail.ProductId} is not found in the database");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                //Get Txx
                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }

            //create sale model
            SaleDbModel sale = new SaleDbModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            


            using (SqlDataAccess sql = new SqlDataAccess())
            {
                try
                {
                    //Save Sale Record
                    sql.StartTransaction("TRMData");
                    sql.SaveDataInTransaction("dbo.spSale_Insert", sale);

                    //Get id from sale..
                    sale.Id = sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                    //fill in rest
                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                    }

                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }
    }
}
