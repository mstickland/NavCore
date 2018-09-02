using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.ConnectionFinders
{

    /// <summary>
    /// Finds available connections from an object 
    /// 
    /// A typical example would be finding connections from one pathfinding node
    /// to other nodes that can be directly pathed (i.e. one jump) from that node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConnectionFinder<T>
    {
        /// <summary>
        /// returns a list of connections from the argument t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>a list nodes directly connected to t</returns>
        IEnumerable<T> GetConnections(T t);
    }

}
