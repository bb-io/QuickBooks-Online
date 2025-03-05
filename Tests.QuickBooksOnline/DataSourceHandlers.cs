using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.QuickBooksOnline.Base;

namespace Tests.QuickBooksOnline
{
    [TestClass]
    public class DataSourceHandlers : TestBase
    {
        [TestMethod]
        public async Task ItemDataHandler_ShouldNotThrowError()
        {
            var handler = new ItemDataSourceHandler(InvocationContext);
            await TestDataHandler(handler);
        }

        [TestMethod]
        public async Task TermDataHandler_ShouldNotThrowError()
        {
            var handler = new TermDataSource(InvocationContext);
            await TestDataHandler(handler);
        }


        private async Task TestDataHandler(IAsyncDataSourceItemHandler dataSourceItemHandler)
        {
            var result = await dataSourceItemHandler.GetDataAsync(new(), default)
                         ?? throw new Exception("Data handler should not return null");

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Value}: {item.DisplayName}");
            }
        }

        [TestMethod]
        public async Task VendorDataHandler_ShouldNotThrowError()
        {
            var handler = new VendorDataSource(InvocationContext);
            await TestDataHandler(handler);
        }

        [TestMethod]
        public async Task PaymentDataHandler_ShouldNotThrowError()
        {
            var handler = new PaymentDataSourceHandler(InvocationContext);
            await TestDataHandler(handler);
        }
    }
}