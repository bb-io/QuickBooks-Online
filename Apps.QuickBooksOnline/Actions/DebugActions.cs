using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.QuickBooksOnline.Actions;

[ActionList("Debug")]
public class DebugActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Debug", Description = "For debugging purposes")]
    public DebugResponse Debug()
    {
        var token = Creds.First(p => p.KeyName == "Authorization");
        return new DebugResponse
        {
            Token = token.Value
        };
    }
}

public class DebugResponse
{
    public string Token { get; set; }
}
