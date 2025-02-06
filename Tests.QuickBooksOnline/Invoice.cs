using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Requests.Invoices;
using Tests.QuickBooksOnline.Base;

namespace Tests.QuickBooksOnline
{
    [TestClass]
    public class Invoice : TestBase
    {
        [TestMethod]
        public async Task CreateInvoice_IsSuccess()
        {
         
            var action = new InvoiceActions(InvocationContext,FileManager);

            var input = new CreateInvoiceRequest {CustomerId= "67", LineAmounts = [12,33], ItemIds = ["1","2"], 
                UnitPrices = ["2","3"], DocNumber="CASE",
                SalesTerms = "1",
                Quantities = [6,11],
                Descriptions = ["CASE","CASE"]
            };

            var result = await action.CreateInvoice(input);

            Assert.IsNotNull(result);
        }

    }
}
