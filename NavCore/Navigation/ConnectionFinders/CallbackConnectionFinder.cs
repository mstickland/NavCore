using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.ConnectionFinders
{


    public class CallbackConnectionFinder<T> : IConnectionFinder<T>
    {

        private readonly Func<T, IEnumerable<T>> _dele;

        public IEnumerable<T> GetConnections(T t)
        {
            return _dele(t);
        }
        
        public CallbackConnectionFinder(Func<T, IEnumerable<T>> func)
        {
            _dele = func;
        }

    }
}
