using NavCore.DistanceNavigation;
using NavCore.Navigation;
using NavCore.Navigation.ConnectionFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole.DistanceNavigation
{
    internal class DistanceNavigationSample
    {

        /// <summary>
        /// Demonstrates sample navigation using Euclidian distance.
        /// Also demonstrates the effect of properly weighting your heuristic.
        /// 
        /// Map would some thing like this:
        /// 
        ///          ce-+ 
        ///   w     /   e
        ///    \   |   / |
        ///     \  |  m  |
        ///      \ | /   |
        ///        s    /
        ///         \  /
        ///          cs
        ///          
        /// Legend:
        /// s: start
        /// e: end
        /// w: wrong
        /// m: middle
        /// cs: CloseToStart
        /// ce: CloseToEnd
        /// 
        /// We can see that the most effective path is via middle node
        /// </summary>
        public static void Start()
        {

            var start        = new PositionNode("Start", new NavPoint(0, 0));
            var end          = new PositionNode("End", new NavPoint(100, 100));
            var middle       = new PositionNode("Middle", new NavPoint(50, 50));
            var wrong        = new PositionNode("WrongDirection", new NavPoint(-100, -100));
            var closeToStart = new PositionNode("CloseToStart", new NavPoint(20, -20));
            var closeToEnd   = new PositionNode("CloseToEnd", new NavPoint(100, 120));

            start.AddConnections(new[] { wrong, middle, closeToStart, closeToEnd });
            middle.AddConnection(end);
            closeToEnd.AddConnection(end);
            closeToStart.AddConnection(end);


            var comparer  = new DistancePathWeighter();
            var navigator = new Navigator<PositionNode>(new SimpleConnectionFinder<PositionNode>(), comparer);

            //Notice that the navigator will always find a path if one exists
            //However effectively weighting our heuristic can improve our results
            Console.WriteLine("Distance Navigation - with different weights:");
            //using default weighting
            Console.WriteLine(navigator.Navigate(start, end));
            comparer.SetWeights(0.9, 0.1);
            Console.WriteLine(navigator.Navigate(start, end));
            comparer.SetWeights(0.1, 0.9);
            Console.WriteLine(navigator.Navigate(start, end));

        }
    }
}
