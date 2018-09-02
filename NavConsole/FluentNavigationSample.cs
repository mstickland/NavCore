using NavConsole.LetterNavigation;
using NavCore.Fluent;
using NavCore.Navigation;
using NavCore.Navigation.ConnectionFinders;
using NavCore.Navigation.PathWeighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole
{
    internal class FluentNavigationSample
    {


        /// <summary>
        /// Letter navigation using fluent syntax
        /// </summary>
        public static void Start()
        {

            IPathWeighter<char> heuristic = new CallbackWeighter<char>(node => LetterFinder.IsVowel(node) ? 0 : 1);
            IConnectionFinder<char> connectionFinder = new LetterFinder();

            Console.WriteLine("Works exactly the same as other example, but with nice fluent syntax.");
            
            //Interfaces make function calls more straight forward
            var path = FluentNavigation
                                .StartNavigation<char>()
                                .From('a')
                                .To('z')
                                .FindConnectionsWith(connectionFinder)
                                .Using(heuristic)
                                .GetPath();

            Console.WriteLine(path);
        }
    }
}
