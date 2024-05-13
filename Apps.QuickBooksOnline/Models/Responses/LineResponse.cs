using Apps.QuickBooksOnline.Models.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.QuickBooksOnline.Models.Responses;

public class LineResponse
{
    public double Amount { get; set; }
    
    [Display("Linked transactions")]
    public List<LinkedTxnResponse> LinkedTxn { get; set; }
    
    [Display("Line extended")]
    public List<NameValueResponse> LineEx { get; set; }

    public LineResponse(LineDto dto)
    {
        Amount = dto.Amount;
        LinkedTxn = dto.LinkedTxn.Select(x => new LinkedTxnResponse
        {
            TxnId = x.TxnId,
            TxnType = x.TxnType
        }).ToList();
        LineEx = dto.LineEx.Any.Select(x => new NameValueResponse
        {
            UrlName = x.UrlName,
            Name = x.Value.Name,
            Value = x.Value.Value,
            DeclaredType = x.DeclaredType,
            Scope = x.Scope,
            Nil = x.Nil,
            GlobalScope = x.GlobalScope,
            TypeSubstituted = x.TypeSubstituted
        }).ToList();
    }
}

public class LinkedTxnResponse
{
    [Display("Transaction ID")]
    public string TxnId { get; set; }
    
    [Display("Transaction type")]
    public string TxnType { get; set; }
}

public class NameValueResponse
{
    [Display("URL name")]
    public string UrlName { get; set; }
    
    [Display("Name")]
    public string Name { get; set; }
    
    [Display("Value")]
    public string Value { get; set; }
    
    [Display("Declared type")]
    public string DeclaredType { get; set; }
    
    [Display("Scope")]
    public string Scope { get; set; }
    
    [Display("Nil")]
    public bool Nil { get; set; }
    
    [Display("Global scope")]
    public bool GlobalScope { get; set; }
    
    [Display("Type substituted")]
    public bool TypeSubstituted { get; set; }
}