using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Api.Models.Responses;

public class GetInvoiceResponse
{
    public GetInvoiceResponse(InvoiceDto invoice) 
    {
        InvoiceId = invoice.Id;
        CustomerId = invoice.CustomerRef.Value;
        CustomerName = invoice.CustomerRef.Name;
        Lines = invoice.Line.Select(l => new LineDto
        {
            LineId = l.Id,
            LineNumber = l.LineNum,
            Amount = l.Amount,
            DetailType = l.DetailType,
            ItemName = l.SalesItemLineDetail.ItemRef.Name,
            ItemId = l.SalesItemLineDetail.ItemRef.Value
        });
        ShipFromAddressId = invoice.ShipAddr.Id;
        ShipFromAddressLine1 = invoice.ShipAddr.Line1;
        PrintStatus = invoice.PrintStatus;
        EmailStatus = invoice.EmailStatus;
        BillEmail = invoice.BillEmail.Address;
        Balance = invoice.Balance;
        SyncToken = invoice.SyncToken;
    }

    [Display("Customer ID")]
    public string CustomerId { get; set; }

    [Display("Customer name")]
    public string CustomerName { get; set; }

    public IEnumerable<LineDto> Lines { get; set; }

    [Display("Ship from address ID")]
    public string ShipFromAddressId { get; set; }

    [Display("Ship from address line 1")]
    public string ShipFromAddressLine1 { get; set; }

    [Display("Print status")]
    public string PrintStatus { get; set; }

    [Display("Email status")]
    public string EmailStatus { get; set; }
    
    [Display("Bill email address")]
    public string BillEmail { get; set; }
    
    public double Balance { get; set; }

    [Display("Sync token")]
    public string SyncToken { get; set; }

    [Display("Invoice ID")]
    public string InvoiceId { get; set; }
}


public class LineDto
{
    [Display("Line ID")]
    public string LineId { get; set; }

    [Display("Line number")]
    public int LineNumber { get; set; }

    public double Amount { get; set; }

    [Display("Detail type")]
    public string DetailType { get; set; }

    [Display("Item name")]
    public string ItemName { get; set; }

    [Display("Item ID")]
    public string ItemId { get; set; }
}