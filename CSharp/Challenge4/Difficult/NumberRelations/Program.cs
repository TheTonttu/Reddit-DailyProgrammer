using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private static Func<long, long, CalcOperation>[] DefinedOps = {
            (a, b) => new CalcOperation(a, Operation.Addition, b),
            (a, b) => new CalcOperation(a, Operation.Subtraction, b),
            (a, b) => new CalcOperation(a, Operation.Multiplication, b),
            (a, b) => new CalcOperation(a, Operation.Division, b)
        };

        static void Main(string[] args) {
            // TODO: input format? 'x, y, z', 'x,y,z' or 'x y z'? or support all?
            var numbers = new List<long>();
            foreach (var arg in args) {
                long number;
                if (long.TryParse(arg, out number)) {
                    numbers.Add(number);
                } else {
                    throw new ArgumentException(String.Format("'{0}' is not a number.", arg));
                }
            }

            var combos = GetNumberCombinations(numbers);
            List<CalcOperation> operations = new List<CalcOperation>();
            foreach (var combo in combos) {
                if (combo.Count == 3) {
                    long expectedResult = combo[2];
                    foreach (var opFunc in DefinedOps) {
                        long a = combo[0];
                        long b = combo[1];
                        var op = opFunc(a, b);
                        if (op.Result == expectedResult) {
                            operations.Add(op);
                        }
                    }
                }
            }

            foreach (var op in operations) {
                Console.WriteLine(op);
            }
            Console.ReadKey();
        }

        private static List<List<long>> GetNumberCombinations(List<long> numbers) {
            var combos = new List<List<long>>();
            if (numbers.Count <= 1) {
                combos.Add(numbers);
                return combos;
            }

            for (int i = 0; i < numbers.Count; i++) {
                long firstNumber = numbers[i];
                var remainingNumbers = new List<long>();
                for (int j = 0; j < numbers.Count; j++) {
                    if (i != j) {
                        remainingNumbers.Add(numbers[j]);
                    }
                }
                var remainingCombo = GetNumberCombinations(remainingNumbers);
                foreach (var combo in remainingCombo) {
                    combo.Insert(0, firstNumber);
                    combos.Add(combo);
                }
            }
            return combos;
        } 
    }
}
