using NavCore.Navigation;
using NavCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.DistanceNavigation {

    public class PositionNode : NavigationNodeBase<PositionNode> {

        /// <summary>
        /// This node's position in 2D space. 
        /// Pathfinding can be weighted
        /// </summary>
        public NavPoint Position { get; set; }

        public PositionNode(string name, NavPoint pos) : base(name) {
            Position = pos;
        }

        /// <summary>
        /// Adds a two way connection - that is it is valid to and from the connection node
        /// </summary>
        /// <param name="node"></param>
        public void AddConnection(PositionNode node) {
            _connections.Add(node);
            node._connections.Add(this);
        }

        /// <summary>
        /// Adds a two way connection - that is it is valid to and from the connection node
        /// </summary>
        /// <param name="node"></param>
        public void AddConnections(IEnumerable<PositionNode> nodes) {
            foreach (var node in nodes)
                AddConnection(node);
        }
    }
}
