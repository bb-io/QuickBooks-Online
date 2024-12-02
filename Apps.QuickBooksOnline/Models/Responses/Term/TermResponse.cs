using Apps.QuickBooksOnline.Models.Dtos.Terms;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses.Term
{
    public class TermResponse(TermDto dto)
    {
        [Display("Term ID")]
        public string Id { get; set; } = dto.Id;

        public string Name { get; set; } = dto.Name;
    }
}
