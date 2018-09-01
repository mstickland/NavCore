using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.PathFinders
{
    
    /// <summary>
    /// Finds available paths from 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConnectionFinder<T>
    {
        IEnumerable<T> GetConnections(T t);
    }

}
