using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.PathWeighters {

    public interface IPathWeighter<TNode> {

        double GetPathWeight(TNode start, TNode current, TNode potential, TNode destination, IEnumerable<TNode> pathSoFar);

    }
}
