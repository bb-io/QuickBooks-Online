using Apps.QuickBooksOnline.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.QuickBooksOnline.Models.Requests.Classes;

public class ClassRequest
{
    [Display("Class ID"), DataSource(typeof(ClassDataHandler))]
    public string ClassId { get; set; }
}