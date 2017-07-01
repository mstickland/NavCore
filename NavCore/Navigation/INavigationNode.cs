using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation {

    public interface INavigationNode {

        /// <summary>
        /// The connections between this node and other navigatable nodes
        /// 
        /// Connections are unidirectional. In order to create a mutal (i.e two way) connection 
        /// the other node needs a connection to this this node as well
        /// </summary>
        IEnumerable<INavigationNode> Connections { get; }

    }
}
