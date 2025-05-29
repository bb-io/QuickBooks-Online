using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Api.Models.Requests
{
    public class CreateBillRequest
    {
        [Display("Vendor ID"), DataSource(typeof(VendorDataSource))]
        public string VendorId { get; set; }

        [Display("Category IDs", Description = "Account references for each line item"), DataSource(typeof(AccountDataSource))]
        public IEnumerable<string> AccountIds { get; set; }

        [Display("Line amounts", Description = "Amounts for each line item")]
        public IEnumerable<double> LineAmounts { get; set; }

        [Display("Descriptions", Description = "Optional descriptions for each line item")]
        public IEnumerable<string>? Descriptions { get; set; }

        [Display("Bill date")]
        public DateTime? BillDate { get; set; }

        [Display("Due date")]
        public DateTime? DueDate { get; set; }

        [Display("Doc number")]
        public string? DocNumber { get; set; }

        [Display("Private note")]
        public string? PrivateNote { get; set; }

        //[DataSource(typeof(DepartmentDataSource))]
        //public string? Department { get; set; }

        [Display("Sales terms"), DataSource(typeof(TermDataSource))]
        public string? SalesTerms { get; set; }

        //[Display("Tax code"), DataSource(typeof(TaxCodeDataHandler))]
        //public IEnumerable<string>? TaxCode { get; set; }
    }
}
