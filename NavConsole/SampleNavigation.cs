using NavCore.DistanceNavigation;
using NavCore.Navigation;
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

        private const string  Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string Vowels = "AEIOUY";

        /// <summary>
        /// Runs a series of sample navigation displays their output to the console
        /// </summary>
        /// <param name="args"></param>
        internal void Run(string[] args) {

            WriteHeader("Letter Navigation");
            DoLetterNavigation(args.FirstOrDefault());
            WriteHeader("Distance Navigation");
            DoDistanceNavigation();
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
        ///  Map would look some thing like this:
        /// 
        /// 
        ///    B-C-D   J-K-L-M-N  V-W-X
        ///     \   \   \     /  /   /
        ///      A---E---I---O---U---Y-Z
        ///         /   /   /     \
        ///        F-G-H   P-Q-R-S-T
        ///        
        /// The quickest path from A-Z is straight through the vowels 
        /// (all connections between individual letters are equal)
        /// </summary>
        /// <param name="args"></param>
        private void DoLetterNavigation(string favouriteLetters) {

            //nodes are setup so that each letter is connected sequentially, and so is each vowel
            Dictionary<char, NavigationNode> nodes = SetupNodes();
            Navigator<NavigationNode> navigator;

            //navigate while prefering vowels - using a lambda that only considers potential node to navigate
            Console.WriteLine("Prefer vowels:");
            navigator = new Navigator<NavigationNode>(p => IsVowel(p.Name) ? 0.0f : 1.0f);
            Console.WriteLine(navigator.Navigate(nodes.First().Value, nodes.Last().Value));

            //navigate by prefering consonants 
            Console.WriteLine("Prefer consonants:");
            navigator = new Navigator<NavigationNode>(p => IsVowel(p.Name) ? 1.0f : 0.0f);
            Console.WriteLine(navigator.Navigate(nodes.First().Value, nodes.Last().Value));

            //prefer nodes closer to the end - uses a more complex lamda to consider current, potential, and destination nodes
            Console.WriteLine("Prefer letters close to Z:");
            navigator = new Navigator<NavigationNode>((c, p, d) => d.Name[0] - p.Name[0]);
            Console.WriteLine(navigator.Navigate(nodes.First().Value, nodes.Last().Value));

            //prefer vowels less as you near the end
            Console.WriteLine("Prefer vowels less as you near the end:");
            navigator = new Navigator<NavigationNode>((c, p, d) => IsVowel(p.Name) ? 1.0f - (d.Name[0] - c.Name[0]) / (float)Letters.Count() : 0.5f);
            Console.WriteLine(navigator.Navigate(nodes.First().Value, nodes.Last().Value));

            //Use Comparer Class            
            FavouriteComparer comparer = new FavouriteComparer();
            comparer.FavouriteCharacters = favouriteLetters ?? "AEFOPY";
            Console.WriteLine("Using a comparer class to prefer our favourite characters: {0}", comparer.FavouriteCharacters);
            navigator = new Navigator<NavigationNode>(comparer);
            Console.WriteLine(navigator.Navigate(nodes.First().Value, nodes.Last().Value));

            //Navigate by following random valid paths
            Console.WriteLine("Navigating randomly:");
            var random = new Random();
            navigator = new Navigator<NavigationNode>(p => random.Next());
            for (int i = 0; i < 5; ++i)
                Console.WriteLine(navigator.Navigate(nodes.First().Value, nodes.Last().Value));

        }

        /// <summary>
        /// Demonstrates sample navigation using Euclidian distance.
        /// Also demonstrates the effect of properly weighting your heuristic.
        /// 
        /// Map would some thing like this:
        /// 
        ///          ce-+ 
        ///   w     /   e
        ///    \   |   / |
        ///     \  |  m  |
        ///      \ | /   |
        ///        s    /
        ///         \  /
        ///          cs
        ///          
        /// Legend:
        /// s: start
        /// e: end
        /// w: wrong
        /// m: middle
        /// cs: CloseToStart
        /// ce: CloseToEnd
        /// 
        /// We can see that the most effective path is via middle node
        /// </summary>
        private void DoDistanceNavigation() {

            var start   = new PositionNode("Start", new NavPoint(0, 0));
            var end     = new PositionNode("End", new NavPoint(100, 100));
            var middle  = new PositionNode("Middle", new NavPoint(50, 50));
            var wrong   = new PositionNode("WrongDirection", new NavPoint(-100, -100));
            var closeToStart = new PositionNode("CloseToStart", new NavPoint(20, -20));
            var closeToEnd   = new PositionNode("CloseToEnd", new NavPoint(100, 120));

            start.AddConnections(new[] { wrong, middle, closeToStart, closeToEnd });            
            middle.AddConnection(end);
            closeToEnd.AddConnection(end);
            closeToStart.AddConnection(end);
            

            var comparer = new DistancePathWeighter();
            var navigator = new Navigator<PositionNode>( comparer);

            //Notice that the navigator will always find a path if one exists
            //However effectively weighting our heuristic can improve our results
            Console.WriteLine("Distance Navigation - with different weights:");
            //using default weighting
            Console.WriteLine(navigator.Navigate(start, end));
            comparer.SetWeights(0.9, 0.1);
            Console.WriteLine(navigator.Navigate(start, end));
            comparer.SetWeights(0.1, 0.9);
            Console.WriteLine(navigator.Navigate(start, end));
        }

        /// <summary>
        /// True if equal to A,E,I,O,U, or Y
        /// 
        /// Note, Y is included
        /// </summary>
        private static bool IsVowel(char c) {
            return Vowels.Any(v => v == c);
        }

        /// <summary>
        /// True if equal to A,E,I,O,U, or Y
        /// 
        /// Note, Y is included
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsVowel(string c) {
            return Vowels.Any(v => v.ToString() == c);
        }

        /// <summary>
        /// Sets up a series of nodes where each letter connects to the next alphabetically
        /// (i.e. A is connected to B, B to C, C to D, etc)
        /// 
        /// And vowels are connected to each other sequentially in a similar manner (A to E, E to I, I to O, etc)
        /// 
        /// Therefore the fastest route to travel across vowels
        /// </summary>
        /// <returns></returns>
        private static Dictionary<char, NavigationNode> SetupNodes() {

            var nodes = new Dictionary<char, NavigationNode>();

            //create node for each letter
            foreach (char c in Letters)
                nodes.Add(c, new NavigationNode(c.ToString()));

            //connect every node
            char last = Letters.First();
            foreach (char c in Letters.Skip(1)) {
                nodes[last].AddConnection(nodes[c]);
                last = c;
            }

            //connect vowel nodes
            last = Vowels.First();
            foreach (var c in Vowels.Skip(1)) {
                nodes[last].AddConnection(nodes[c]);
                last = c;
            }

            return nodes;
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
            var end = new NavigationNode("end");

            var p1 = new NavigationNode("p1");
            var p2 = new NavigationNode("p2");
            var p3 = new NavigationNode("p3");
            var p4 = new NavigationNode("p4");
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

            var navigator = new Navigator<NavigationNode>(node => node.Name[0] == 'f' ? 0.0f : 1.0f);
            var progress = new ConsoleProgress<INavigationResult<NavigationNode>>(route => Console.WriteLine("In progress: {0}",  route));
            var result = navigator.Navigate(start, end, progress);

            Console.WriteLine("Final Route: {0}", result);

            //Simulate slow navigation
            navigator = new Navigator<NavigationNode>(node => {
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
