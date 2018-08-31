using NavCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NavConsole {
    class Program {

        /// <summary>
        /// Program entry point. Runs Sample Navigation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

            new SampleNavigation().Run(args);

            Console.WriteLine();
            Console.Write("Done. Press <ENTER> to exit.");
            Console.ReadLine();

        }

        

    }
}
