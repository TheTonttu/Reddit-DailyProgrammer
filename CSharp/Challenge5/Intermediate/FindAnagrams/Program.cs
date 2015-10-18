using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Challenge5.FindAnagrams {
    /// <summary>
    /// Find anagrams by nottoobadguy
    /// (https://www.reddit.com/r/dailyprogrammer/comments/pnhtj/2132012_challenge_5_intermediate/)
    /// 
    /// Your challenge today is to write a program that can find the amount of anagrams within a .txt file. For example, "snap" would be an anagram of "pans", and "skate" would be an anagram of "stake".
    /// </summary>
    /// <remarks>
    /// The challenge description is a bit broad so I'm assuming every line has word, or short sentence.
    /// </remarks>
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("File required.");
                Console.WriteLine();
                return;
            }
            string filePath = args[0];

            if (!File.Exists(filePath)) {
                Console.WriteLine("File does not exist.");
                Console.WriteLine();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            int anagramMatchCount = 0;
            // TODO: Do not count matches twice. e.g. test = sets; sets = test
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                string sorted = CopyLettersNumbersAndSort(line);

                int lineMatchCount = 0;
                for (int j = 0; j < lines.Length; j++) {
                    if (i == j) continue;
                    string otherLine = lines[j];
                    string otherSorted = CopyLettersNumbersAndSort(otherLine);
                    //Console.WriteLine("comparison: '{0}', '{1}'", sorted, otherSorted);
                    if (sorted.Length != otherSorted.Length) continue;

                    if (String.Equals(sorted, otherSorted, StringComparison.OrdinalIgnoreCase)) {
                        lineMatchCount++;
                        //Console.WriteLine("match: '{0}', '{1}'", line, otherLine);
                    }
                }
                anagramMatchCount += lineMatchCount;
                if (lineMatchCount > 0) {
                    Console.WriteLine("'{0}' matches: {1}", line, lineMatchCount);
                }
            }
            if (anagramMatchCount > 0) {
                Console.WriteLine();
            }
            Console.WriteLine("Total matches: {0}", anagramMatchCount);
        }

        private static string CopyLettersNumbersAndSort(string str) {
            char[] origChars = new char[str.Length];
            int charCount = 0;
            foreach (char c in str) {
                if (Char.IsLetter(c) || Char.IsNumber(c)) {
                    origChars[charCount] = Char.IsUpper(c) ? Char.ToLower(c) : c;
                    charCount++;
                }
            }
            int length = charCount;
            char[] chars = new char[length];
            Array.Copy(origChars, chars, length);
            Array.Sort(chars);

            return new String(chars);
        }
    }
}
