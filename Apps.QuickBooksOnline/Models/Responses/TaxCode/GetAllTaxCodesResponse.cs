using Apps.QuickBooksOnline.Models.Dtos.TaxCodes;

namespace Apps.QuickBooksOnline.Models.Responses.TaxCode;

public class GetAllTaxCodesResponse
{
    public List<TaxCodeResponse> TaxCodes { get; set; }
    
    public GetAllTaxCodesResponse(List<TaxCodeDto> dtos)
    {
        TaxCodes = dtos.Select(dto => new TaxCodeResponse(dto)).ToList();
    }
}