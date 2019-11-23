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

            //Save Sale Record
            SqlDataAccess sql = new SqlDataAccess(); // CRINGE!!!!!
            sql.SaveData("dbo.spSale_Insert", sale, "TRMData");

            //Get id from sale..
            sale.Id = sql.LoadData<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }, "TRMData").FirstOrDefault();

            //fill in rest
            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                sql.SaveData("dbo.spSaleDetail_Insert", item, "TRMData");
            }

            

        }

        //public List<ProductModel> GetProducts()
        //{
        //    SqlDataAccess sql = new SqlDataAccess(); // CRINGE!!!!!

        //    var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "TRMData"); // TODO change this

        //    return output;
        //}
    }
}
