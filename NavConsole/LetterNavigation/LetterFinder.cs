using NavCore.Navigation.ConnectionFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole.LetterNavigation
{
    internal class LetterFinder : IConnectionFinder<Char>
    {
        
        private const string Vowels = "AEIOUY";

        public IEnumerable<char> GetConnections(char c)
        {
            if (!IsVowel(c))
                return new char[] { NextChar(c) };
            return new[] { NextChar(c), NextVowel(c) };
        }

        public static bool IsVowel(Char c)
        {
            return Vowels.Contains(Char.ToUpper(c));
        }

        public static char NextChar(Char c)
        {
            char nextChar = (char)(c + 1);
            return Char.ToUpper(nextChar) > 'Z' ? '\0' : nextChar;
        }

        public static char NextVowel(Char c)
        {
            for (int i = 0; i < Vowels.Length; ++i)
            {
                int diff = Vowels[i] - Char.ToUpper(c);

                //returning the sum rather than Vowels[i], lets it work for upper or lower case 
                if (diff > 0)
                    return (char)(c + diff);
            }
            return '\0';
        }

        
    }
}
