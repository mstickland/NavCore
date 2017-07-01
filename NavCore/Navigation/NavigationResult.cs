using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation {


    /// <summary>
    /// Represents the results of an attempted navigation
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public class NavigationResult<TNode> : INavigationResult<TNode> {

        /// <summary>
        /// True if a path to the destination node was found
        /// </summary>
        public bool Success { get; internal set; } = false;


        /// <summary>
        /// TODO:
        /// </summary>
        public bool InProgress { get; internal set; } = false;

        /// <summary>
        /// The navigated route. Will be empty if Success is false
        /// </summary>
        public IReadOnlyCollection<TNode> Route { get; private set; }
        public double TotalWeight { get; }

        /// <summary>
        /// No arg constructor
        /// </summary>
        public NavigationResult() {
            Route = new List<TNode>();
        }

        public NavigationResult(List<TNode> route) {
            Route = route;
        }


        /// <summary>
        /// If a successful route is found,
        /// will represent that route as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString() {

            if((!Success && !InProgress) || Route == null || !Route.Any())
                return "Navigation Failed";
            return String.Join("-->", Route);
        }
    }

}
