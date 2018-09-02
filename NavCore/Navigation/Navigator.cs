using NavCore.Navigation.ConnectionFinders;
using NavCore.Navigation.PathWeighters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NavCore.Navigation
{


    /// <summary>
    /// Navigates between two nodes 
    /// </summary>
    /// <typeparam name="TNavNode"></typeparam>
    public class Navigator<TNavNode> : INavigator<TNavNode> //where TNavNode
    {

        /// <summary>
        /// This is what determines how paths are weighted
        /// </summary>
        public IPathWeighter<TNavNode> Comparer { get; set; }
        /// <summary>
        /// Finds connections from a given node to other nodes
        /// </summary>
        public IConnectionFinder<TNavNode> Finder { get; set; }




        public Navigator(IConnectionFinder<TNavNode> finder, IPathWeighter<TNavNode> comparer)
        {
            Finder   = finder;
            Comparer = comparer;
        }

        /// <summary>
        /// Simple constructor that sets the finder and comparer 
        /// </summary>
        /// <param name="comparer"></param>
        public Navigator(Func<TNavNode, IEnumerable<TNavNode>> finder, IPathWeighter<TNavNode> comparer)
        {
            Finder   = new CallbackConnectionFinder<TNavNode>(finder);
            Comparer = comparer;
        }

        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        public Navigator(IConnectionFinder<TNavNode> finder, Func<TNavNode, double> comparison)   {
            Finder   = finder;
            Comparer = new CallbackWeighterOneArg<TNavNode>(comparison);
        }

        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        public Navigator(Func<TNavNode, IEnumerable<TNavNode>> finder, Func<TNavNode, double> comparison)
        {
            Finder   = new CallbackConnectionFinder<TNavNode>(finder);
            Comparer = new CallbackWeighterOneArg<TNavNode>(comparison);
        }

        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        public Navigator(IConnectionFinder<TNavNode> finder, Func<TNavNode, TNavNode, TNavNode, double> comparison) {
            Finder   = finder;
            Comparer = new CallbackWeighterThreeArg<TNavNode>(comparison);
        }


        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        public Navigator(Func<TNavNode, IEnumerable<TNavNode>> finder, Func<TNavNode, TNavNode, TNavNode, double> comparison)
        {
            Finder   = new CallbackConnectionFinder<TNavNode>(finder);
            Comparer = new CallbackWeighterThreeArg<TNavNode>(comparison);
        }

        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        public Navigator(Func<TNavNode, TNavNode, TNavNode, TNavNode, double> comparison) {
            Comparer = new CallbackWeighterFourArg<TNavNode>(comparison);
        }

        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        public Navigator(Func<TNavNode, TNavNode, TNavNode, TNavNode, IEnumerable<TNavNode>, double> comparison) {
            Comparer = new CallbackWeighterFiveArg<TNavNode>(comparison);
        }

        /// <summary>
        /// If successful returns a path from the start node to the end node
        /// </summary>
        /// <param name="start">The node from which our navigation</param>
        /// <param name="destination">The node which are trying to navigate to</param>
        /// <returns>A Navigation result. Will contain information about if we were successful and if so the path we took</returns>
        public INavigationResult<TNavNode> Navigate(TNavNode start, TNavNode destination) {
            
            List<TNavNode> route = new List<TNavNode>();
            double weight = NavigationRecursive(start, start, destination, new List<TNavNode>(), route, 0, null);

            return new NavigationResult<TNavNode>(route) { Success = weight < double.PositiveInfinity};
        }


        /// <summary>
        /// If successful returns a path from the start node to the end node
        /// 
        /// routeStates will be a list of in-progress routes representing the path finding process
        /// </summary>
        /// <param name="start">The node from which our navigation</param>
        /// <param name="destination">The node which are trying to navigate to</param>
        /// <returns>A Navigation result. Will contain information about if we were successful and if so the path we took</returns>
        public INavigationResult<TNavNode> Navigate(TNavNode start, TNavNode destination, IProgress<INavigationResult<TNavNode>> progress = null) {

            List<TNavNode> route = new List<TNavNode>();

            bool success = NavigationRecursive(start, start, destination, new List<TNavNode>(), route, 0, progress) < double.PositiveInfinity;
            
            return new NavigationResult<TNavNode>(route) { Success = success};
        }

        /// <summary>
        /// recursive function for path finding
        /// </summary>
        /// <param name="location"></param>
        /// <param name="destination"></param>
        /// <param name="vistedPlaces"></param>
        /// <param name="route"></param>
        /// <param name="routeStates"></param>
        /// <returns></returns>
        private double NavigationRecursive(TNavNode start, TNavNode location, TNavNode destination, List<TNavNode> vistedPlaces, List<TNavNode> route, double weight, IProgress<INavigationResult<TNavNode>> progress) {

            route.Add(location);
            progress?.Report(new NavigationResult<TNavNode>(new List<TNavNode>(route)) { InProgress = true, Success = false });

            //if (ReferenceEquals(location, destination))
            if (location.Equals(destination))
                return weight;

            vistedPlaces.Add(location);

            var unvisitedConnections = Finder.GetConnections(location).
                    Except(vistedPlaces).
                    Select(n => new { Node = n, Weight = Comparer.GetPathWeight(start, location, (TNavNode)n, destination, route) }).
                    Where(x => x.Weight < double.PositiveInfinity).
                    OrderBy(x => x.Weight);


            foreach (var connection in unvisitedConnections) {
                if (NavigationRecursive(start, (TNavNode)connection.Node, destination, vistedPlaces, route, weight + connection.Weight, progress) < double.PositiveInfinity)
                    return weight + connection.Weight;
            }

            route.Remove(location);

            return double.PositiveInfinity;

        }


    }
}
