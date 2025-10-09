using Apps.QuickBooksOnline.Actions;
using Apps.QuickBooksOnline.Api.Models.Requests;
using Apps.QuickBooksOnline.Models.Requests.Vendors;
using Tests.QuickBooksOnline.Base;

namespace Tests.QuickBooksOnline;

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

    [TestMethod]
    public async Task FindVendor_IsSuccess()
    {
        var action = new VendorActions(InvocationContext);

        var input = new GetVendorFilterRequest
        {
            DisplayName = "Test VendorAA",
            //CompanyName = "Test VendorAA"
        };

        var result = await action.FindVendor(input);

        Assert.IsNotNull(result, "Vendor should be found.");
        Assert.IsNotNull(result.Vendor, "Vendor response should contain a vendor.");
        Assert.AreEqual("Test VendorAA", result.Vendor.DisplayName, "Vendor DisplayName should match the filter.");

        Console.WriteLine($"Vendor ID: {result.Vendor.Id}");
        Console.WriteLine($"Company Name: {result.Vendor.CompanyName}");
    }



}

