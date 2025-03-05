using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.QuickBooksOnline.Models.Dtos;
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
    }
    public class BillWrapper
    {
        [JsonProperty("Bill")]
        public Bill Bill { get; set; }
    }

    public class Bill
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("VendorRef")]
        public VendorRef VendorRef { get; set; }

        [JsonProperty("TxnDate")]
        public string TxnDate { get; set; }

        [JsonProperty("DueDate")]
        public string DueDate { get; set; }

        [JsonProperty("DocNumber")]
        public string DocNumber { get; set; }

        [JsonProperty("Line")]
        public IEnumerable<ExpenseLine> Line { get; set; }
    }
    public class ExpenseLine
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("DetailType")]
        public string DetailType { get; set; }

        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [JsonProperty("AccountBasedExpenseLineDetail")]
        public AccountBasedExpenseLineDetail AccountBasedExpenseLineDetail { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }


    public class AccountBasedExpenseLineDetail
    {
        [JsonProperty("AccountRef")]
        public AccountRef AccountRef { get; set; }
    }
    public class AccountRef
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class VendorRef
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
