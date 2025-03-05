using Apps.QuickBooksOnline.Api.Models.Requests;

namespace Apps.QuickBooksOnline.Api.Models.Responses
{
    public class GetBillResponse
    {
        public Bill Bill { get; set; }

        public GetBillResponse(Bill bill)
        {
            Bill = bill;
        }
    }
}
