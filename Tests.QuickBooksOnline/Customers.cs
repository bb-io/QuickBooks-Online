using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.DataSourceHandlers;
using Apps.QuickBooksOnline.Models.Requests.Customers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.QuickBooksOnline.Base;

namespace Tests.QuickBooksOnline
{
    [TestClass]
    public class Customers : TestBase
    {
        [TestMethod]
        public async Task CanCreateCustomer()
        {
            var actions = new CustomerActions(InvocationContext);
            var result = await actions.CreateCustomer(new CreateCustomerRequest
            {
                City = "Mt Pleasant",
                Term = "3",
                Line1 = "72 E Blue Grass Road",
                Line2 = "Suite D",
                Country = "Canada [CA]",
                Currency = "CAD",
                PostalCode = "a1b 2c3",
                CompanyName = "Test company 4",
                DisplayName = "Test company 4",
                CountrySubDivisionCode = "MI"
            });

            Assert.IsTrue(result.Active);
        }
    }
}
