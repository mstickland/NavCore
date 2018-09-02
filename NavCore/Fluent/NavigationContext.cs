using NavCore.Navigation;
using NavCore.Navigation.ConnectionFinders;
using NavCore.Navigation.PathWeighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Fluent
{

    public interface IToOrFromable<TNode> 
    {
        IToable<TNode> From(TNode node);
        IFromable<TNode> To(TNode node);
    }

    public interface IFromable<TNode> 
    {
        IFindConnectionsWithable<TNode> From(TNode node);
    }

    public interface IToable<TNode> 
    {
        IFindConnectionsWithable<TNode> To(TNode node);
    }

    public interface IUsingWithable<TNode> 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        IGetPathable<TNode> Using(IPathWeighter<TNode> comparer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        IGetPathable<TNode> Using(Func<TNode, double> comparison);
        /// <summary>
        /// Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        IGetPathable<TNode> Using(Func<TNode, TNode, TNode, double> comparison);
        /// <summary>
        ///  Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        IGetPathable<TNode> Using(Func<TNode, TNode, TNode, TNode, double> comparison);

        /// <summary>
        /// Constructor. Takes a function reference that will be used to determine how paths are weighted
        /// </summary>
        /// <param name="comparison"></param>
        IGetPathable<TNode> Using(Func<TNode, TNode, TNode, TNode, IEnumerable<TNode>, double> comparison);
    }

    public interface IGetPathable<TNode> 
    {
        INavigationResult<TNode> GetPath();
    }

    public interface IFindConnectionsWithable<TNode>
    {
        IUsingWithable<TNode> FindConnectionsWith(IConnectionFinder<TNode> finder);
        IUsingWithable<TNode> FindConnectionsWith(Func<TNode, IEnumerable<TNode>> func);
    }

    public class NavigationContext<TNode> : IFromable<TNode>, IToable<TNode>, IUsingWithable<TNode>, IGetPathable<TNode>, IToOrFromable<TNode>, IFindConnectionsWithable<TNode>            
    {

        private IPathWeighter<TNode> _heuristic;
        private IConnectionFinder<TNode> _finder;//TODO:

        private TNode _from;
        private TNode _to;

        /// <summary>
        /// hide constructor 
        /// </summary>
        internal NavigationContext()
        {

        }

        public IUsingWithable<TNode> FindConnectionsWith(IConnectionFinder<TNode> finder)
        {
            _finder = finder;
            return this;
        }

        public IUsingWithable<TNode> FindConnectionsWith(Func<TNode, IEnumerable<TNode>> func)
        {
            _finder = new CallbackConnectionFinder<TNode>(func);
            return this;
        }

        IFindConnectionsWithable<TNode> IFromable<TNode>.From(TNode node)
        {
            _from = node;
            return this;
        }

        IFindConnectionsWithable<TNode> IToable<TNode>.To(TNode node)
        {
            _to = node;
            return this;
        }

        IFromable<TNode> IToOrFromable<TNode>.To(TNode node)
        {
            _to = node;
            return this;
        }


        IToable<TNode> IToOrFromable<TNode>.From(TNode node)
        {
            _from = node;
            return this;
        }

        INavigationResult<TNode> IGetPathable<TNode>.GetPath()
        {

            var navigator = new Navigator<TNode>(_finder, _heuristic);
            return navigator.Navigate(_from, _to);
        }



        IGetPathable<TNode> IUsingWithable<TNode>.Using(IPathWeighter<TNode> comparer)
        {
            _heuristic = comparer;
            return this;
        }

        IGetPathable<TNode> IUsingWithable<TNode>.Using(Func<TNode, double> comparison)
        {
            _heuristic = new CallbackWeighter<TNode>(comparison);
            return this;
        }

        IGetPathable<TNode> IUsingWithable<TNode>.Using(Func<TNode, TNode, TNode, double> comparison)
        {
            _heuristic = new CallbackWeighter<TNode>(comparison);
            return this;
        }

        IGetPathable<TNode> IUsingWithable<TNode>.Using(Func<TNode, TNode, TNode, TNode, double> comparison)
        {
            _heuristic = new CallbackWeighter<TNode>(comparison);
            return this;
        }

        IGetPathable<TNode> IUsingWithable<TNode>.Using(Func<TNode, TNode, TNode, TNode, IEnumerable<TNode>, double> comparison)
        {
            _heuristic = new CallbackWeighter<TNode>(comparison);
            return this;
        }
    }
}
