using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.QuickBooksOnline.Contracts
{
    public class AddInvoiceLineParameters : CreateInvoiceParameters
    {
        public string InvoiceId { get; set; }

        public string SyncToken { get; set; }
    }
}
