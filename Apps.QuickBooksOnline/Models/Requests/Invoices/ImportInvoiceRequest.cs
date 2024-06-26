﻿using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.QuickBooksOnline.Models.Requests.Invoices;

public class ImportInvoiceRequest
{
    [Display("File", Description = "The file to import. Must be in JSON format.")]
    public FileReference File { get; set; }

    [Display("Customer ID", Description = "Required for invoice creation."), DataSource(typeof(CustomerDataSource))]
    public string? CustomerId { get; set; }

    [Display("Invoice ID", Description = "The ID of the invoice to reimport."), DataSource(typeof(InvoiceDataHandler))]
    public string? InvoiceId { get; set; }

    [Display("Class ID", Description = "The ID of the class to assign to the invoice."), DataSource(typeof(ClassDataHandler))]
    public string? ClassId { get; set; }
}