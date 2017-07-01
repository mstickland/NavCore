using NavCore.Navigation;
using NavCore.Navigation.PathWeighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole {


    internal class FavouriteComparer : IPathWeighter<NavigationNode> {

        private string _favouriteChars;
        public string FavouriteCharacters { get { return _favouriteChars ?? String.Empty; } set { _favouriteChars = value; } }

        public double GetPathWeight(NavigationNode start, NavigationNode current, NavigationNode potential, NavigationNode destination, List<NavigationNode> pathSoFar) {
            if (String.IsNullOrEmpty(potential.Name))
                return 1.0f;

            return FavouriteCharacters.Any(c => potential.Name[0] == c) ? 0.0f : 1.0f;

        }

    }
}
