using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeServer
{
    class GenerateLetter
    {
        public static string GenerateLetters(string type)
        {
            var chars = "";
            if (type.ToLower().Equals("vowels"))
            {
                chars = "AEIOU";
            }
            else if (type.Equals("Consonants"))
            {
                chars = "BCDFGHJKLMNPQRSTVWXYZ";
            }

            var stringChars = "";
            var random = new Random();
            stringChars = chars[random.Next(chars.Length)].ToString();

            return stringChars;
        }
    }
}
