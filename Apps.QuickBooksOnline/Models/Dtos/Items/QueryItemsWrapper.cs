using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Items;

public class QueryItemsWrapper
{
    [JsonProperty("QueryResponse")]
    public ItemsWrapper QueryResponse { get; set; }
}

public class ItemsWrapper
{
    [JsonProperty("Item")]
    public List<ItemDto> Item { get; set; }
}
