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

            var matches = new Dictionary<string, List<string>>();
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                if (String.IsNullOrWhiteSpace(line)) continue;

                string anagramKey = CopyLettersNumbersAndSort(line);

                List<string> anagrams;
                if (!matches.TryGetValue(anagramKey, out anagrams)) {
                    anagrams = new List<string>();
                    matches.Add(anagramKey, anagrams);
                }
                anagrams.Add(line);
            }

            int anagramMatchCount = 0;
            foreach (var anagrams in matches.Values) {
                if (anagrams.Count > 1) {
                    // 2 => 1 match; 3 => 3 matches; 4 => (1+2+3=)6 matches
                    for (int i = 1; i < anagrams.Count; i++) {
                        anagramMatchCount += i;
                    }
                    Console.WriteLine(String.Join(" <=> ", anagrams));

                }
            }
            Console.WriteLine("Anagram matches: {0}", anagramMatchCount);
        }

        private static string CopyLettersNumbersAndSort(string str) {
            char[] origChars = new char[str.Length];
            int length = 0;
            foreach (char c in str) {
                if (Char.IsLetter(c) || Char.IsNumber(c)) {
                    origChars[length] = Char.IsUpper(c)
                        ? Char.ToLower(c)
                        : c;
                    length++;
                }
            }
            char[] chars = new char[length];
            Array.Copy(origChars, chars, length);
            Array.Sort(chars);

            return new String(chars);
        }
    }
}
