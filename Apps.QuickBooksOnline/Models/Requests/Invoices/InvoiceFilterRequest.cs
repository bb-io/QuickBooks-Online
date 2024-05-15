namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class InvoiceFilterRequest
{
    public bool? Paid { get; set; }
    
    public bool? Overdue { get; set; }
}