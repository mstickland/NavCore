using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.ConnectionFinders
{

    /// <summary>
    /// Implements the IConnectionFinder interface via a callback function
    /// 
    /// Provides a quick and easy way to implement IConnectionFinder without creating a custom class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CallbackConnectionFinder<T> : IConnectionFinder<T>
    {

        private readonly Func<T, IEnumerable<T>> _dele;

        /// <summary>
        /// returns a list of connections from the argument t
        /// </summary>
        /// <param name="t"></param>
        /// <returns>a list nodes directly connected to t</returns>
        public IEnumerable<T> GetConnections(T t)
        {
            return _dele(t);
        }
        
        /// <summary>
        /// A constructor that takes a function to use as the callback
        /// </summary>
        /// <param name="func"></param>
        public CallbackConnectionFinder(Func<T, IEnumerable<T>> func)
        {
            _dele = func;
        }

        public static implicit operator CallbackConnectionFinder<T>(Func<T, IEnumerable<T>> func)
        {
            return new CallbackConnectionFinder<T>(func);
        }

    }
}
