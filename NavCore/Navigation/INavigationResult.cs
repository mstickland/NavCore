using System.Collections.Generic;

namespace NavCore.Navigation {

    /// <summary>
    /// Represents the results of an attempted navigation
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public interface INavigationResult<TNode> {

        /// <summary>
        /// True if a path to the destination node was found
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// The navigated route. Will be empty if Success is false
        /// </summary>
        IEnumerable<TNode> Route { get; }

        double TotalWeight { get; }


    }
}