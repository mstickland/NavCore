using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.ConnectionFinders
{

    /// <summary>
    /// Simple connection finder, requires that the generic type implements IHasConnections, no other setup needed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleConnectionFinder<T> : IConnectionFinder<T> where T : IHasConnections<T>
    {
        public IEnumerable<T> GetConnections(T t) => t.Connections;
    }

}
