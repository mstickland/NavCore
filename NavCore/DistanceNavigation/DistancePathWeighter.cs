using NavCore.Navigation;
using NavCore.Navigation.PathWeighters;
using NavCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.DistanceNavigation {

    public class DistancePathWeighter : IPathWeighter<PositionNode>  {

        public double ImmediateDistWeight { get; set; } = 0.4;
        public double DestinationDistWeight { get; set; } = 0.6;

        public void SetWeights(double immediateWeight, double destWeight) {
            ImmediateDistWeight     = immediateWeight;
            DestinationDistWeight   = destWeight;
        }

        public double GetPathWeight(PositionNode start, PositionNode current, PositionNode potential, PositionNode destination, IEnumerable<PositionNode> path) {
            return NodeMath.GetDistance(potential.Position, current.Position) * ImmediateDistWeight +
                   NodeMath.GetDistance(potential.Position, destination.Position) * DestinationDistWeight;
        }


    }
}
