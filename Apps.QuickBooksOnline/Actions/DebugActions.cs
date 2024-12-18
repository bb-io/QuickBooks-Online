using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.QuickBooksOnline.Actions
{
    [ActionList]
    public class DebugActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
    {
        [Display("Debug")]
        public InvocationContext Debug()
        {
            return InvocationContext;
        }
    }

}
