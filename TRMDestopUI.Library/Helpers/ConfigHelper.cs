using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDestopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {

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
