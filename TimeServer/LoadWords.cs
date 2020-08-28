using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TimeServer
{
    class LoadWords
    {
        public static List<string> LoadWordsList()
        {
            List<string> ValidWords = new List<string>();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\words.txt");
            foreach (var w in File.ReadAllLines(path))
            {
                ValidWords.Add(w);
            }

            return ValidWords;
        }
    }
}