using Apps.QuickBooksOnline.Models.Dtos.Terms;

namespace Apps.QuickBooksOnline.Models.Responses.Term;

public class GetAllTermsResponse
{
    public List<TermResponse> Terms { get; set; }
    
    public GetAllTermsResponse(List<TermDto> dtos)
    {
        Terms = dtos.Select(dto => new TermResponse(dto)).ToList();
    }
}