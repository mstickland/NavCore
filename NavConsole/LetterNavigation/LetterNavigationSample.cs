using NavCore.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole.LetterNavigation
{
    public class LetterNavigationSample
    {

        /// <summary>
        ///  In the following examples letters are treated as nodes where  each letter connects to the next alphabetically
        /// (i.e. A is connected to B, B to C, C to D, etc)
        /// 
        /// And vowels are connected to each other sequentially in a similar manner (A to E, E to I, I to O, etc)
        /// 
        /// Therefore the fastest route to travel across vowels
        /// 
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
        public static void Start()
        {

            var sample = new LetterNavigationSample();

            sample.PreferVowels();            
            sample.PreferConsonants();
            sample.PreferLettersCloseToZ();
            sample.PreferVowelsInverseToProgress();
            sample.RandomNavigation();
            
        }

      

        private void PreferVowels()
        {
            
            //navigate while prefering vowels - using a lambda that only considers potential node to navigate
            Console.WriteLine("Prefer vowels:");
            Navigator<char> navigator = new Navigator<char>(new LetterFinder(), c => LetterFinder.IsVowel(c) ? 0.0f : 1.0f);

            var path = navigator.Navigate('a', 'z');
            Console.WriteLine(path);
        }

        private void PreferConsonants()
        {
            Console.WriteLine("Prefer consonants:");
            Navigator<char> navigator = new Navigator<char>(new LetterFinder(), c => LetterFinder.IsVowel(c) ? 1.0f : 0.0f);

            var path = navigator.Navigate('a', 'z');
            Console.WriteLine(path);
        }

        private void PreferLettersCloseToZ()
        {

            Console.WriteLine("Prefer letters close to Z:");
            //prefer nodes closer to the end - uses a more complex lamda to consider current, potential, and destination nodes
            var navigator = new Navigator<char>(new LetterFinder(), (c, p, d) => d - p);

            var path = navigator.Navigate('a', 'z');
            Console.WriteLine(path);
        }

        private void PreferVowelsInverseToProgress()
        {
            //prefer vowels less as you near the end
            Console.WriteLine("Prefer vowels less as you near the end:");
            var navigator = new Navigator<char>(new LetterFinder(), (c, p, d) => LetterFinder.IsVowel(p) ? 1.0f - (d - c) / 26.0f : 0.5f);
            
            var path = navigator.Navigate('a', 'z');
            Console.WriteLine(path);
        }

        private void PreferFavouriteLetters()
        {

            //Use Comparer Class            
            FavouriteLetterComparer comparer = new FavouriteLetterComparer("AEFOPY");            
            Console.WriteLine("Using a comparer class to prefer our favourite characters: {0}", comparer.FavouriteCharacters);
            
            var navigator = new Navigator<char>(new LetterFinder(), comparer);            

            var path = navigator.Navigate('a', 'z');
            Console.WriteLine(path);
        }

        private void RandomNavigation()
        {
            //Navigate by following random valid paths
            Console.WriteLine("Navigating randomly:");
            var random = new Random();
            var navigator = new Navigator<char>(new LetterFinder(), p => random.Next());
            for (int i = 0; i < 5; ++i)
                Console.WriteLine(navigator.Navigate('a', 'z'));
        }
        

        /// <returns></returns>
    

    }
}
