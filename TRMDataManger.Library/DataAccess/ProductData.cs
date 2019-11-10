﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManger.Library.Internal.DataAccess;
using TRMDataManger.Library.Models;

namespace TRMDataManger.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess(); // CRINGE!!!!!

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { },"TRMData"); // TODO change this

            return output;
        }
    }
}
