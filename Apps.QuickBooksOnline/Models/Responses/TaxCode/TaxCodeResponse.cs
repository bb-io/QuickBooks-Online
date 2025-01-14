using Apps.QuickBooksOnline.Models.Dtos.TaxCodes;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.TaxCode
{
    public class TaxCodeResponse(TaxCodeDto dto)
    {
        [Display("Tax code ID")]
        public string Id { get; set; } = dto.Id;

        public string Name { get; set; } = dto.Name;
    }
}
