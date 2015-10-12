using System;
using System.Linq;

namespace Tonttu.Reddit.DailyProgrammer.Weekly24.BaseN {
    /// <summary>
    /// Weekly #24
    /// To base n by casualfrog
    /// https://www.reddit.com/r/dailyprogrammer/comments/3o4tpz/weekly_24_mini_challenges/cvu1xu3
    /// 
    /// To base n - Convert a base 10 number to given base
    /// 
    /// Given: an integer x in base 10 and an integer base n between 2 and 10
    /// Output: x in base n
    /// Example input: 987 4
    /// Example output: 33123
    /// </summary>
    /// <remarks>
    /// Allows converting from base 10 to base 2-36. Negative numbers are printed same as positive but with minus sign.
    /// </remarks>
    class BaseNProgram {
        // Could also add a-z and other characters...
        private static readonly char[] digitCharMap = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        static void Main(string[] args) {
            var argParser = new BaseNArgumentParser(digitCharMap.Length);

            BaseNArguments arguments;
            try {
                arguments = argParser.ParseArgs(args);
            } catch (ArgumentException e) {
                PrintUsage(e.Message);
                return;
            }

            if (arguments.Base == 10) {
                Console.WriteLine(arguments.Number);
                return;
            }

            Number result = BaseConverter.Convert(arguments.Number, arguments.Base);
            Console.WriteLine(ConvertToString(result));
        }

        private static string ConvertToString(Number result) {
            string mappedDigits = new String(result.Digits.Select(d => digitCharMap[d]).ToArray());

            return result.IsNegative
                ? "-" + mappedDigits
                : mappedDigits;
        }

        private static void PrintUsage(string errorMsg) {
            Console.WriteLine(errorMsg);
            Console.WriteLine("Usage: basen.exe x b");
            Console.WriteLine("Where x is base10 number and b is base(2-{0}). The number is converted to the provided base.", digitCharMap.Length);
            Console.WriteLine();
        }
    }
}
