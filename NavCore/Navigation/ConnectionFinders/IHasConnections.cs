using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavCore.Navigation.ConnectionFinders
{

    public interface IHasConnections<T> 
    {
        IEnumerable<T> Connections { get; }
    }

}
