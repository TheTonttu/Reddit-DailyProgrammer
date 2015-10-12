using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBench;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.RampNumbers.Benchmark {
    class RampBenchProgram {
        private const long MAX_NUMBER = 999999999L;

        static void Main(string[] args) {
            var isRampBenchs = InitializeRampBenchmarks();

            // Runs benchs for at least 1 second
            Console.WriteLine(isRampBenchs.Bench());

            Console.ReadKey();
        }

        private static Action[] InitializeRampBenchmarks() {
            var range = EnumerableEx.Range(0L, MAX_NUMBER);
            int rangeInitCount = range.Count();

            int a1Count;
            int a2Count;
            int a3Count;
            int a4Count;
            Action a1 = () => { a1Count = BenchIsRampNumberEnumerable(DerpMath.IsRampNumber, range); };
            Action a2 = () => { a2Count = BenchIsRampNumberEnumerable(DerpMath.IsRampNumberFaster, range); };
            Action a3 = () => { a3Count = BenchIsRampNumberForLoop(DerpMath.IsRampNumberFaster, MAX_NUMBER); };
            Action a4 = () => { a4Count = BenchIsRampNumberForLoop(DerpMath.IsRampNumberStringManipulation, MAX_NUMBER); };

            var isRampBenchs = new Action[] { 
                // awful performance: a1,
                a2, a3
                // even more awful performance:, a4
            };
            return isRampBenchs;
        }


        static int BenchIsRampNumberEnumerable(Func<long, bool> method, IEnumerable<long> range) {
            int count = range
                .Where(n => method(n))
                .Count();

            return count;
        }

        static int BenchIsRampNumberForLoop(Func<long, bool> method, long maxNumber) {
            int count = 0;            
            for (int i = 0; i < maxNumber; i++) {
                if (method(i)) {
                    count++;
                }
            }
            return count;
        }
    }
}
