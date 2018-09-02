using NavConsole.DistanceNavigation;
using NavConsole.IntNavigation;
using NavConsole.LetterNavigation;
using NavCore.DistanceNavigation;
using NavCore.Fluent;
using NavCore.Navigation;
using NavCore.Navigation.ConnectionFinders;
using NavCore.Navigation.PathWeighters;
using NavCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NavConsole {

    /// <summary>
    /// A collection of example use cases for navigation.
    /// Note that some cases are meant to be more illustrativw than directly useful.
    /// </summary>
    internal class SampleNavigation {



        /// <summary>
        /// Runs a series of sample navigation displays their output to the console
        /// </summary>
        /// <param name="args"></param>
        internal void Run(string[] args) {


            WriteHeader("Int Navigation");
            IntNavigationSample.Start();
            WriteHeader("Letter Navigation");
            LetterNavigationSample.Start();
            WriteHeader("Distance Navigation");
            DistanceNavigationSample.Start();
            WriteHeader("Fluent Navigation");
            FluentNavigationSample.Start();
            WriteHeader("Progress Example");
            DoNavigateWithProgress();

            
        }

        

        /// <summary>
        /// Writes a big header block to the console 
        /// with the passed in message appearing in the middle
        /// 
        /// e.g.:
        ///========================================
        ///========================================
        ///========== YOUR MESSAGE HERE! ==========
        ///========================================
        ///========================================
        /// </summary>
        /// <param name="message">The message to appear in the text block. Messages longer than 20 characters may not appear correctly.</param>
        private void WriteHeader(string message) {
            WriteHeader(message, Console.Out);
        }

        /// <summary>
        /// Writes a big header block to the stream 
        /// with the passed in message appearing in the middle
        /// 
        /// e.g.:
        ///========================================
        ///========================================
        ///========== YOUR MESSAGE HERE! ==========
        ///========================================
        ///========================================
        /// </summary>
        /// <param name="message">The message to appear in the text block. Messages longer than 20 characters may not appear correctly.</param>
        /// <param name="stream">The stream to use to write the header block.</param>
        private void WriteHeader(string message, System.IO.TextWriter stream) {


            if(stream == null)
                throw new ArgumentException("stream cannot be null");
            if(message == null)
                throw new ArgumentException("message cannot be null");

            const int spacing = 3;

            string row = "========================================";
            string halfRow = row.Substring(0, (row.Length / 2) - ((message.Length + spacing) / 2));
            stream.WriteLine();
            stream.WriteLine(row);
            stream.WriteLine(row);
            stream.WriteLine("{0} {1} {2}", halfRow, message, halfRow + (message.Length % 2 != 0 ? "=" : String.Empty));
            stream.WriteLine(row);
            stream.WriteLine(row);
            stream.WriteLine();
        }

        /// <summary>
        /// Provides a sample navigation that will report on its progress
        /// 
        /// Watching the progress you will see the navigator go down false paths until discovering a dead end.
        /// then back tracking to find a different path until it eventually finds the right path.
        /// (A smarter heuristic might have avoided the false path altogether, but this heuristic is intentionally 
        /// dumb to show the inner workings of the algorithm and potential uses of the progresss feature).
        /// 
        /// Map:
        ///                ->p3-->p4-->end
        ///               /
        /// start-->p1--p2-->fp1_1-->fp1_2
        ///               \
        ///                ->fp2_1-->fp2_2
        ///                      \
        ///                       ->fp3_1
        /// 
        /// </summary>
        private void DoNavigateWithProgress() {

            const int WaitTime = 250;
            var start = new NavigationNode("start");
            var end   = new NavigationNode("end");

            var p1    = new NavigationNode("p1");
            var p2    = new NavigationNode("p2");
            var p3    = new NavigationNode("p3");
            var p4    = new NavigationNode("p4");
            var fp1_1 = new NavigationNode("fp1-1");
            var fp1_2 = new NavigationNode("fp1-2");
            var fp2_1 = new NavigationNode("fp2-1");
            var fp2_2 = new NavigationNode("fp2-2");
            var fp3_1 = new NavigationNode("fp3-1");


            NodeUtil.AddConnectionsInSeries(start, new[] { p1, p2 });
            NodeUtil.AddConnectionsInSeries(p2, new[] { fp1_1, fp1_2 });
            NodeUtil.AddConnectionsInSeries(p2, new[] { fp2_1, fp2_2 });
            fp2_1.AddConnection(fp3_1);
            NodeUtil.AddConnectionsInSeries(p2, new[] { p3, p4 });
            p4.AddConnection(end);

            var navigator = new Navigator<NavigationNode>(new SimpleConnectionFinder<NavigationNode>(), node => node.Name[0] == 'f' ? 0.0f : 1.0f);
            var progress = new ConsoleProgress<INavigationResult<NavigationNode>>(route => Console.WriteLine("In progress: {0}",  route));
            var result = navigator.Navigate(start, end, progress);

            Console.WriteLine("Final Route: {0}", result);

            //Simulate slow navigation
            navigator = new Navigator<NavigationNode>(new SimpleConnectionFinder<NavigationNode>(), node => {
                Thread.Sleep(WaitTime);
                return node.Name[0] == 'f' ? 0.0f : 1.0f;
            });

            Console.WriteLine();
            string emptyLine = new string(' ', Console.BufferWidth);
            progress = new ConsoleProgress<INavigationResult<NavigationNode>>(route => {
                Thread.Sleep(WaitTime);
                Console.Write("\rIn progress: {0}{1}", route, emptyLine.Substring(0, route.ToString().Length));
            });
            result = navigator.Navigate(start, end, progress);
            Thread.Sleep(WaitTime);
            Console.WriteLine();
            Console.WriteLine("Final Route: {0}", result);

        }
    }
}
