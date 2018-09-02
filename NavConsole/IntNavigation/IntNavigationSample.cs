using NavCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole.IntNavigation
{
    public class IntNavigationSample
    {

        public static void Start()
        {
            var sample = new IntNavigationSample();

            sample.SimpleSample();
            sample.ALittleMoreComplexSample();
        }



        private void SimpleSample()
        {
            Console.WriteLine("Simple Int Navigation");
            //in this is sample each int is connection to the integer one greater (returned as an array)
            //and all ints have an equal weighting of one
            var navigator = new Navigator<int>(i => new [] { i + 1}, i => 1);

            var path = navigator.Navigate(0, 10);
            Console.WriteLine(path);
            path = navigator.Navigate(50, 55);
            Console.WriteLine(path);
        }

        private void ALittleMoreComplexSample()
        {
            Console.WriteLine("A little more complex int Navigation");
            //in this is sample each int is next in to the number 1 greater, 1 greater, and 1 less
            //the navigator will use the fewest jumps, because the wieghing prefers numbers that are closer to the destination
            var navigator = new Navigator<int>(i => new[] { i + 1, i + 5, i - 1 }, (current, potential, destination) => Math.Abs(destination - potential));

            var path = navigator.Navigate(0, 53);
            Console.WriteLine(path);
        }

    }
}
