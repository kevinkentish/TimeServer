using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeServer
{
    class CountWords
    {
        public static void CheckWordLenght(String input)
        {
            Winner.score += input.Length;
        }       
    }
    public static class Winner
    {
        public static int score = 0;
    }
}
