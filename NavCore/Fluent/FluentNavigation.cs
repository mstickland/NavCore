using NavCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Fluent
{
    /// <summary>
    /// Provides a Fluent interface for navigation.
    /// 
    /// Example Usage:
    /// 
    /// FluentNavigation.StartNavigation()
    ///                 .From(somePlace)
    ///                 .To(someOtherPlace)
    ///                 .With(navigationHueristic)
    ///                 .GetPath();
    /// 
    /// </summary>
    public class FluentNavigation
    {
        

        public static IToOrFromable<TNode> StartNavigation<TNode>() where TNode : class, INavigationNode {
            return new NavigationContext<TNode>();
        }
    }
}
