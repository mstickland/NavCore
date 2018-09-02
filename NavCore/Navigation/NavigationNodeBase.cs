using NavCore.Navigation.ConnectionFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NavCore.Navigation {

    /// <summary>
    /// Represents one node used for navigation
    /// </summary>
    public class NavigationNodeBase<T>: INavigationNode<T>, IHasConnections<T> {
        
        /// <summary>
        /// connections/paths to other nodes
        /// </summary>
        protected List<T> _connections = new List<T>();
        public IEnumerable<T> Connections { get { return _connections; } } 

        /// <summary>
        /// Identitifer largely for iding it in a navigation route
        /// </summary>
        public string Name { get; set; }


        //IEnumerable<T> INavigationNode.Connections {
        //    get {
        //        return Connections;
        //    }
        //}

        private const int RandIdLength = 5;
        /// <summary>
        /// Defaults the name to random string
        /// </summary>
        public NavigationNodeBase() {
            Name = RandomString(RandIdLength);
        }


        /// <summary>
        /// Generates a random string of length 'length'
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string RandomString(int length) {

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Constructor that sets the name
        /// </summary>
        /// <param name="name"></param>
        public NavigationNodeBase(string name) {
            Name = name;
        }

        /// <summary>
        /// Adds a one way connection - that is you can travel to the node but not back
        /// </summary>
        /// <param name="nodes"></param>
        public void AddOneWayConnection(T node) {
            _connections.Add(node);
        }

        /// <summary>
        /// Adds a one way connections - see AddOneWayConnection
        /// </summary>
        /// <param name="nodes"></param>
        public void AddOneWayConnections(IEnumerable<T> nodes) {
            foreach (var node in nodes) 
                AddOneWayConnection(node);            
        }

        /// <summary>
        /// returns the name of this node
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Name;
        }

    }


}
