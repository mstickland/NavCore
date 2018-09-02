using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation {


    /// <summary>
    /// Finds optimal path between two nodes using AStar
    /// </summary>
    public interface INavigator<TNavNode>  {

        /// <summary>
        /// Returns a path between the two nodes
        /// </summary>
        /// <param name="start">The node to start on</param>
        /// <param name="destination">The node to navigate to</param>
        /// <returns></returns>
        INavigationResult<TNavNode> Navigate(TNavNode start, TNavNode destination);

        INavigationResult<TNavNode> Navigate(TNavNode start, TNavNode destination, IProgress<INavigationResult<TNavNode>> progress);
    }
}
