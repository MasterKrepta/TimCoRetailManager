using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManger.Library
{
    public class ConfigHelper
    {
        public static decimal GetTaxRate()
        {
            //TODO put this into the API
            string rateText = ConfigurationManager.AppSettings["taxRate"];

            bool isValidTaxRate = decimal.TryParse(rateText, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The Tax Rate is not setup properly");
            }

            return output;
        }
    }
}
