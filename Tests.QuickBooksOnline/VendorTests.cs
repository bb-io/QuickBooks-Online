using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Models.Requests.Vendors;
using Tests.QuickBooksOnline.Base;

namespace Tests.QuickBooksOnline
{ 
    [TestClass]
    public class VendorTests :TestBase
    {
        [TestMethod]
        public async Task CreateVendor_IsSuccess()
        {
            var action = new VendorActions(InvocationContext);
            var input = new CreateVendorRequest
            {
                DisplayName = "Test VendorAA",
                //CurrencyCode = "EUR"
            };

            var result = await action.CreateVendor(input);

            Console.WriteLine(result.Currency);
            Console.WriteLine(result.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateVendor_IsSuccess()
        {
            var action = new VendorActions(InvocationContext);
            var input = new UpdateVendorRequest
            {
                VendorId = "69",
                DisplayName = "Test VendorA",
            };

            var result = await action.UpdateVendor(input);

            Console.WriteLine(result.Currency);
            Assert.IsNotNull(result);
        }
    }
}

