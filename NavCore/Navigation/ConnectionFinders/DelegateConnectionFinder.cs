using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.PathFinders
{


    public class DelegateConnectionFinder<T> : IConnectionFinder<T>
    {

        public delegate IEnumerable<T> FindConnectionDelegate(T t);

        private readonly FindConnectionDelegate _dele;

        public IEnumerable<T> GetConnections(T t)
        {
            return _dele(t);
        }

        public DelegateConnectionFinder(FindConnectionDelegate dele)
        {
            _dele = dele;
        }

        public DelegateConnectionFinder(Func<T, IEnumerable<T>> func)
        {
            _dele = new FindConnectionDelegate(func);
        }

    }
}
