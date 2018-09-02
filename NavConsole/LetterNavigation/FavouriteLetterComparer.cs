using NavCore.Navigation.PathWeighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole.LetterNavigation
{
    internal class FavouriteLetterComparer : IPathWeighter<char>
    {
        public string FavouriteCharacters { get; private set; }

        public FavouriteLetterComparer(string favouriteCharacters)
        {
            FavouriteCharacters = favouriteCharacters;
        }

        public double GetPathWeight(char start, char current, char potential, char destination, IEnumerable<char> pathSoFar)
        {
            return FavouriteCharacters.Any(c => potential == c) ? 0.0f : 1.0f;
        }
    }
}
