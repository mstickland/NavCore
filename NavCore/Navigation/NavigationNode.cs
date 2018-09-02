using NavCore.Navigation.ConnectionFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation {


    public class NavigationNode : NavigationNodeBase<NavigationNode>, IHasConnections<NavigationNode> {

        /// <summary>
        /// Passes name to base constructor
        /// </summary>
        /// <param name="name"></param>
        public NavigationNode(string name) : base (name) {
        }



        /// <summary>
        /// Adds a two way connection - that is it is valid to and from the connection node
        /// </summary>
        /// <param name="node"></param>
        public void AddConnection(NavigationNode node) {
            _connections.Add(node);
            node._connections.Add(this);
        }

        /// <summary>
        /// Adds a two way connection - that is it is valid to and from the connection node
        /// </summary>
        /// <param name="node"></param>
        public void AddConnections(IEnumerable<NavigationNode> nodes) {
            foreach (var node in nodes)
                AddConnection(node);
        }



    }
}
