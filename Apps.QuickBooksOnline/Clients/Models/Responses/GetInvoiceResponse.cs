using System.Text.Json.Serialization;
using Apps.QuickBooksOnline.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Clients.Models.Responses
{
    public class GetInvoiceResponse
    {
        public GetInvoiceResponse(InvoiceDto invoice) {

            CustomerId = invoice.Customer.Id;
            CustomerName = invoice.Customer.Name;
            Lines = invoice.Line.Select(x => new LineDto()
            {
                LineId = x.Id,
                LineNumber = x.LineNumber,
                Amount = x.Amount,
                DetailType = x.DetailType,
                ItemName = x.SalesItemLineDetail?.Item?.Name,
                ItemId = x.SalesItemLineDetail?.Item?.Id
            });
            ShipFromAddressId = invoice.ShipFromAddress?.Id;
            ShipFromAddressLine1 = invoice.ShipFromAddress?.Line1;
            ShipFromAddressLine2 = invoice.ShipFromAddress?.Line2;
            PrintStatus = invoice.PrintStatus;
            EmailStatus = invoice.EmailStatus;
            Balance = invoice.Balance;
            SyncToken = invoice.SyncToken;
            InvoiceId = invoice.InvoiceId;
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

        [Display("Ship from address line 2")]
        public string ShipFromAddressLine2 { get; set; }

        [Display("Print status")]
        public string PrintStatus { get; set; }

        [Display("Email status")]
        public string EmailStatus { get; set; }

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
}
