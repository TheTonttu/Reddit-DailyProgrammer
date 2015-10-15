using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScrambledWords {
    /// <summary>
    /// Scrambled words by nottoobadguy
    /// https://www.reddit.com/r/dailyprogrammer/comments/pkwgf/2112012_challenge_3_difficult/
    /// THIS WORD LIST: http://pastebin.com/jSD873gL
    /// 
    /// Welcome to cipher day!
    /// For this challenge, you need to write a program that will take the scrambled words from this post, and compare them against THIS WORD LIST to unscramble them. For bonus points, sort the words by length when you are finished. Post your programs and/or subroutines!
    /// Here are your words to de-scramble:
    /// mkeart
    /// sleewa
    /// edcudls
    /// iragoge
    /// usrlsle
    /// nalraoci
    /// nsdeuto
    /// amrhat
    /// inknsy
    /// iferkna
    /// </summary>
    class Program {
        static void Main(string[] args) {
            string[] scrambledWords = File.ReadAllLines("scrambledwords.txt");
            string[] wordList = File.ReadAllLines("wordlist.txt");

            List<string> matches = new List<string>();
            foreach (string scrambled in scrambledWords) {
                string orderedScramble = CopyAndSort(scrambled);
                foreach (string word in wordList) {
                    if (orderedScramble.Length != word.Length) continue;

                    string orderedWord = CopyAndSort(word);
                    if (String.Equals(orderedScramble, orderedWord)) {
                        matches.Add(String.Format("{0} = {1}", word, scrambled));
                    }
                }
            }
            matches.Sort(SortByLength);
            matches.ForEach(Console.WriteLine);

            Console.ReadKey();
        }

        private static int SortByLength(string s1, string s2) {
            int lencomp = s1.Length.CompareTo(s2.Length);
            return lencomp == 0
                ? s1.CompareTo(s2)
                : lencomp;
        }

        // Faster than LINQ String.Concat(word.OrderBy(c => c))
        private static string CopyAndSort(string unordered) {
            char[] chars = unordered.ToArray();
            Array.Sort(chars);
            string ordered = new String(chars);
            return ordered;
        }
    }
}
