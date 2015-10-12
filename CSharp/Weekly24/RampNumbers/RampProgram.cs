using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.RampNumbers {
    /// <summary>
    /// Weekly 24 - mini challenges
    /// Ramp numbers by Atrolantra
    /// https://www.reddit.com/r/dailyprogrammer/comments/3o4tpz/weekly_24_mini_challenges/cvudq0c
    /// 
    /// A ramp number is a number whose digits from left to right either only rise or stay the same. 1234 is a ramp number as is 1124. 1032 is not.
    /// 
    /// Given: A positive integer, n.
    /// Output: The number of ramp numbers less than n.
    /// Example input: 123
    /// Example output: 65
    /// Challenge input: 99999
    /// Challenge output: 2001
    /// </summary>
    /// <remarks>
    /// Naive brute force implementation. Very slow.
    /// Note: This implementation allows negative numbers. The range is flipped from 0 - n to n - 0 when n is negative.
    /// Although it would have been simpler to just Math.Abs the negative arg, oh well. ¯\_(ツ)_/¯
    /// </remarks>
    class RampProgram {
        static void Main(string[] args) {
            if (args.Length < 1) {
                PrintError("No number given.");
                return;
            }

            long numberRange;
            if (!long.TryParse(args[0], out numberRange)) {
                PrintError("That's not a number or it's a too big number. :C");
                return;
            }

            long start;
            long stop;
            if (numberRange >= 0) {
                start = 0;
                stop = numberRange;
            } else {
                start = numberRange + 1;
                stop = 1;
            }

            int count = 0;
            for (long n = start; n < stop; n++) {
                if (DerpMath.IsRampNumberFaster(n)) {
                    count++;
                    //Debug.WriteLine(rampNumber, "rampNumber");
                }
            }
            //Debug.WriteLine(count, "count");
            Console.WriteLine(count);
        }

        private static void PrintError(string errorMsg) {
            Console.WriteLine(errorMsg);
            Console.WriteLine();
        }
    }
}
