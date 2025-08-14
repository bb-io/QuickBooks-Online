using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Api.Models.Requests
{
    public class CreateBillRequestBody
    {
        [JsonProperty("Line")]
        public List<ExpenseLine> Line { get; set; }

        [JsonProperty("VendorRef")]
        public VendorRef VendorRef { get; set; }

        [JsonProperty("TxnDate")]
        public string TxnDate { get; set; }

        [JsonProperty("DueDate")]
        public string DueDate { get; set; }

        [JsonProperty("DocNumber")]
        public string DocNumber { get; set; }

        [JsonProperty("PrivateNote")]
        public string PrivateNote { get; set; }

        [JsonProperty("SalesTermRef")]
        public SalesTermRef? SalesTermRef { get; set; }

        [JsonProperty("DepartmentRef")]
        public DepartmentRef? DepartmentRef { get; set; }

        [JsonProperty("CurrencyRef")]
        public CurrencyRef? CurrencyRef { get; set; }

        [JsonProperty("ExchangeRate")]
        public decimal? ExchangeRate { get; set; }

        [JsonProperty("GlobalTaxCalculation")]
        public string? GlobalTaxCalculation { get; set; }
    }
    public class BillWrapper
    {
        [JsonProperty("Bill")]
        public Bill Bill { get; set; }
    }

    public class Bill
    {
        [JsonProperty("Id")]
        [Display("Bill ID")]
        public string Id { get; set; }

        [JsonProperty("VendorRef")]
        [Display("Vendor")]
        public VendorRef VendorRef { get; set; }

        [JsonProperty("TxnDate")]
        public string TxnDate { get; set; }

        [JsonProperty("DueDate")]
        [Display("Due date")]
        public string DueDate { get; set; }

        [JsonProperty("DocNumber")]
        [Display("Document number")]
        public string DocNumber { get; set; }

        [JsonProperty("Line")]
        public IEnumerable<ExpenseLine> Line { get; set; }

        [JsonProperty("DepartmentRef")]
        public DepartmentRef? DepartmentRef { get; set; }

        [JsonProperty("SalesTermRef")]
        public SalesTermRef? SalesTermRef { get; set; }

        [JsonProperty("CurrencyRef")]
        public CurrencyRef? CurrencyRef { get; set; }
    }
    public class ExpenseLine
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("DetailType")]
        [Display("Detail type")]
        public string DetailType { get; set; }

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [JsonProperty("AccountBasedExpenseLineDetail")]
        [Display("Account based expense line detail")]
        public AccountBasedExpenseLineDetail AccountBasedExpenseLineDetail { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }


    public class AccountBasedExpenseLineDetail
    {
        [JsonProperty("TaxCodeRef")]
        public TaxCodeRef? TaxCodeRef { get; set; }

        [JsonProperty("AccountRef")]
        [Display("Account reference")]
        public AccountRef AccountRef { get; set; }        
    }
    public class AccountRef
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class CurrencyRef
    {
        [JsonProperty("value")]
        public string? Value { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

    }

    public class VendorRef
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class RateWrapper
    {
        public ExchangeRate ExchangeRate { get; set; }
    }

    public class ExchangeRate
    {
        public string SourceCurrencyCode { get; set; }
        public string TargetCurrencyCode { get; set; }
        public decimal Rate { get; set; }
    }
}
