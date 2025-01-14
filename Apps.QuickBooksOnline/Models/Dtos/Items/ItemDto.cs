using Apps.QuickBooksOnline.Models.Dtos.Payments;
using Newtonsoft.Json;

namespace Apps.QuickBooksOnline.Models.Dtos.Items;

public class ItemDto
{
    [JsonProperty("Name")]
    public string Name { get; set; }
    
    [JsonProperty("Description")]
    public string Description { get; set; }
    
    [JsonProperty("Active")]
    public bool Active { get; set; }
    
    [JsonProperty("FullyQualifiedName")]
    public string FullyQualifiedName { get; set; }
    
    [JsonProperty("Taxable")]
    public bool Taxable { get; set; }
    
    [JsonProperty("UnitPrice")]
    public double UnitPrice { get; set; }
    
    [JsonProperty("Type")]
    public string Type { get; set; }
    
    [JsonProperty("IncomeAccountRef")]
    public IncomeAccountRefDto IncomeAccountRef { get; set; }

    [JsonProperty("SalesTaxCodeRef")]
    public SalesTaxCodeRefDto? SalesTaxCodeRef { get; set; }

   [JsonProperty("PurchaseCost")] 
   public double PurchaseCost { get; set; }

   public bool TrackQtyOnHand { get; set; }

   public string Domain { get; set; }

   [JsonProperty("sparse")]
   public bool Sparse { get; set; }

   public string Id { get; set; }

   public string SyncToken { get; set; }

   public MetaDataDto MetaData { get; set; }
}

public class IncomeAccountRefDto
{
    [JsonProperty("value")]
    public string Value { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
}
public class SalesTaxCodeRefDto
{
    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}