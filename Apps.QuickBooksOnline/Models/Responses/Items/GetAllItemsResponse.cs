using Apps.QuickBooksOnline.Models.Dtos.Items;

namespace Apps.QuickBooksOnline.Models.Responses.Items;

public class GetAllItemsResponse
{
    public List<ItemResponse> Items { get; set; }
    
    public GetAllItemsResponse(List<ItemDto> dtos)
    {
        Items = dtos.Select(dto => new ItemResponse(dto)).ToList();
    }
}