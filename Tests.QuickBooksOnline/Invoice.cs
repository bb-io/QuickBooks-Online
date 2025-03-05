using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api.Models.Requests;
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

            var input = new CreateInvoiceRequest {CustomerId= "67", LineAmounts = [12,33], ItemIds = ["",""], 
                UnitPrices = ["2","3"], DocNumber="CASE",
                SalesTerms = "1",
                Quantities = [6,11],
                Descriptions = ["CASE","CASE"]
            };

            var result = await action.CreateInvoice(input);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateBill_IsSuccess()
        {

            var action = new InvoiceActions(InvocationContext, FileManager);

            var input = new CreateBillRequest
            {
                VendorId = "56",
                LineAmounts = new[] { 400.0 },
                Descriptions = new[] { "Test line" },
                BillDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                DocNumber = "TestBill001",
                AccountIds = new[] { "1" }
            };

            var result = await action.CreateBill(input);

            Assert.IsNotNull(result);
        }

    }
}
