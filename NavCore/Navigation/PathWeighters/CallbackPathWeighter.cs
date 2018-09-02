using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.PathWeighters {

    /// <summary>
    /// The most flexible implementation.
    /// Determines best function to use, however this is class is likely to be called alot,
    /// so it may be worth it to use a more specific version to skip the overhead to finding the best method each time.
    /// 
    /// If multiple callbacks are available will default to the one with the most arguments
    /// However there should only ever be one set
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public sealed class CallbackWeighter<TNode> : IPathWeighter<TNode> {


        private readonly Func<TNode, double> _oneArg;
        private readonly Func<TNode, TNode, TNode, double> _threeArg;
        private readonly Func<TNode, TNode, TNode, TNode, double> _fourArg;
        private readonly Func<TNode, TNode, TNode, TNode, IEnumerable<TNode>, double> _fiveArg;

        /// <summary>
        /// Constructor that sets callback
        /// 
        /// /// <summary>
        /// Argument Order: potential
        /// </summary>
        /// </summary>
        /// <param name="callback"></param>
        public CallbackWeighter(Func<TNode, double> callback) {
            _oneArg = callback;
        }


        /// <summary>
        /// Argument Order: current, potential, destination
        /// </summary>
        public CallbackWeighter(Func<TNode, TNode, TNode, double> callback) {
            _threeArg = callback;
        }

        /// <summary>
        /// Argument Order: start, current, potential, destination
        /// </summary>
        public CallbackWeighter(Func<TNode, TNode, TNode, TNode, double> callback) {
            _fourArg = callback;
        }

        /// <summary>
        /// Argument Order: start, current, potential, destination, pathSoFar
        /// </summary>
        /// <param name="callback"></param>
        public CallbackWeighter(Func<TNode, TNode, TNode, TNode, IEnumerable<TNode>, double> callback) {
            _fiveArg = callback;
        }

        /// <summary>
        /// Determines weight based via callback
        /// </summary>
        /// <param name="start">The origin node of the path</param>
        /// <param name="current">the current/most recent node in the path</param>
        /// <param name="potential">The node we are currently evaluating.</param>
        /// <param name="destination">The destination node. Where we need to reach</param>
        /// <param name="pathSoFar">A list of every node in our current path</param>
        /// <returns>The weight/cost of traveling from current node to potential node</returns>
        /// <returns></returns>
        public double GetPathWeight(TNode start, TNode current, TNode potential, TNode destination, IEnumerable<TNode> pathSoFar) {

            return _fiveArg?.Invoke(start, current, potential, destination, pathSoFar) ?? 
                   _fourArg?.Invoke(start, current, potential, destination) ??
                   _threeArg?.Invoke(current, potential, destination) ??
                   _oneArg(potential);            
        }
    }


    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public sealed class CallbackWeighterOneArg<TNode> : IPathWeighter<TNode> {


        private readonly Func<TNode, double> _callback;

        /// <summary>
        /// Constructor that sets callback
        /// </summary>
        /// <param name="callback"></param>
        public CallbackWeighterOneArg(Func<TNode, double> callback) {
            _callback = callback;
        }

        /// <summary>
        /// Determines weight based via callback
        /// </summary>
        /// <param name="start">The origin node of the path</param>
        /// <param name="current">the current/most recent node in the path</param>
        /// <param name="potential">The node we are currently evaluating.</param>
        /// <param name="destination">The destination node. Where we need to reach</param>
        /// <param name="pathSoFar">A list of every node in our current path</param>
        /// <returns>The weight/cost of traveling from current node to potential node</returns>
        public double GetPathWeight(TNode start, TNode current, TNode potential, TNode destination, IEnumerable<TNode> pathSoFar) {
            return _callback(potential);
        }
    }

    public sealed class CallbackWeighterThreeArg<TNode> : IPathWeighter<TNode> {

        
        private readonly Func<TNode, TNode, TNode, double> _callback;

        /// <summary>
        /// Constructor that sets callback
        /// </summary>
        /// <param name="callback"></param>
        public CallbackWeighterThreeArg(Func<TNode, TNode, TNode, double> callback) {
            _callback = callback;
        }

        /// <summary>
        /// Determines weight based via callback
        /// </summary>
        /// <param name="start">The origin node of the path</param>
        /// <param name="current">the current/most recent node in the path</param>
        /// <param name="potential">The node we are currently evaluating.</param>
        /// <param name="destination">The destination node. Where we need to reach</param>
        /// <param name="pathSoFar">A list of every node in our current path</param>
        /// <returns>The weight/cost of traveling from current node to potential node</returns>
        public double GetPathWeight(TNode start, TNode current, TNode potential, TNode destination, IEnumerable<TNode> pathSoFar) {
            return _callback(current, potential, destination);
        }
    }

    public sealed class CallbackWeighterFourArg<TNode> : IPathWeighter<TNode> {


        private readonly Func<TNode, TNode, TNode, TNode, double> _callback;

        /// <summary>
        /// Constructor that sets callback
        /// </summary>
        /// <param name="callback"></param>
        public CallbackWeighterFourArg(Func<TNode, TNode, TNode, TNode, double> callback) {
            _callback = callback;
        }

        /// <summary>
        /// Determines weight based via callback
        /// </summary>
        /// <param name="start">The origin node of the path</param>
        /// <param name="current">the current/most recent node in the path</param>
        /// <param name="potential">The node we are currently evaluating.</param>
        /// <param name="destination">The destination node. Where we need to reach</param>
        /// <param name="pathSoFar">A list of every node in our current path</param>
        /// <returns>The weight/cost of traveling from current node to potential node</returns>
        public double GetPathWeight(TNode start, TNode current, TNode potential, TNode destination, IEnumerable<TNode> pathSoFar) {
            return _callback(start, current, potential, destination);
        }
    }

    public sealed class CallbackWeighterFiveArg<TNode> : IPathWeighter<TNode> {


        private readonly Func<TNode, TNode, TNode, TNode, IEnumerable<TNode>, double> _callback;

        /// <summary>
        /// Constructor that sets callback
        /// </summary>
        /// <param name="callback"></param>
        public CallbackWeighterFiveArg(Func<TNode, TNode, TNode, TNode, IEnumerable<TNode>, double> callback) {
            _callback = callback;
        }


        /// <summary>
        /// Determines weight based via callback
        /// </summary>
        /// <param name="start">The origin node of the path</param>
        /// <param name="current">the current/most recent node in the path</param>
        /// <param name="potential">The node we are currently evaluating.</param>
        /// <param name="destination">The destination node. Where we need to reach</param>
        /// <param name="pathSoFar">A list of every node in our current path</param>
        /// <returns>The weight/cost of traveling from current node to potential node</returns>
        public double GetPathWeight(TNode start, TNode current, TNode potential, TNode destination, IEnumerable<TNode> pathSoFar) {
            return _callback(start, current, potential, destination, pathSoFar);
        }
    }
}
