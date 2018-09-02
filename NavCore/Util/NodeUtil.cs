using NavCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Util {




    public class NodeUtil {

        public static void AddConnectionsInSeries<T>(T start, IEnumerable<T> nodes) where T : NavigationNodeBase<T>
        {

            if (start == null)
                throw new ArgumentException("start node must not be null.");
            if (nodes == null)
                throw new ArgumentException("nodes must not be null.");

            T lastNode = start;

            foreach (var node in nodes)
            {
                lastNode.AddOneWayConnection(node);
                lastNode = node;
            }
        }

        public static void AddConnectionsInSeries<T>(IEnumerable<T> nodes) where T : NavigationNodeBase<T>
        {


            if (nodes == null)
                throw new ArgumentException("nodes must not be null.");
            if (nodes.Count() < 1)
                throw new ArgumentException("nodes must have a size of at least one.");

            T lastNode = nodes.First();

            foreach (var node in nodes.Skip(1))
            {
                lastNode.AddOneWayConnection(node);
                lastNode = node;
            }
        }

    }
}
