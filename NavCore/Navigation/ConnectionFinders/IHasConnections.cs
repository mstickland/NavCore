using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.PathFinders
{

    public interface IHasConnections<T> where T : IHasConnections<T>
    {
        IEnumerable<T> Connections { get; }
    }

}
