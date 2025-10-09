using Apps.QuickBooksOnline.Models.Dtos.Items;
using Apps.QuickBooksOnline.Models.Requests;
using Apps.QuickBooksOnline.Models.Responses.Items;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.QuickBooksOnline.Actions;

[ActionList("Items")]
public class ItemActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all items", Description = "Get all items")]
    public async Task<GetAllItemsResponse> GetAllItems([ActionParameter] FilterRequest filterRequest)
    {
        var sql = "select * from Item";

        if (filterRequest.Limit.HasValue)
        {
            sql += $" maxresults {filterRequest.Limit.Value}";
        }
        
        var itemsWrapper =
            await Client.ExecuteWithJson<QueryItemsWrapper>($"/query?query={sql}", Method.Get, null, Creds);
            
        return new GetAllItemsResponse(itemsWrapper.QueryResponse.Item);
    }
    
    [Action("Get item", Description = "Get item by ID")]
    public async Task<ItemResponse> GetItem([ActionParameter] string id)
    {
        var itemWrapper = await Client.ExecuteWithJson<ItemWrapper>($"/item/{id}", Method.Get, null, Creds);
        return new(itemWrapper.Item);
    }
    
    public async Task<List<ItemResponse>> GetItemsByIds(IEnumerable<string> ids)
    {
        var items = new List<ItemResponse>();
        
        foreach (var id in ids)
        {
            var item = await GetItem(id);
            items.Add(item);
        }
        
        return items;
    }
}