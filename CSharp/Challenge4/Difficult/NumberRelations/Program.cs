using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Tonttu.Reddit.DailyProgrammer.Challenge4.NumberRelations {
    /// <summary>
    /// Number relations by nottoobadguy
    /// https://www.reddit.com/r/dailyprogrammer/comments/pm7g7/2122012_challange_4_difficult/
    /// 
    /// Today your challenge is to create a program that will take a series of numbers (5, 3, 15), and find how those numbers can add, subtract, multiply, or divide in various ways to relate to eachother. This string of numbers should result in 5 * 3 = 15, or 15 /3 = 5, or 15/5 = 3. When you are done, test your numbers with the following strings:
    /// 4, 2, 8
    /// 6, 2, 12
    /// 6, 2, 3
    /// 9, 12, 108
    /// 4, 16, 64
    /// For extra credit, have the program list all possible combinations.
    /// For even more extra credit, allow the program to deal with strings of greater than three numbers.For example, an input of(3, 5, 5, 3) would be 3 * 5 = 15, 15/5 = 3. When you are finished, test them with the following strings.
    /// 2, 4, 6, 3
    /// 1, 1, 2, 3
    /// 4, 4, 3, 4
    /// 8, 4, 3, 6
    /// 9, 3, 1, 7
    /// </summary>
    class Program {
        private const double EPSILON = 0.00001;

        private static Func<double, double, CalcOperation>[] DefinedOps = {
            (a, b) => new CalcOperation(a, b, Operation.Addition),
            (a, b) => new CalcOperation(a, b, Operation.Subtraction),
            (a, b) => new CalcOperation(a, b, Operation.Multiplication),
            (a, b) => new CalcOperation(a, b, Operation.Division),
            // Modulo seems to work fine but it glutters the results, so leaving it off
            //(a, b) => new CalcOperation(a, b, Operation.Modulo)
        };

        static void Main(string[] args) {
            var numbers = new List<double>();
            foreach (var arg in args) {
                double number = ParseNumber(arg);
                numbers.Add(number);
            }
            if (numbers.Count < 3) {
                Console.WriteLine("Minimum 3 numbers required. Two for calculation and one for expected result.");
            }

            var operationChains = CreateMatchingOperationChains(numbers);
            var distinct = operationChains.Select(oc => String.Join(", ", oc)).Distinct();
            foreach (var opChain in distinct) {
                Console.WriteLine(opChain);
            }
        }

        private static double ParseNumber(string arg) {
            double number;
            var trimmedArg = arg.TrimEnd(',');
            if (Double.TryParse(trimmedArg, out number)) {
                return number;
            } else {
                // Try parsing with invariant culture if normal parse fails (e.g. decimal point is different than current culture)
                if (Double.TryParse(trimmedArg, NumberStyles.Any, CultureInfo.InvariantCulture, out number)) {
                    return number;
                }
            }
            throw new ArgumentException(String.Format("'{0}' is not a number.", arg));
        }

        private static List<List<CalcOperation>> CreateMatchingOperationChains(List<double> numbers) {
            if (numbers.Count < 3) {
                return new List<List<CalcOperation>>();
            }

            int maxIndex = DefinedOps.Length - 1;
            // calc count per row = numbers - 2
            // e.g. x, y, r = (x op y) = r
            // or x, y, z, r = ((x op y) op z) = r
            var calcCount = numbers.Count - 2;
            var calcIndexTable = GenerateIndexTable(calcCount, maxIndex);

            var operationChains = new List<List<CalcOperation>>();
            var combos = CreateCombinations(numbers);
            foreach (var calcIndices in calcIndexTable) {
                foreach (var combo in combos) {
                    int lastComboIndex = combo.Count - 1;
                    double expectedResult = combo[lastComboIndex];
                    List<CalcOperation> comboOpChain = CreateOperationChain(combo, calcIndices);
                    if (IsExpectedResult(comboOpChain, expectedResult)) {
                        operationChains.Add(comboOpChain);
                    }
                }
            }
            return operationChains;
        }

        private static List<List<int>> GenerateIndexTable(int indexTableLength, int maxIndex) {
            var indexTable = new List<List<int>>();

            int maxIter = 0;
            for (int i = 0; i < indexTableLength; i++) {
                maxIter = maxIter * 10 + maxIndex;
            }

            for (int n = 0; n <= maxIter; n++) {
                List<int> indices = new List<int>();
                int remaining = n;
                bool containsInvalidIndexes = false;
                while (remaining > 0) {
                    int index = remaining % 10;
                    if (index > maxIndex) {
                        containsInvalidIndexes = true;
                        break;
                    }
                    remaining /= 10;
                    indices.Add(index);
                }

                if (!containsInvalidIndexes) {
                    // Pad with zeroes.
                    while (indices.Count < indexTableLength) {
                        indices.Insert(0, 0);
                    }
                    indexTable.Add(indices);
                }
            }

            return indexTable;
        }

        private static List<List<T>> CreateCombinations<T>(List<T> items) {
            var combos = new List<List<T>>();
            if (items.Count <= 1) {
                combos.Add(items);
                return combos;
            }

            for (int i = 0; i < items.Count; i++) {
                T firstItem = items[i];
                var remainingItems = new List<T>();
                for (int j = 0; j < items.Count; j++) {
                    if (i != j) {
                        remainingItems.Add(items[j]);
                    }
                }
                var remainingCombo = CreateCombinations(remainingItems);
                foreach (var combo in remainingCombo) {
                    combo.Insert(0, firstItem);
                    combos.Add(combo);
                }
            }
            return combos;
        }

        private static List<CalcOperation> CreateOperationChain(List<double> numbers, List<int> calcIndices) {
            int lastNumberIndex = numbers.Count - 1;
            var opChain = new List<CalcOperation>(numbers.Count - 2);
            CalcOperation prevOp = null;
            for (int i = 0; i < lastNumberIndex && i + 1 < lastNumberIndex; i++) {
                double a;
                if (i == 0) {
                    a = numbers[i];
                } else {
                    a = prevOp.Result;
                }
                double b = numbers[i + 1];

                var opFunc = DefinedOps[calcIndices[i]];
                var op = opFunc(a, b);
                opChain.Add(op);
                prevOp = op;
            }

            return opChain;
        }

        private static bool IsExpectedResult(List<CalcOperation> opChain, double expectedResult) {
            int lastOpIndex = opChain.Count - 1;
            var lastOp = opChain[lastOpIndex];
            return NearlyEqual(lastOp.Result, expectedResult, EPSILON);
        }

        private static bool NearlyEqual(double a, double b, double epsilon) {
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);

            if (a == b) {
                return true;
            } else if (a == 0 || b == 0 || diff < Double.Epsilon) {
                return diff < (epsilon * Double.Epsilon);
            } else {
                return diff / Math.Min((absA + absB), Double.MaxValue) < epsilon;
            }
        }
    }
}
