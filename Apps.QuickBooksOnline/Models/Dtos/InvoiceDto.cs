using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos;

public class InvoiceWrapper
{
    [JsonProperty("Invoice")]
    public InvoiceDto Invoice { get; set; }
}
    
public class InvoiceDto
{
    [JsonProperty("TxnDate")]
    public string TxnDate { get; set; }
        
    [JsonProperty("domain")]
    public string Domain { get; set; }
        
    [JsonProperty("PrintStatus")]
    public string PrintStatus { get; set; }
        
    [JsonProperty("TotalAmt")]
    public double TotalAmt { get; set; }
        
    [JsonProperty("Line")]
    public Line[] Line { get; set; }
        
    [JsonProperty("DueDate")]
    public string DueDate { get; set; }
        
    [JsonProperty("ApplyTaxAfterDiscount")]
    public bool ApplyTaxAfterDiscount { get; set; }
        
    [JsonProperty("DocNumber")]
    public string DocNumber { get; set; }
        
    [JsonProperty("sparse")]
    public bool Sparse { get; set; }
        
    [JsonProperty("ProjectRef")]
    public ProjectRef ProjectRef { get; set; }
        
    [JsonProperty("Deposit")]
    public double Deposit { get; set; }
        
    [JsonProperty("Balance")]
    public double Balance { get; set; }
        
    [JsonProperty("CustomerRef")]
    public CustomerRef CustomerRef { get; set; }
        
    [JsonProperty("TxnTaxDetail")]
    public TxnTaxDetail TxnTaxDetail { get; set; }
        
    [JsonProperty("SyncToken")]
    public string SyncToken { get; set; }
        
    [JsonProperty("LinkedTxn")]
    public object[] LinkedTxn { get; set; }
        
    [JsonProperty("ShipAddr")]
    public Address ShipAddr { get; set; }
        
    [JsonProperty("EmailStatus")]
    public string EmailStatus { get; set; }
    
    [JsonProperty("BillEmail")]
    public BillEmail BillEmail { get; set; }
        
    [JsonProperty("BillAddr")]
    public Address BillAddr { get; set; }
        
    [JsonProperty("MetaData")]
    public MetaData MetaData { get; set; }
        
    [JsonProperty("CustomField")]
    public CustomField[] CustomField { get; set; }

    [JsonProperty("ClassRef")]
    public ClassRef ClassRef { get; set; }
        
    [JsonProperty("Id")]
    public string Id { get; set; }
        
    [JsonProperty("time")]
    public string Time { get; set; }
}

public class ClassRef
{
    [JsonProperty("value")] 
    public string? Value { get; set; }

    [JsonProperty("name")] 
    public string? Name { get; set; }
}

public class ProjectRef
{
    [JsonProperty("value")]
    public string Value { get; set; }
}
    
public class TxnTaxDetail
{
    [JsonProperty("TotalTax")]
    public double TotalTax { get; set; }
}
    
public class Address
{
    [JsonProperty("City")]
    public string City { get; set; }
        
    [JsonProperty("Line1")]
    public string Line1 { get; set; }
        
    [JsonProperty("PostalCode")]
    public string PostalCode { get; set; }
        
    [JsonProperty("Lat")]
    public string Lat { get; set; }
        
    [JsonProperty("Long")]
    public string Long { get; set; }
        
    [JsonProperty("CountrySubDivisionCode")]
    public string CountrySubDivisionCode { get; set; }
        
    [JsonProperty("Id")]
    public string Id { get; set; }
}
    
public class MetaData
{
    [JsonProperty("CreateTime")]
    public string CreateTime { get; set; }
        
    [JsonProperty("LastUpdatedTime")]
    public string LastUpdatedTime { get; set; }
}
    
public class Line
{
    [JsonProperty("LineNum")]
    public int LineNum { get; set; }
        
    [JsonProperty("Amount")]
    public double Amount { get; set; }
        
    [JsonProperty("SalesItemLineDetail")]
    public SalesItemLineDetail SalesItemLineDetail { get; set; }
        
    [JsonProperty("Id")]
    public string Id { get; set; }
        
    [JsonProperty("DetailType")]
    public string DetailType { get; set; }
}
    
public class SalesItemLineDetail
{
    [JsonProperty("TaxCodeRef")]
    public TaxCodeRef TaxCodeRef { get; set; }
        
    [JsonProperty("ItemRef")]
    public ItemRef ItemRef { get; set; }
}
    
public class TaxCodeRef
{
    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}
    
public class CustomField
{
    [JsonProperty("DefinitionId")]
    public string DefinitionId { get; set; }
        
    [JsonProperty("Type")]
    public string Type { get; set; }
        
    [JsonProperty("Name")]
    public string Name { get; set; }
    
    [JsonProperty("StringValue")]
    public string? StringValue { get; set; }
}

public class BillEmail
{
    [JsonProperty("Address")]
    public string Address { get; set; }
}