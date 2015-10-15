using System;
using System.Linq;
using UBench;

namespace Tonttu.Reddit.DailyProgrammer.Challenge3.ScrambledWords.Benchmark {

    class BenchProgram {
        private const string SortTarget = "öäåzyxwvutsrqponmlkjihgfedcbaÖÄÅZYXWVUTSRQPONMLKJIHGFEDCBA";

        static void Main(string[] args) {
            Action[] benchmarks = {
                () => String.Concat(SortTarget.OrderBy(c => c)),
                () => new String(SortTarget.OrderBy(c => c).ToArray()),
                () => CopyAndSort(SortTarget)
            };
            
            Console.WriteLine(benchmarks.Bench());
            Console.WriteLine();
            Console.ReadKey();
        }

        private static string CopyAndSort(string unordered) {
            char[] chars = unordered.ToArray();
            Array.Sort(chars);
            string ordered = new String(chars);
            return ordered;
        }
    }
}
